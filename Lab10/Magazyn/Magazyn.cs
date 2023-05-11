using System;
using System.Threading.Tasks;
using MassTransit;
using Komunikaty;

namespace Magazyn
{
    class Magazyn : IConsumer<IPytanieoWolne>, IConsumer<IAkceptacjaZamowienia>, IConsumer<IOdrzucenieZamowienia>
    {
        public int Wolne { get; set; } = 0;
        public int Zarezerwowane { get; set; } = 0;
        public Task Consume(ConsumeContext<IPytanieoWolne> context)
        {
            return Task.Run(() =>
            {

                if (Wolne >= context.Message.Ilosc)
                {
                    Wolne -= context.Message.Ilosc;
                    Zarezerwowane += context.Message.Ilosc;
                    Console.Out.WriteLineAsync($"Magazyn moze zrealizowac zamowienie {context.Message.CorrelationId} na ilosc {context.Message.Ilosc}");
                    context.RespondAsync(new OdpowiedzWolne() { CorrelationId = context.Message.CorrelationId });
                }
                else
                {
                    Zarezerwowane += context.Message.Ilosc;
                    Wolne -= context.Message.Ilosc;
                    Console.Out.WriteLineAsync($"Magazyn nie ma odpowiedniej liczby produktow na zrealizowanie zamowienia {context.Message.CorrelationId} w ilosci {context.Message.Ilosc}");
                    context.RespondAsync(new OdpowiedzWolneNegatywna() { CorrelationId = context.Message.CorrelationId });
                }
            });
        }

        public Task Consume(ConsumeContext<IAkceptacjaZamowienia> context)
        {
            return Task.Run(() =>
            {
                Zarezerwowane -= context.Message.Ilosc;
                Console.WriteLine($"Zrealizowano zamowienie: {context.Message.CorrelationId} w ilosci {context.Message.Ilosc}, paczka zostala wyslana do klienta");
            });
        }
        public Task Consume(ConsumeContext<IOdrzucenieZamowienia> context)
        {
            return Task.Run(() =>
            {
                Zarezerwowane -= context.Message.Ilosc;
                Wolne += context.Message.Ilosc;
                Console.WriteLine($"Odrzucono zamowienie: {context.Message.CorrelationId} w ilosci {context.Message.Ilosc}, zarezerwowane produkty sa ponownie dostepne w sprzedarzy");
            });
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            var magazyn = new Magazyn();
            var bus = Bus.Factory.CreateUsingRabbitMq(
            sbc =>
            {
                sbc.Host(new Uri("rabbitmq://localhost/103057"),
                h => { h.Username("guest"); h.Password("guest"); });
                sbc.ReceiveEndpoint( "magazyn",
                    ep => ep.Instance(magazyn));
            });
            Console.WriteLine("----Magazyn----");
            bus.Start();
            
            int liczbaDostarczonychPorduktow = 0;

            while (true)
            {
                if (Console.ReadKey().Key == ConsoleKey.D)
                {
                    Console.WriteLine("\nPodaj Liczbe Dostarczonych Produktow");
                    try
                    {
                        liczbaDostarczonychPorduktow = Convert.ToInt32(Console.ReadLine());
                        magazyn.Wolne += liczbaDostarczonychPorduktow;
                        Console.WriteLine($"\nStan Magazynu \nDOSTAWA: {liczbaDostarczonychPorduktow} \nWolne: {magazyn.Wolne} \nZarezerwowane: {magazyn.Zarezerwowane}");
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Nie podales liczby! Anulowano operacje");
                    }
                }
            }
            bus.Stop();
        }
    }
}
