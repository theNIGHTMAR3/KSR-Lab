using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace Klient6
{
    [ServiceContract]
    public interface IZadanie6
    {
        [OperationContract]
        int Dodaj(int a, int b);
    }
    class Program
    {
        static void Main(string[] args)
        {
            var fabryka = new ChannelFactory<IZadanie6>(
                new NetNamedPipeBinding(),
                new EndpointAddress("net.pipe://localhost/Router"));
            var klient = fabryka.CreateChannel();
            Console.WriteLine(klient.Dodaj(160000, 5407));
            Console.ReadKey();
            ((IDisposable)klient).Dispose();
            fabryka.Close();
        }
    }
}
