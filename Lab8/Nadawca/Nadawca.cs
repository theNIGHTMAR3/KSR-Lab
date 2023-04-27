using System;
using MassTransit;
using Wiadomosci;

namespace Nadawca
{
    class Wiadomosc1 : IWiadomosc1
    {
        public string TrescWiadomosci1 { get; set; }
    }
    class Wiadomosc2 : IWiadomosc2
    {
        public string TrescWiadomosci2 { get; set; }
    }
    class Wiadomosc_1_2 : IWiadomosc_1_2
    {
        public string TrescWiadomosci1 { get; set; }
        public string TrescWiadomosci2 { get; set; }
    }
    class Program
    {
        static void Main(string[] args)
        {



            var bus = Bus.Factory.CreateUsingRabbitMq(sbc =>
            {
                sbc.Host(new Uri("rabbitmq://localhost/103057"),
                h => { h.Username("guest"); h.Password("guest"); });
            });
            bus.Start();
            Console.WriteLine("----Nadawca----");

            while (Console.ReadKey().Key != ConsoleKey.Escape)
            {
                for (int i = 0; i < 10; i++)
                {
                    bus.Publish(new Wiadomosc1() { TrescWiadomosci1 = $"Wiadomosc1 nr{i}" },
                        ctx =>
                        {
                            ctx.Headers.Set("header1", $"Michal Kuprianowicz {i}");
                            ctx.Headers.Set("header2", $"184631 {i}");
                        });
                    bus.Publish(new Wiadomosc2() { TrescWiadomosci2 = $"Wiadomosc2 nr{i}" });

                    bus.Publish(new Wiadomosc_1_2() { TrescWiadomosci1 = $"Wiadomosc1 nr{i} version_2", TrescWiadomosci2 = $"Wiadomosc2 nr{i} version_2" },
                        ctx =>
                        {
                            ctx.Headers.Set("header1", $"Michal Kuprianowicz {i} version_2");
                            ctx.Headers.Set("header2", $"184631 {i} version_2");
                        });
                }
            }

            bus.Stop();
        }
    }
}
