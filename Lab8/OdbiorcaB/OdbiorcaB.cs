using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MassTransit;
using Wiadomosci;

namespace OdbiorcaB
{
    class HandlerClass : IConsumer<IWiadomosc_1_2>
    {
        int wartosc = 0;
        public Task Consume(ConsumeContext<IWiadomosc_1_2> ctx)
        {
            wartosc += 1;
            return Console.Out.WriteLineAsync(
                $"Odebranych wiadmosci: {wartosc}" +
                $"\nTresc1: {ctx.Message.TrescWiadomosci1}" +
                $"\nTresc2: {ctx.Message.TrescWiadomosci2}");
        }
    }
    class Program
    {
        static void Main(string[] args)
        {

            var instance = new HandlerClass();
            var bus = Bus.Factory.CreateUsingRabbitMq(sbc =>
            {
                sbc.Host(new Uri("rabbitmq://localhost/103057"),
                h => { h.Username("guest"); h.Password("guest"); });
                sbc.ReceiveEndpoint( "recvQueueB", ep =>
                {
                    ep.Instance(instance);
                });
            });
            bus.Start();
            Console.WriteLine("----Odbiorca B----");
            Console.ReadKey();
            bus.Stop();
        }
    }
}
