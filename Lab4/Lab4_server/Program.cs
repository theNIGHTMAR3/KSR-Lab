using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel
using KSR_WCF1;
using System.Runtime.Serialization;

namespace Lab4_server
{
    internal class Program
    {
        static void Main(string[] args)
        {

            var host = new ServiceHost(typeof(Zadanie2));

            host.AddServiceEndpoint((typeof(IZadanie2)), 
                new NetNamedPipeBinding(),
                "net.pipe://localhost/ksr-wcf1-zad2");

            // zad3
            var b = host.Description.Behaviors.Find<ServiceMetadataBehavior>();
            if (b == null)
                b = new ServiceMetadataBehavior();
            host.Description.Behaviors.Add(b);
            host.AddServiceEndpoint(ServiceMetadataBehavior.MexContractName,
                MetadataExchangeBindings.CreateMexNamedPipeBinding(),
                "net.pipe://localhost/metadata");

            // zad4
            host.AddServiceEndpoint(typeof(IZadanie2),
                new NetTcpBinding(),
                "nwt.tcp://127.0.0.1:55765");

            // zad7
            var host7=new ServiceHost(typeof(Zadanie7));
            host7.AddServiceEndpoint(typeof(IZadanie7),
                new NetNamedPipeBinding(),
                "net.pipe://localhost/metadata2");

            var b7=host7.Description.Behaviors. Find<ServiceMetadataBehavior>();
            if (b7 == null)
                b7 = new ServiceMetadataBehavior();

            host.Description.Behaviors.Add(b7);
            host.AddServiceEndpoint(ServiceMetadataBehavior.MexContractName,
                MetadataExchangeBindings.CreateMexNamedPipeBinding(),
                "net.pipe://localhost/metadata2");



            host.Open();

            Console.ReadKey();

            host.Close();

            Console.ReadLine();

        }
    }

    // zad2
    [ServiceContract] public interface IZadanie2
    {
        [OperationContract] string Test(string arg);
    }


    public class Zadanie2 : IZadanie2
    {
        public string Test(string arg)
        {
            return $"You are testing with argument: {arg}";
        }
    }


    // zad7
    [ServiceContract]
    public interface IZadanie7
    {
        [OperationContract][FaultContract(typeof(Wyjatek7))]

        void RzucWyjatek7(string a, int b);
    }


    [DataContract]
    public class Wyjatek7
    {
        [DataMember] public string Opis { get; set; }
        [DataMember] public string A { get; set; }
        [DataMember] public int B { get; set; }
    }


    public class Zadanie7 : IZadanie7
    {
        public void RzucWyjatek7(string a, int b)
        {
            var exception = new FaultException<Wyjatek7>(new Wyjatek7(),
                new FaultReason("Wyrzucono wyjatek7 " + a + b));

            throw exception;
        }
    }
}
