using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var client = new ServiceReference1.Service1Client();

            Console.WriteLine("czekam na odpalenie się uslugi, kliknij aby zaczac");
            Console.ReadLine();

            while (true)
            {

                client.Koduj("test", "Hello World");
                client.Koduj("test2", "qwerty123");
                client.Koduj("test3", "pg.edu.pl");

                Console.WriteLine(client.Pobierz("test"));
                Console.WriteLine(client.Pobierz("test2"));
                Console.WriteLine(client.Pobierz("test3"));

                Console.WriteLine("nacisnij przycisk aby powtorzyc");
                Console.ReadLine();

            }
        }
    }
}
