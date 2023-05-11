using System;
using MassTransit;

namespace Komunikaty
{
    public interface ITimeout : CorrelatedBy<Guid>
    {
    }
    public class Timeout : ITimeout
    {
        public Guid CorrelationId { get; set; }
    }
    public interface IStartZamowienia
    {
        string Login { get; set; }
        int Ilosc { get; set; }
    }
    public class StartZamowienia : IStartZamowienia
    {
        public string Login { get; set; }
        public int Ilosc { get; set; }
    }
    public interface IPytanieoPotwierdzenie : CorrelatedBy<Guid>
    {
        string Login { get; set; }
        int Ilosc { get; set; }
    }
    public class PytanieoPotwierdzenie : IPytanieoPotwierdzenie
    {
        public string Login { get; set; }
        public int Ilosc { get; set; }
        public Guid CorrelationId { get; set; }
    }
    public interface IPotwierdzenie : CorrelatedBy<Guid>
    {
    }
    public class Potwierdzenie : IPotwierdzenie
    {
        public Guid CorrelationId { get; set; }
    }
    public interface IBrakPotwierdzenia : CorrelatedBy<Guid>
    {
    }
    public class BrakPotwierdzenia : CorrelatedBy<Guid>
    {
        public Guid CorrelationId { get; set; }
    }
    public interface IPytanieoWolne : CorrelatedBy<Guid>
    {
        int Ilosc { get; set; }
    }
    public class PytanieoWolne : IPytanieoWolne
    {
        public Guid CorrelationId { get; set; }
        public int Ilosc { get; set; }
    }
    public interface IOdpowiedzWolne : CorrelatedBy<Guid>
    {
    }
    public class OdpowiedzWolne : IOdpowiedzWolne
    {
        public Guid CorrelationId { get; set; }
    }
    public interface IOdpowiedzWolneNegatywna : CorrelatedBy<Guid>
    {
    }
    public class OdpowiedzWolneNegatywna : IOdpowiedzWolneNegatywna
    {
        public Guid CorrelationId { get; set; }
    }
    public interface IAkceptacjaZamowienia : CorrelatedBy<Guid>
    {
        string Login { get; set; }
        int Ilosc { get; set; }
    }
    public class AkceptacjaZamowienia : IAkceptacjaZamowienia
    {
        public string Login { get; set; }
        public Guid CorrelationId { get; set; }
        public int Ilosc { get; set; }
    }
    public interface IOdrzucenieZamowienia : CorrelatedBy<Guid>
    {
        string Login { get; set; }
        int Ilosc { get; set; }
    }
    public class OdrzucenieZamowienia : IOdrzucenieZamowienia
    {
        public string Login { get; set; }
        public Guid CorrelationId { get; set; }
        public int Ilosc { get; set; }
    }
}
