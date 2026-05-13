// Klass Jalgratas – ei kuluta kütust, ainult vahemaa
namespace Soidukid
{
    public class Jalgratas : ISoiduk
    {
        private double _vahemaa; // kilomeetrites

        /// <summary>
        /// Loob uue Jalgratas objekti.
        /// </summary>
        /// <param name="vahemaa">Läbitav vahemaa kilomeetrites</param>
        public Jalgratas(double vahemaa)
        {
            _vahemaa = vahemaa;
        }

        // Jalgratas ei kasuta kütust – kulu on 0
        public double ArvutaKulu() => 0.0;

        public double ArvutaVahemaa() => _vahemaa;

        public override string ToString()
        {
            return $"Jalgratas | Vahemaa: {_vahemaa} km, Kulu: 0.00 € (kütusevaba)";
        }
    }
}
