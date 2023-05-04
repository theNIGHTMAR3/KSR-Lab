using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MassTransit;
using Wiadomosci;

namespace AbonentA
{
    class OdpA : IOdpA
    {
        public string Kto { get; set; }
    }

    class Skrzynka : IConsumer<IPubl>, IConsumer<Fault<IOdpA>>, IConsumer<Fault<IOdpB>>
    {
        public Task Consume(ConsumeContext<IPubl> context)
        {
            return Task.Run(() => 
            {
                if(context.Message.Numer % 2 == 0)
                    context.RespondAsync(new OdpA() { Kto = "abonent A" });
                Console.WriteLine($"Otrzymana wiadomosc: {context.Message.Numer} {(context.Message.Numer % 2 == 0 ? "wyslano odpowiedz" : "")}");
            });
        }

        public Task Consume(ConsumeContext<Fault<IOdpA>> context)
        {
            return Task.Run(() =>
            { 
                foreach(var e in context.Message.Exceptions)
                {
                    Console.WriteLine($"\n!!!!Nastapilo wyrzucenie wyjatku !!!! {e.Message} {context.Message.Message.Kto}\n");
                }
            });
        }

        public Task Consume(ConsumeContext<Fault<IOdpB>> context)
        {
            return Task.Run(() =>
            {
                foreach (var e in context.Message.Exceptions)
                {
                    Console.WriteLine($"\n!!!!Nastapilo wyrzucenie wyjatku !!!! {e.Message} {context.Message.Message.Kto}\n");
                }
            });
        }
    }
    class AbonentA
    {
        static void Main(string[] args)
        {
            var skrzynka = new Skrzynka();
            var bus = Bus.Factory.CreateUsingRabbitMq(sbc =>
            {
                sbc.Host(new Uri("rabbitmq://localhost/103057"),
                h => { h.Username("guest"); h.Password("guest"); });
                sbc.ReceiveEndpoint("abonentA", ep => {
                    ep.Instance(skrzynka);
                });
            });
            bus.Start();
            Console.WriteLine("----Abonent A----");
            Console.ReadKey();
            bus.Stop();
        }
    }
}
