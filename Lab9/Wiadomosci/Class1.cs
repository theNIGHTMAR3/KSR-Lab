using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wiadomosci
{
    public interface IPubl
    {
        int Numer { get; set; }
    }
    public interface IUstaw
    {
        bool Dziala { get; set; }
    }
    public interface IOdp
    {
        string Kto { get; set; }
    }
    public interface IOdpA : IOdp
    {
    }
    public interface IOdpB : IOdp
    {
    }
}

