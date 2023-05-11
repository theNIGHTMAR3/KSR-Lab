using System;
using System.Threading.Tasks;
using MassTransit;
using Komunikaty;

namespace KlientA
{
    static class DaneKlienta
    {
        public const string Login = "KlientA";
    }
    class Skrzynka : IConsumer<IPytanieoPotwierdzenie>, IConsumer<IAkceptacjaZamowienia>, IConsumer<IOdrzucenieZamowienia>
    {
        public Task Consume(ConsumeContext<IPytanieoPotwierdzenie> context)
        {
            if (context.Message.Login == DaneKlienta.Login)
            {
                Console.WriteLine($"Czy zaakceptowac zamowienie: {context.Message.CorrelationId} na ilosc{context.Message.Ilosc} ? [T/N] ");
                bool odp = false;
                ConsoleKey response = Console.ReadKey().Key;
                if (response == ConsoleKey.T)
                    odp = true;
                else if (response == ConsoleKey.N)
                    odp = false;

                Console.WriteLine();
                return Task.Run(() =>
                {
                    if (odp)
                        context.RespondAsync(new Potwierdzenie() { CorrelationId = context.Message.CorrelationId });
                    else
                        context.RespondAsync(new BrakPotwierdzenia() { CorrelationId = context.Message.CorrelationId });
                });
            }
            else
                return Task.Run(() => { });
        }

        public Task Consume(ConsumeContext<IAkceptacjaZamowienia> context)
        {
            if (context.Message.Login == DaneKlienta.Login)
                return Console.Out.WriteLineAsync($"Zaakceptowano zamowienie {context.Message.CorrelationId} na ilosc {context.Message.Ilosc}");
            else
                return Task.Run(() => { });
        }

        public Task Consume(ConsumeContext<IOdrzucenieZamowienia> context)
        {
            if (context.Message.Login == DaneKlienta.Login)
                return Console.Out.WriteLineAsync($"Odrzucono zamowienie {context.Message.CorrelationId} na ilosc {context.Message.Ilosc}");
            else
                return Task.Run(() => { });
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var skrzynka = new Skrzynka();
            var bus = Bus.Factory.CreateUsingRabbitMq(
            sbc =>
            {
                sbc.Host(new Uri("rabbitmq://localhost/103057"),
                h => { h.Username("guest"); h.Password("guest"); });
                sbc.ReceiveEndpoint( "klientA",
                    ep => ep.Instance(skrzynka));
            });
            bus.Start();
            Console.WriteLine("----" + DaneKlienta.Login + "----");
            while (true)
            {
                if (Console.ReadKey().Key == ConsoleKey.Z)
                {
                    Console.WriteLine("\nPodaj ilosc produktow do zamowienia");

                    int ilosc = 0;
                    try
                    {
                        ilosc = Convert.ToInt32(Console.ReadLine());
                        bus.Publish(new StartZamowienia() { Login = DaneKlienta.Login, Ilosc = ilosc });
                        Console.WriteLine();
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Ilosc niepodana! Operacja anulowana");
                    }
                }
            }
            bus.Stop();
        }
    }
}
