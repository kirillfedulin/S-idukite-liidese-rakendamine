// Klass Auto – arvutab kütusekulu ja teepikkuse
namespace Soidukid
{
    public class Auto : ISoiduk
    {
        private double _kytusekulu;   // liitrit 100 km kohta
        private double _vahemaa;      // kilomeetrites
        private double _kutuseHind;   // €/liiter

        /// <summary>
        /// Loob uue Auto objekti.
        /// </summary>
        /// <param name="kytusekulu">Kütusekulu liitrites 100 km kohta</param>
        /// <param name="vahemaa">Läbitav vahemaa kilomeetrites</param>
        /// <param name="kutuseHind">Kütuse hind eurodes liitri kohta</param>
        public Auto(double kytusekulu, double vahemaa, double kutuseHind)
        {
            _kytusekulu = kytusekulu;
            _vahemaa = vahemaa;
            _kutuseHind = kutuseHind;
        }

        // Kulu = (vahemaa / 100) * kütusekulu * hind
        public double ArvutaKulu() => (_vahemaa / 100.0) * _kytusekulu * _kutuseHind;

        public double ArvutaVahemaa() => _vahemaa;

        public override string ToString()
        {
            return $"Auto | Vahemaa: {_vahemaa} km, Kulu: {ArvutaKulu():F2} €" +
                   $" (kütusekulu {_kytusekulu} l/100km, hind {_kutuseHind} €/l)";
        }
    }
}
