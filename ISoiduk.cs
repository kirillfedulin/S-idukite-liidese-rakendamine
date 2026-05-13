// Liides ISõiduk – kõik sõidukiklassid peavad rakendama need kaks meetodit
namespace Soidukid
{
    public interface ISoiduk
    {
        /// <summary>Tagastab sõiduki kasutamise kulu eurodes.</summary>
        double ArvutaKulu();

        /// <summary>Tagastab sõiduki läbitud vahemaa kilomeetrites.</summary>
        double ArvutaVahemaa();
    }
}
