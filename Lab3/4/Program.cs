﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using KlasaLib;

namespace Klient_do_C
{
    class Program
    {
        static void Main(string[] args)
        {
            IKlasa klasac = new Klasa();
            klasac.Test("klasac dziala w c#");
        }
    }
}