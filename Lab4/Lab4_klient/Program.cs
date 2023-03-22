using KSR_WCF1;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Lab4
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // zad1
            var fact = new ChannelFactory<IZadanie1>(new NetNamedPipeBinding(),
                new EndpointAddress("net.pipe://localhost/ksr-wcf1-test"));

            var client = fact.CreateChannel();

            Console.WriteLine(client.Test("Testing 184631"));
            try
            {
                client.RzucWyjatek(true);
            }
            catch(FaultException<Wyjatek> e)
            {
                Console.WriteLine(client.OtoMagia(e.Detail.magia));
            }

            // zad7
            var client7 = new ServiceReference2.Zadanie7Client();
            try
            {
                client7.RzucWyjatek7("a",7);
            }
            catch (FaultException<Wyjatek> e)
            {
                Console.WriteLine("Wyjatek7");
            }



            ((IDisposable)client).Dispose();
            fact.Close();
            Console.ReadKey();
        }
    }
}
