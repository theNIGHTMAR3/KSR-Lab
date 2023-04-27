using System;
using System.Linq;
using System.Threading.Tasks;
using MassTransit;
using Wiadomosci;

namespace OdbiorcaA
{
    class Program
    {
        public static Task Handler(ConsumeContext<IWiadomosc1> ctx) =>
       Console.Out.WriteLineAsync(
                   $"Header1_student: {ctx.Headers.GetAll().Where(elem => elem.Key == "header1").First().Value}" +
                   $", Header2_indeks: {ctx.Headers.GetAll().Where(elem => elem.Key == "header2").First().Value}" +
                   $"\nTresc: {ctx.Message.TrescWiadomosci1}");

        static void Main(string[] args)
        {
            var bus = Bus.Factory.CreateUsingRabbitMq(sbc => {
                sbc.Host(new Uri("rabbitmq://localhost/103057"),
                h => { h.Username("guest"); h.Password("guest"); });
                sbc.ReceiveEndpoint("recvQueueA", ep => {
                    ep.Handler<IWiadomosc1>(Handler);
                });
            });
            bus.Start();
            Console.WriteLine("----Odbiorca A----");
            Console.ReadKey();
            bus.Stop();
        }
    }
}
