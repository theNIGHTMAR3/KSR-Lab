using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MassTransit;
using Wiadomosci;
using MassTransit.Serialization;
using GreenPipes;

namespace Kontroler
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
    class Ustaw : IUstaw
    {
        public bool Dziala { get; set; }
    }
    class Program
    {
        public static ConsoleKey MyReadKey(ref ConsoleKey key)
		{
            key = Console.ReadKey().Key;
            return key;
		}
        static void Main(string[] args)
        {
            var bus = Bus.Factory.CreateUsingRabbitMq(sbc =>
            {
                sbc.Host(new Uri("rabbitmq://localhost/103057"),
                h => { h.Username("guest"); h.Password("guest"); });

                sbc.UseEncryptedSerializer(
                    new AesCryptoStreamProvider(
                        new Dostawca("18463118463118463118463118463118"),
                           "1846311846311846"));
            });

            bus.Start();
            Console.WriteLine("----Kontroler----");

            bool stan = true;
            var tsk = bus.GetSendEndpoint(new Uri("rabbitmq://localhost/103057/switcher"));
            tsk.Wait();
            var sendEp = tsk.Result;

            ConsoleKey key=ConsoleKey.W;

            while (MyReadKey(ref key) != ConsoleKey.Escape)
            {

                if (key == ConsoleKey.S && stan==false)
                {
                    stan = !stan;
                    sendEp.Send(new Ustaw() { Dziala = stan },
                    ctx => { ctx.Headers.Set(EncryptedMessageSerializer.EncryptionKeyHeader, Guid.NewGuid().ToString()); });
                    Console.WriteLine($"Zmiana stanu na {(stan ? "ON" : "OFF")}");
                    
                }
                if (key == ConsoleKey.T && stan==true)
                {
                    stan = !stan;
                    sendEp.Send(new Ustaw() { Dziala = stan },
                    ctx => { ctx.Headers.Set(EncryptedMessageSerializer.EncryptionKeyHeader, Guid.NewGuid().ToString()); });
                    Console.WriteLine($"Zmiana stanu generatora na {(stan ? "ON" : "OFF")}");
                }    
            }

            bus.Stop();
        }
    }
}
