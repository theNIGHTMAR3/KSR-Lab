using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Lab5__1
{
    public class Handler : sr1.IZadanie2Callback
    {
        public void Zadanie([MessageParameter(Name = "zadanie")] string zadanie1, int pkt, bool zaliczone)
        {
            Console.WriteLine($"{zadanie1} pkt: {pkt} zaliczone: {zaliczone}");
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            // zad1 
            var client1 = new sr1.Zadanie1Client();
            IAsyncResult result = client1.DlugieObliczeniaAsync();


            for (int x = 0; x < 21; x++)
            {
                Console.WriteLine(client1.Szybciej(x, 3 * x * x - 2 * x));
            }
            Console.ReadKey();

            ((IDisposable)client1).Dispose();

            var client2 = new sr1.Zadanie2Client(new InstanceContext(new Handler()));
            client2.PodajZadania();
            Console.ReadKey();

            ((IDisposable)client2).Dispose();
        }
    }
}


