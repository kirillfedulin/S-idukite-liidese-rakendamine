namespace Soidukid
{
    public class Jalgratas : ISoiduk
    {
        private double _vahemaa;

        public Jalgratas(double vahemaa)
        {
            _vahemaa = vahemaa;
        }

        public double ArvutaKulu() => 0.0;

        public double ArvutaVahemaa() => _vahemaa;

        public override string ToString()
        {
            return $"Jalgratas | Vahemaa: {_vahemaa} km, Kulu: 0.00 € (kütusevaba)";
        }
    }
}
