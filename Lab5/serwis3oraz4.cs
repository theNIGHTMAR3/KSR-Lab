using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KSR_WCF2;
using System.ServiceModel;

namespace Serwis
{
    public class Zadanie3 : IZadanie3
    {
        public void TestujZwrotny(int nrindeksu)
        {
            var callback = OperationContext.Current.GetCallbackChannel<IZadanie3Zwrotny>();
            for (int x = 0; x < 31; x++)
            {
                callback.WolanieZwrotne(x, x * x * x - x * x);
            }
        }
    }
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession)]
    public class Zadanie4 : IZadanie4
    {
        private int counter;
        public int Dodaj(int v)
        {
            return counter = counter + v;
        }

        public void Ustaw(int v)
        {
            counter = v;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            var host3 = new ServiceHost(typeof(Zadanie3));
            host3.AddServiceEndpoint(
                typeof(IZadanie3),
                new NetNamedPipeBinding(),
                "net.pipe://localhost/ksr-wcf2-zad3-35421");

            var host4 = new ServiceHost(typeof(Zadanie4));
            host4.AddServiceEndpoint(
                typeof(IZadanie4),
                new NetNamedPipeBinding(),
                "net.pipe://localhost/ksr-wcf2-zad4-45421");

            host3.Open();
            host4.Open();
            Console.ReadKey();
            host3.Close();
            host4.Close();
        }
    }
}
