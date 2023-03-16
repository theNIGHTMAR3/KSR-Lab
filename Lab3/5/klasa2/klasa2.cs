using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Reflection;

namespace Klasa2
{
    [
        Guid("F59DA79E-29BB-476C-BFF4-2E9C0ADFDD4D"),
        ComVisible(true),
        InterfaceType(ComInterfaceType.InterfaceIsDual)
    ]
    public interface IKlasa2
    {
        void Test(string napis);
    }


    [
        Guid("F08FB011-E87D-472E-9886-659C2559FB10"),
        ComVisible(true),
        ClassInterface(ClassInterfaceType.None),
        ProgId("KSR20.COM3Klasa.2")
    ]
    public class Class1 : IKlasa2
    {
        public Class1()
        {
        }
        public void Test(string napis)
        {
            Console.WriteLine(napis);
        }
    }
}
