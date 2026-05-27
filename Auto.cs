namespace Soidukid
{
    public class Auto : ISoiduk
    {
        private double _kytusekulu;
        private double _vahemaa;
        private double _kutuseHind;

        public Auto(double kytusekulu, double vahemaa, double kutuseHind)
        {
            _kytusekulu = kytusekulu;
            _vahemaa = vahemaa;
            _kutuseHind = kutuseHind;
        }

        public double ArvutaKulu() => (_vahemaa / 100.0) * _kytusekulu * _kutuseHind;

        public double ArvutaVahemaa() => _vahemaa;

        public override string ToString()
        {
            return $"Auto | Vahemaa: {_vahemaa} km, Kulu: {ArvutaKulu():F2} €" +
                   $" (kütusekulu {_kytusekulu} l/100km, hind {_kutuseHind} €/l)";
        }
    }
}
