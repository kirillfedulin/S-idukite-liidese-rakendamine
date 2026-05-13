// Lisapunktid: Klass Elektritõukeratas – elektriline, kulu arvestatakse kWh kaudu
namespace Soidukid
{
    public class ElektriToukeratas : ISoiduk
    {
        private double _vahemaa;        // kilomeetrites
        private double _tarbimine;      // kWh 100 km kohta
        private double _elektrihind;    // €/kWh

        /// <summary>
        /// Loob uue ElektriToukeratas objekti.
        /// </summary>
        /// <param name="vahemaa">Läbitav vahemaa kilomeetrites</param>
        /// <param name="tarbimine">Energiatarbimine kWh / 100 km kohta</param>
        /// <param name="elektrihind">Elektri hind eurodes kWh kohta</param>
        public ElektriToukeratas(double vahemaa, double tarbimine, double elektrihind)
        {
            _vahemaa     = vahemaa;
            _tarbimine   = tarbimine;
            _elektrihind = elektrihind;
        }

        // Kulu = (vahemaa / 100) * tarbimine * elektrihind
        public double ArvutaKulu() => (_vahemaa / 100.0) * _tarbimine * _elektrihind;

        public double ArvutaVahemaa() => _vahemaa;

        public override string ToString()
        {
            return $"Elektritõukeratas | Vahemaa: {_vahemaa} km, Kulu: {ArvutaKulu():F2} €" +
                   $" (tarbimine {_tarbimine} kWh/100km, hind {_elektrihind} €/kWh)";
        }
    }
}
