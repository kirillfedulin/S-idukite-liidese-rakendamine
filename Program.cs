using System;
using System.Collections.Generic;
using System.IO;

namespace Soidukid
{
    class Program
    {
        static bool ProoviDouble(string sisend, string nimetus, out double tulemus)
        {
            if (!double.TryParse(sisend.Replace(',', '.'),
                    System.Globalization.NumberStyles.Any,
                    System.Globalization.CultureInfo.InvariantCulture,
                    out tulemus) || tulemus < 0)
            {
                Console.WriteLine($"  ❌ Vigane {nimetus}: '{sisend}'. Sisesta positiivne arv.");
                return false;
            }
            return true;
        }
        static bool ProoviInt(string sisend, string nimetus, out int tulemus)
        {
            if (!int.TryParse(sisend, out tulemus) || tulemus <= 0)
            {
                Console.WriteLine($"  ❌ Vigane {nimetus}: '{sisend}'. Sisesta positiivne täisarv.");
                return false;
            }
            return true;
        }

        static ISoiduk? LooSoidukReast(string rida, int reanr)
        {
            // Ignoreeri tühje ridu ja kommentaariridu (#)
            if (string.IsNullOrWhiteSpace(rida) || rida.TrimStart().StartsWith('#'))
                return null;

            string[] osad = rida.Split(';');
            string tyyyp = osad[0].Trim().ToLower();

            try
            {
                switch (tyyyp)
                {
                    case "auto":
                        // auto;kütusekulu;vahemaa;kütusehind
                        if (osad.Length < 4) throw new FormatException("Auto vajab 3 parameetrit.");
                        double aKulu    = Parse(osad[1]);
                        double aVahemaa = Parse(osad[2]);
                        double aHind    = Parse(osad[3]);
                        return new Auto(aKulu, aVahemaa, aHind);

                    case "jalgratas":
                        // jalgratas;vahemaa
                        if (osad.Length < 2) throw new FormatException("Jalgratas vajab 1 parameetrit.");
                        double jVahemaa = Parse(osad[1]);
                        return new Jalgratas(jVahemaa);

                    case "buss":
                        // buss;kütusekulu;vahemaa;kütusehind;reisijaid
                        if (osad.Length < 5) throw new FormatException("Buss vajab 4 parameetrit.");
                        double bKulu     = Parse(osad[1]);
                        double bVahemaa  = Parse(osad[2]);
                        double bHind     = Parse(osad[3]);
                        int    bReisijad = int.Parse(osad[4].Trim());
                        return new Buss(bKulu, bVahemaa, bHind, bReisijad);

                    case "elektritoukeratas":
                        // elektritoukeratas;vahemaa;tarbimine;elektrihind
                        if (osad.Length < 4) throw new FormatException("Elektritõukeratas vajab 3 parameetrit.");
                        double eVahemaa  = Parse(osad[1]);
                        double eTarb     = Parse(osad[2]);
                        double eHind     = Parse(osad[3]);
                        return new ElektriToukeratas(eVahemaa, eTarb, eHind);

                    default:
                        Console.WriteLine($"  ⚠️  Rida {reanr}: tundmatu sõidukitüüp '{osad[0]}', vahele jäetud.");
                        return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"  ⚠️  Rida {reanr} viga: {ex.Message}, vahele jäetud.");
                return null;
            }
        }

        // Teisendab stringi double-ks (toetab nii koma kui punkti)
        static double Parse(string s) =>
            double.Parse(s.Trim().Replace(',', '.'),
                System.Globalization.CultureInfo.InvariantCulture);

