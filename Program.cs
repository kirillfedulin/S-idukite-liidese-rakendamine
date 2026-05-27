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
                Console.WriteLine($"Vigane {nimetus}: '{sisend}'. Sisesta positiivne arv.");
                return false;
            }
            return true;
        }

        static bool ProoviInt(string sisend, string nimetus, out int tulemus)
        {
            if (!int.TryParse(sisend, out tulemus) || tulemus <= 0)
            {
                Console.WriteLine($"Vigane {nimetus}: '{sisend}'. Sisesta positiivne täisarv.");
                return false;
            }
            return true;
        }

        static ISoiduk? LooSoidukReast(string rida, int reanr)
        {
            if (string.IsNullOrWhiteSpace(rida) || rida.TrimStart().StartsWith('#'))
                return null;

            string[] osad = rida.Split(';');
            string tyyyp = osad[0].Trim().ToLower();

            try
            {
                switch (tyyyp)
                {
                    case "auto":
                        if (osad.Length < 4) throw new FormatException("Auto vajab 3 parameetrit.");
                        return new Auto(Parse(osad[1]), Parse(osad[2]), Parse(osad[3]));

                    case "jalgratas":
                        if (osad.Length < 2) throw new FormatException("Jalgratas vajab 1 parameetrit.");
                        return new Jalgratas(Parse(osad[1]));

                    case "buss":
                        if (osad.Length < 5) throw new FormatException("Buss vajab 4 parameetrit.");
                        return new Buss(Parse(osad[1]), Parse(osad[2]), Parse(osad[3]), int.Parse(osad[4].Trim()));

                    case "elektritoukeratas":
                        if (osad.Length < 4) throw new FormatException("Elektritõukeratas vajab 3 parameetrit.");
                        return new ElektriToukeratas(Parse(osad[1]), Parse(osad[2]), Parse(osad[3]));

                    default:
                        Console.WriteLine($"Rida {reanr}: tundmatu sõidukitüüp '{osad[0]}', vahele jäetud.");
                        return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Rida {reanr} viga: {ex.Message}, vahele jäetud.");
                return null;
            }
        }

        static double Parse(string s) =>
            double.Parse(s.Trim().Replace(',', '.'), System.Globalization.CultureInfo.InvariantCulture);

        static ISoiduk? KasitiSisestus()
        {
            Console.WriteLine("\nVali sõiduki tüüp:");
            Console.WriteLine("1 – Auto");
            Console.WriteLine("2 – Jalgratas");
            Console.WriteLine("3 – Buss");
            Console.WriteLine("4 – Elektritõukeratas");
            Console.Write("Sinu valik: ");
            string valik = Console.ReadLine()?.Trim() ?? "";

            switch (valik)
            {
                case "1":
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
                case "2":
                    {
                        double vahemaa;
                        Console.Write("Vahemaa (km): ");
                        if (!ProoviDouble(Console.ReadLine() ?? "", "vahemaa", out vahemaa)) return null;
                        return new Jalgratas(vahemaa);
                    }
                case "3":
                    {
                        double kulu, vahemaa, hind;
                        int reisijaid;
                        Console.Write("Kütusekulu (l/100km): ");
                        if (!ProoviDouble(Console.ReadLine() ?? "", "kütusekulu", out kulu)) return null;
                        Console.Write("Vahemaa (km): ");
                        if (!ProoviDouble(Console.ReadLine() ?? "", "vahemaa", out vahemaa)) return null;
                        Console.Write("Kütuse hind (€/l): ");
                        if (!ProoviDouble(Console.ReadLine() ?? "", "kütuse hind", out hind)) return null;
                        Console.Write("Reisijate arv: ");
                        if (!ProoviInt(Console.ReadLine() ?? "", "reisijate arv", out reisijaid)) return null;
                        return new Buss(kulu, vahemaa, hind, reisijaid);
                    }
                case "4":
                    {
                        double vahemaa, tarbimine, hind;
                        Console.Write("Vahemaa (km): ");
                        if (!ProoviDouble(Console.ReadLine() ?? "", "vahemaa", out vahemaa)) return null;
                        Console.Write("Energiatarbimine (kWh/100km): ");
                        if (!ProoviDouble(Console.ReadLine() ?? "", "tarbimine", out tarbimine)) return null;
                        Console.Write("Elektri hind (€/kWh): ");
                        if (!ProoviDouble(Console.ReadLine() ?? "", "elektri hind", out hind)) return null;
                        return new ElektriToukeratas(vahemaa, tarbimine, hind);
                    }
                default:
                    Console.WriteLine("Tundmatu valik.");
                    return null;
            }
        }

        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            List<ISoiduk> soidukid = new List<ISoiduk>();

            Console.WriteLine("SÕIDUKITE KULUARVUTAJA");

            string failTee = "soidukid.txt";
            if (File.Exists(failTee))
            {
                Console.WriteLine($"\nLeitud sisendifail '{failTee}', laen andmed...");
                string[] read = File.ReadAllLines(failTee);
                int loetud = 0;
                for (int i = 0; i < read.Length; i++)
                {
                    ISoiduk? s = LooSoidukReast(read[i], i + 1);
                    if (s != null) { soidukid.Add(s); loetud++; }
                }
                Console.WriteLine($"Laaditud {loetud} sõidukit failist.");
            }
            else
            {
                Console.WriteLine($"\nFaili '{failTee}' ei leitud, jätkan käsitsisisestusega.");
            }

            Console.WriteLine("\nKas soovid lisada sõidukeid käsitsi? (j/ei)");
            Console.Write("Vastus: ");
            string vastus = Console.ReadLine()?.Trim().ToLower() ?? "ei";

            while (vastus == "j" || vastus == "jah")
            {
                ISoiduk? uus = KasitiSisestus();
                if (uus != null)
                {
                    soidukid.Add(uus);
                    Console.WriteLine("Sõiduk lisatud!");
                }
                Console.Write("\nLisa veel üks sõiduk? (j/ei): ");
                vastus = Console.ReadLine()?.Trim().ToLower() ?? "ei";
            }

            if (soidukid.Count == 0)
            {
                Console.WriteLine("\nNimekirjas pole ühtegi sõidukit.");
                return;
            }

            Console.WriteLine("TULEMUSED");

            double koguKulu = 0;
            double koguVahemaa = 0;

            for (int i = 0; i < soidukid.Count; i++)
            {
                Console.WriteLine($"\n[{i + 1}] {soidukid[i]}");
                koguKulu += soidukid[i].ArvutaKulu();
                koguVahemaa += soidukid[i].ArvutaVahemaa();
            }

            Console.WriteLine($" KOGU VAHEMAA : {koguVahemaa:F1} km");
            Console.WriteLine($" KOGU KULU    : {koguKulu:F2} €");
        }
    }
}
