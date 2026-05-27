namespace Soidukid
{
    public class Jalgratas : ISoiduk
    {
        private double _vahemaa;

        public Jalgratas(double vahemaa)
        {
            _vahemaa = vahemaa;
        }

        public double ArvutaKulu()
        {
            return 0.0;
        }

        public double ArvutaVahemaa()
        {
            return _vahemaa;
        }
    }
}
