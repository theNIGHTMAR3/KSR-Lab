using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MassTransit.Serialization;
using Wiadomosci;
using GreenPipes;
using MassTransit;

namespace Wydawca
{


	class Klucz : SymmetricKey
    {
        public byte[] Key { get; set; }
        public byte[] IV { get; set; }
    }
    public class Dostawca : ISymmetricKeyProvider
    {
        private string k;
        public Dostawca(string _k)
        { k = _k; }
        public bool TryGetKey(string keyId, out SymmetricKey key)
        {
            var sk = new Klucz();
            sk.IV = Encoding.ASCII.GetBytes(keyId.Substring(0, 16));
            sk.Key = Encoding.ASCII.GetBytes(k); key = sk;
            return true;
        }
    }
    class Switcher : IConsumer<IUstaw>
    {
        public bool stan { get; private set; } = true;

        public Task Consume(ConsumeContext<IUstaw> context)
        {
            stan = context.Message.Dziala;
            return Task.Run(() =>
            {
                Console.WriteLine($"Kontroler: {(stan ? "ON" : "OFF")}");
            });
        }
    }
    class Handler : IConsumer<IOdpA>, IConsumer<IOdpB>
    {
        public Task Consume(ConsumeContext<IOdpA> context)
        {
            return Task.Run(() =>
            {
                var czyRzucacWyjatek = new Random();
                if (czyRzucacWyjatek.Next(0, 100) < 33)
                    throw new Exception();
                Console.WriteLine($"Otrzymano odpowiedz od: {context.Message.Kto}");
            });
        }

        public Task Consume(ConsumeContext<IOdpB> context)
        {
            return Task.Run(() =>
            {
                var czyRzucacWyjatek = new Random();
                if (czyRzucacWyjatek.Next(0, 100) < 33)
                    throw new Exception();
                Console.WriteLine($"Otrzymano odpowiedz od: {context.Message.Kto}");
            });
        }
    }

    class PublishObserver : IPublishObserver
    {
        public int counter = 0;
        public Task PostPublish<T>(PublishContext<T> context) where T : class
        {
            return Task.Run(() => { counter++; });
        }

        public Task PrePublish<T>(PublishContext<T> context) where T : class
        {
            return Task.Run(() => { });
        }

        public Task PublishFault<T>(PublishContext<T> context, Exception exception) where T : class
        {
            return Task.Run(() => { });
        }
    }

    class ReceiveObserver : IReceiveObserver
    {
        public int counter = 0;
        public int postCount = 0;
        public int postConsumer = 0;
        public Task ConsumeFault<T>(ConsumeContext<T> context, TimeSpan duration, string consumerType, Exception exception) where T : class
        {
            return Task.Run(() => { });
        }

        public Task PostConsume<T>(ConsumeContext<T> context, TimeSpan duration, string consumerType) where T : class
        {
            return Task.Run(() => { postConsumer++; });
        }

        public Task PostReceive(ReceiveContext context)
        {
            return Task.Run(() => { postCount++; });
        }

        public Task PreReceive(ReceiveContext context)
        {
            return Task.Run(() => { counter++; });
        }

        public Task ReceiveFault(ReceiveContext context, Exception exception)
        {
            return Task.Run(() => { });
        }
    }

    class Publ : IPubl
    {
        public int Numer { get; set; }
    }
    class Wydawca
    {
        static void Main(string[] args)
        {
            var switcher = new Switcher();
            var handler = new Handler();

            var receiveObserver = new ReceiveObserver();
            var publishObserver = new PublishObserver();

            var busSwitcher = Bus.Factory.CreateUsingRabbitMq(sbc =>
            {
                sbc.Host(new Uri("rabbitmq://localhost/103057"),
                h => { h.Username("guest"); h.Password("guest"); });

                sbc.UseEncryptedSerializer(
                       new AesCryptoStreamProvider(
                           new Dostawca("18463118463118463118463118463118"),
                           "1846311846311846"));

                sbc.ReceiveEndpoint("switcher", ep => {
                    ep.Instance(switcher);
                });
            });

            var bus = Bus.Factory.CreateUsingRabbitMq(sbc =>
            {
                sbc.Host(new Uri("rabbitmq://localhost/103057"),
                h => { h.Username("guest"); h.Password("guest"); });

                sbc.ReceiveEndpoint( "handler", ep => {
                    ep.Instance(handler);
                    ep.UseRetry(r => { r.Immediate(2); });
                });
            });
            bus.ConnectReceiveObserver(receiveObserver);
            bus.ConnectPublishObserver(publishObserver);



            int count = 1;
            bool wylacz = false;
            bus.Start();
            busSwitcher.Start();
            Console.WriteLine("----Wydawca----");

            Task.Run(() =>
            {
                while (!wylacz)
                {
                    while (switcher.stan)
                    {
                        bus.Publish(new Publ() { Numer = count });
                        Console.WriteLine($"Publ_{count}");
                        count += 1;
                        System.Threading.Thread.Sleep(1000);
                    }
                }
            });

            ConsoleKey key;
            while ((key = Console.ReadKey().Key) != ConsoleKey.Escape)
            {
                if (key == ConsoleKey.S)
                {
                    Console.WriteLine($"\nStatyskytki: ");
					Console.WriteLine($"Proby obsluzenia komunikatow kazdego typu: {receiveObserver.counter}");
					Console.WriteLine($"Pomyslne proby obsluzenia komunikatow kazdego typu: {receiveObserver.postCount}");
					Console.WriteLine($"Opublikowane komunikaty: {publishObserver.counter}\n");
                }
            }

            busSwitcher.Stop();
            bus.Stop();
        }
    }
}
