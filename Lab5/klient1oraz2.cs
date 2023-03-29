using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace Klient
{
    public class Handler : Zadanie2.IZadanie2Callback
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
            var client1 = new Zadanie1.Zadanie1Client();
            IAsyncResult result = client1.BeginDlugieObliczenia(null, null);

            for (int x = 0; x < 21; x++)
            {
                Console.WriteLine(client1.Szybciej(x, 3 * x * x - 2 * x));
            }
            Console.WriteLine(client1.EndDlugieObliczenia(result));
            Console.ReadKey();

            ((IDisposable)client1).Dispose();

            var client2 = new Zadanie2.Zadanie2Client(new InstanceContext(new Handler()));
            client2.PodajZadania();
            Console.ReadKey();

            ((IDisposable)client2).Dispose();
        }
    }
}
