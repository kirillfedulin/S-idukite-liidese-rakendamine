namespace Soidukid
{
    public class Buss : ISoiduk
    {
        private double _kytusekulu;
        private double _vahemaa;
        private double _kutuseHind;
        private int _reisijaid;

        public Buss(double kytusekulu, double vahemaa, double kutuseHind, int reisijaid)
        {
            _kytusekulu = kytusekulu;
            _vahemaa = vahemaa;
            _kutuseHind = kutuseHind;
            _reisijaid = reisijaid;
        }

        public double ArvutaKulu()
        {
            double koguKulu = (_vahemaa / 100.0) * _kytusekulu * _kutuseHind;
            return koguKulu / _reisijaid;
        }

        public double ArvutaVahemaa() => _vahemaa;

        public override string ToString()
        {
            return $"Buss | Vahemaa: {_vahemaa} km, Kulu/reisija: {ArvutaKulu():F2} €" +
                   $" ({_reisijaid} reisijat, kütusekulu {_kytusekulu} l/100km)";
        }
    }
}
