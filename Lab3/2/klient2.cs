using System;


namespace KSRKlient2
{
    class Program
    {
        public static void Main(string[] args)
        {
            string progId = "KSR20.COM3Klasa.1";
            string methodName = "Test";

            Type type = Type.GetTypeFromProgID(progId);
            if (type != null)
            {
                try
                {
                    object act = Activator.CreateInstance(type);
                    type.InvokeMember(methodName, System.Reflection.BindingFlags.InvokeMethod, null, act, new object[] { "Klasa dzia³a w C#" });
                }
                catch
                {
                    Console.WriteLine("cos poszlo nie tak");
                }
            }
            else
            {
                Console.WriteLine("nie pobrano typu");
            }
        }
    }
} 
