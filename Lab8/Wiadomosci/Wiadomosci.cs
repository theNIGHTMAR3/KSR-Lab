
namespace Wiadomosci
{
    public interface IWiadomosc1
    {
        string TrescWiadomosci1 { get; set; }
    }

    public interface IWiadomosc2
    {
        string TrescWiadomosci2 { get; set; }
    }

    public interface IWiadomosc_1_2 : IWiadomosc1, IWiadomosc2
    {
    }
}
