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

        public double ArvutaKulu()
        {
            return (_vahemaa / 100.0) * _kytusekulu * _kutuseHind;
        }

        public double ArvutaVahemaa()
        {
            return _vahemaa;
        }
    }
}
