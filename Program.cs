using System;
using System.Collections.Generic;

namespace Soidukid
{
    class Program
    {
        static void Main(string[] args)
        {
            List<ISoiduk> soidukid = new List<ISoiduk>();

            Console.WriteLine("Mitu sõidukit soovid lisada?");
            int arv = int.Parse(Console.ReadLine());

            for (int i = 0; i < arv; i++)
            {
                Console.WriteLine("\nVali sõiduki tüüp:");
                Console.WriteLine("1 - Auto");
                Console.WriteLine("2 - Jalgratas");
                Console.WriteLine("3 - Buss");
                Console.Write("Valik: ");
                string valik = Console.ReadLine();

                if (valik == "1")
                {
                    Console.Write("Kütusekulu (l/100km): ");
                    double kulu = double.Parse(Console.ReadLine());
                    Console.Write("Vahemaa (km): ");
                    double vahemaa = double.Parse(Console.ReadLine());
                    Console.Write("Kütuse hind (€/l): ");
                    double hind = double.Parse(Console.ReadLine());
                    soidukid.Add(new Auto(kulu, vahemaa, hind));
                }
                else if (valik == "2")
                {
                    Console.Write("Vahemaa (km): ");
                    double vahemaa = double.Parse(Console.ReadLine());
                    soidukid.Add(new Jalgratas(vahemaa));
                }
                else if (valik == "3")
                {
                    Console.Write("Kütusekulu (l/100km): ");
                    double kulu = double.Parse(Console.ReadLine());
                    Console.Write("Vahemaa (km): ");
                    double vahemaa = double.Parse(Console.ReadLine());
                    Console.Write("Kütuse hind (€/l): ");
                    double hind = double.Parse(Console.ReadLine());
                    Console.Write("Reisijate arv: ");
                    int reisijaid = int.Parse(Console.ReadLine());
                    soidukid.Add(new Buss(kulu, vahemaa, hind, reisijaid));
                }
            }

            Console.WriteLine("\nTulemused:");
            for (int i = 0; i < soidukid.Count; i++)
            {
                Console.WriteLine($"Sõiduk {i + 1}: Kulu = {soidukid[i].ArvutaKulu():F2} €, Vahemaa = {soidukid[i].ArvutaVahemaa()} km");
            }
        }
    }
}
