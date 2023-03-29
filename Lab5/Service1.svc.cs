using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using KSR_WCF2;

namespace WCFService
{
    public class Service1 : IZadanie5, IZadanie6
    {
        public string ScalNapisy(string a, string b)
        {
            return a + b;
        }
        public void Dodaj(int a, int b)
        {
            var callback = OperationContext.Current.GetCallbackChannel<IZadanie6Zwrotny>();
            callback.Wynik(a + b);
        }
    }
}
