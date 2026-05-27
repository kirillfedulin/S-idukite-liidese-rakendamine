namespace Soidukid
{
    public class ElektriToukeratas : ISoiduk
    {
        private double _vahemaa;
        private double _tarbimine;
        private double _elektrihind;

        public ElektriToukeratas(double vahemaa, double tarbimine, double elektrihind)
        {
            _vahemaa = vahemaa;
            _tarbimine = tarbimine;
            _elektrihind = elektrihind;
        }

        public double ArvutaKulu() => (_vahemaa / 100.0) * _tarbimine * _elektrihind;

        public double ArvutaVahemaa() => _vahemaa;

        public override string ToString()
        {
            return $"Elektritõukeratas | Vahemaa: {_vahemaa} km, Kulu: {ArvutaKulu():F2} €" +
                   $" (tarbimine {_tarbimine} kWh/100km, hind {_elektrihind} €/kWh)";
        }
    }
}
