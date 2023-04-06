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
                new EndpointAddress("net.pipe://localhost/router"));
            var klient = fabryka.CreateChannel();
            Console.WriteLine(klient.Dodaj(13, 37));
            Console.ReadKey();
            ((IDisposable)klient).Dispose();
            fabryka.Close();
        }
    }
}