        static ISoiduk? KasitiSisestus()
        {
            Console.WriteLine("\nVali sõiduki tüüp:");
            Console.WriteLine("  1 – Auto");
            Console.WriteLine("  2 – Jalgratas");
            Console.WriteLine("  3 – Buss");
            Console.WriteLine("  4 – Elektritõukeratas");
            Console.Write("Sinu valik: ");
            string valik = Console.ReadLine()?.Trim() ?? "";

            switch (valik)
            {
                case "1": // Auto
                {
                    double kulu, vahemaa, hind;
                    Console.Write("  Kütusekulu (l/100km): ");
                    if (!ProoviDouble(Console.ReadLine() ?? "", "kütusekulu", out kulu)) return null;
                    Console.Write("  Vahemaa (km): ");
                    if (!ProoviDouble(Console.ReadLine() ?? "", "vahemaa", out vahemaa)) return null;
                    Console.Write("  Kütuse hind (€/l): ");
                    if (!ProoviDouble(Console.ReadLine() ?? "", "kütuse hind", out hind)) return null;
                    return new Auto(kulu, vahemaa, hind);
                }
                case "2": // Jalgratas
                {
                    double vahemaa;
                    Console.Write("  Vahemaa (km): ");
                    if (!ProoviDouble(Console.ReadLine() ?? "", "vahemaa", out vahemaa)) return null;
                    return new Jalgratas(vahemaa);
                }
                case "3": // Buss
                {
                    double kulu, vahemaa, hind;
                    int reisijaid;
                    Console.Write("  Kütusekulu (l/100km): ");
                    if (!ProoviDouble(Console.ReadLine() ?? "", "kütusekulu", out kulu)) return null;
                    Console.Write("  Vahemaa (km): ");
                    if (!ProoviDouble(Console.ReadLine() ?? "", "vahemaa", out vahemaa)) return null;
                    Console.Write("  Kütuse hind (€/l): ");
                    if (!ProoviDouble(Console.ReadLine() ?? "", "kütuse hind", out hind)) return null;
                    Console.Write("  Reisijate arv: ");
                    if (!ProoviInt(Console.ReadLine() ?? "", "reisijate arv", out reisijaid)) return null;
                    return new Buss(kulu, vahemaa, hind, reisijaid);
                }
                case "4": // Elektritõukeratas
                {
                    double vahemaa, tarbimine, hind;
                    Console.Write("  Vahemaa (km): ");
                    if (!ProoviDouble(Console.ReadLine() ?? "", "vahemaa", out vahemaa)) return null;
                    Console.Write("  Energiatarbimine (kWh/100km): ");
                    if (!ProoviDouble(Console.ReadLine() ?? "", "tarbimine", out tarbimine)) return null;
                    Console.Write("  Elektri hind (€/kWh): ");
                    if (!ProoviDouble(Console.ReadLine() ?? "", "elektri hind", out hind)) return null;
                    return new ElektriToukeratas(vahemaa, tarbimine, hind);
                }
                default:
                    Console.WriteLine("  ❌ Tundmatu valik.");
                    return null;
            }
        }

        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            List<ISoiduk> soidukid = new List<ISoiduk>();

            Console.WriteLine("     SÕIDUKITE KULUARVUTAJA          ");

            string failTee = "soidukid.txt";
            if (File.Exists(failTee))
            {
                Console.WriteLine($"\n📂 Leitud sisendifail '{failTee}', laen andmed...");
                string[] read = File.ReadAllLines(failTee);
                int loetud = 0;
                for (int i = 0; i < read.Length; i++)
                {
                    ISoiduk? s = LooSoidukReast(read[i], i + 1);
                    if (s != null) { soidukid.Add(s); loetud++; }
                }
                Console.WriteLine($"  ✅ Laaditud {loetud} sõidukit failist.");
            }
            else
            {
                Console.WriteLine($"\n  ℹ️  Faili '{failTee}' ei leitud, jätkan käsitsisisestusega.");
            }

            Console.WriteLine("\n➕ Kas soovid lisada sõidukeid käsitsi? (j/ei)");
            Console.Write("Vastus: ");
            string vastus = Console.ReadLine()?.Trim().ToLower() ?? "ei";

            while (vastus == "j" || vastus == "jah")
            {
                ISoiduk? uus = KasitiSisestus();
                if (uus != null)
                {
                    soidukid.Add(uus);
                    Console.WriteLine("  ✅ Sõiduk lisatud!");
                }

                Console.Write("\nLisa veel üks sõiduk? (j/ei): ");
                vastus = Console.ReadLine()?.Trim().ToLower() ?? "ei";
            }

            if (soidukid.Count == 0)
            {
                Console.WriteLine("\n⚠️  Nimekirjas pole ühtegi sõidukit.");
                return;
            }

            Console.WriteLine(" TULEMUSED");

            double koguKulu    = 0;
            double koguVahemaa = 0;

            for (int i = 0; i < soidukid.Count; i++)
            {
                Console.WriteLine($"\n[{i + 1}] {soidukid[i]}");
                koguKulu    += soidukid[i].ArvutaKulu();
                koguVahemaa += soidukid[i].ArvutaVahemaa();
            }

            Console.WriteLine($" KOGU VAHEMAA : {koguVahemaa:F1} km");
            Console.WriteLine($" KOGU KULU    : {koguKulu:F2} €");
        }
    }
}
