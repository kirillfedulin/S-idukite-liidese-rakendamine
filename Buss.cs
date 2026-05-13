// Klass Buss – kulu ja vahemaa, kulu jagatakse reisijate vahel
namespace Soidukid
{
    public class Buss : ISoiduk
    {
        private double _kytusekulu;    // liitrit 100 km kohta
        private double _vahemaa;       // kilomeetrites
        private double _kutuseHind;    // €/liiter
        private int    _reisijaid;     // reisijate arv

        /// <summary>
        /// Loob uue Buss objekti.
        /// </summary>
        /// <param name="kytusekulu">Kütusekulu liitrites 100 km kohta</param>
        /// <param name="vahemaa">Läbitav vahemaa kilomeetrites</param>
        /// <param name="kutuseHind">Kütuse hind eurodes liitri kohta</param>
        /// <param name="reisijaid">Reisijate arv bussis</param>
        public Buss(double kytusekulu, double vahemaa, double kutuseHind, int reisijaid)
        {
            _kytusekulu = kytusekulu;
            _vahemaa    = vahemaa;
            _kutuseHind = kutuseHind;
            _reisijaid  = reisijaid;
        }

        // Kogu kulu jagatud reisijate arvuga
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
