using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Xml;
using System.IO;

namespace WcfService1
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    [ServiceContract]
    public interface IZadanie3
    {
        [OperationContract, WebGet(UriTemplate = "index.html"), XmlSerializerFormat]
        XmlDocument Serwuj();

        [OperationContract, WebInvoke(UriTemplate = "Dodaj/{a}/{b}")]
        int Dodaj(string a, string b);

        [OperationContract, WebGet(UriTemplate = "scripts.js")]
        Stream GetStream();
    }

    public class Service1 : IZadanie3
    {
        string indexFile = "C:\\Users\\macie\\Desktop\\MojeSemestr6\\KSR\\Lab6\\podejscie1\\index.xhtml";
        string scriptFile = "C:\\Users\\macie\\Desktop\\MojeSemestr6\\KSR\\Lab6\\podejscie1\\scripts.js";
        public XmlDocument Serwuj()
        {
            var xml = new XmlDocument();
            xml.Load(indexFile);
            return xml;
        }

        public int Dodaj(string a, string b)
        {
            return Int32.Parse(a) + Int32.Parse(b);
        }

        public Stream GetStream()
        {
            if (File.Exists(scriptFile))
            {
                return new FileStream(scriptFile, FileMode.Open);
            }
            return null;
        }
    }
}
