using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MassTransit;
using Wiadomosci;

namespace OdbiorcaC
{
    class Program
    {
        public static Task Handler(ConsumeContext<IWiadomosc2> ctx) =>
            Console.Out.WriteLineAsync($"\nTresc: {ctx.Message.TrescWiadomosci2}");
        static void Main(string[] args)
        {
            var bus = Bus.Factory.CreateUsingRabbitMq(sbc =>
            {
                sbc.Host(new Uri("rabbitmq://localhost/103057"),
                h => { h.Username("guest"); h.Password("guest"); });
                sbc.ReceiveEndpoint("recvQueueC", ep =>
                {
                    ep.Handler<IWiadomosc2>(Handler);
                });
            });
            bus.Start();
            Console.WriteLine("----Odbiorca C----");
            Console.ReadKey();
            bus.Stop();
        }
    }
}
