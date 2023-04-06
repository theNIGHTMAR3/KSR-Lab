using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.ServiceModel.Routing;
using System.ServiceModel.Dispatcher;

namespace Router6
{
    class Program
    {
        static void Main(string[] args)
        {
            var routePath1 = "net.pipe://localhost/zad6_1";
            var routePath2 = "net.pipe://localhost/zad6_2";
            var routeAdres = "net.pipe://localhost/router";

            var host = new ServiceHost(typeof(RoutingService));
            host.AddServiceEndpoint(
                typeof(IRequestReplyRouter),
                new NetNamedPipeBinding(),
                routeAdres);

            var routeConfig = new RoutingConfiguration();
            var contract = ContractDescription.GetContract(typeof(IRequestReplyRouter));
            var klient1 = new ServiceEndpoint(
                contract,
                new NetNamedPipeBinding(),
                new EndpointAddress(routePath1));
            var klient2 = new ServiceEndpoint(
                contract,
                new NetNamedPipeBinding(),
                new EndpointAddress(routePath2));

            var list = new List<ServiceEndpoint>();
            list.Add(klient1);
            list.Add(klient2);

            routeConfig.FilterTable.Add(new MatchAllMessageFilter(), list);
            host.Description.Behaviors.Add(new RoutingBehavior(routeConfig));

            host.Open();
            Console.ReadKey();
            host.Close();
        }
    }
}
