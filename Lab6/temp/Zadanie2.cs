using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.ServiceModel.Discovery;
using System.Collections.ObjectModel;

namespace ConsoleApp2
{
    [ServiceContract]
    public interface IZadanie1
    {
        [OperationContract]
        string ScalNapisy(string a, string b);
    }
    class Program
    {
        static void Main(string[] args)
        {
            //Zad2
            var klinetOdkrywca = new DiscoveryClient(
                new UdpDiscoveryEndpoint("soap.udp://localhost:30001"));
            var lst = klinetOdkrywca.Find(new FindCriteria(typeof(IZadanie1))).Endpoints;
            klinetOdkrywca.Close();

            if (lst.Count > 0)
            {
                var adres = lst[0].Address;
                var klient = ChannelFactory<IZadanie1>
                    .CreateChannel(new NetNamedPipeBinding(), adres);
                Console.WriteLine(klient.ScalNapisy("Klient zad2", " dziala"));
                Console.ReadKey();
                ((IDisposable)klient).Dispose();
            }
        }   
    }
}
