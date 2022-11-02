using msakac_zadaca_1.Aplikacija;
using msakac_zadaca_1.Modeli;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace msakac_zadaca_1.CsvCitac
{
    public class LukeCsvCitac : ICsvCitac
    {
        public void citajPodatke(string datoteka)
        {
            Console.WriteLine($"\nLuke | Učitavam datoteku: {datoteka}...");
            var trenutniDirektorij = System.AppContext.BaseDirectory;
            BrodskaLuka brodskaLuka = BrodskaLuka.Instanca();
            try
            {
                using StreamReader citac = new StreamReader(trenutniDirektorij + datoteka);
                string prviRedak = citac.ReadLine()!;
                int brojAtributa = prviRedak.Split(';').Count();
                int brojPropertija = typeof(Luka).GetProperties().Length;
                if (brojAtributa != brojPropertija)
                {
                    IspisPoruke.FatalnaGreska("Broj stupaca u datoteci i atributa u klasi su razliciti!");
                }
                string redak;
                int ucitaniPodaci = 0;
                while ((redak = citac.ReadLine()!) != null)
                {
                    try
                    {
                        string[]? podaci = redak?.Split(';');
                        string naziv = podaci![0];
                        long gps_sirina = long.Parse(podaci[1].Replace(".", ""));
                        long gps_visina = long.Parse(podaci[2].Replace(".", ""));
                        int dubinaLuke = int.Parse(podaci[3]);
                        int ukBrojPutnickihVezova = int.Parse(podaci[4]);
                        int ukBrojPoslovnihVezova = int.Parse(podaci[5]);
                        int ukBrojOstalihVezova = int.Parse(podaci[6]);
                        DateTime virtualnoVrijeme = DateTime.Parse(podaci[7]);

                        Luka luka = new Luka(naziv, gps_sirina, gps_visina, dubinaLuke, ukBrojPutnickihVezova, ukBrojPoslovnihVezova, ukBrojOstalihVezova, virtualnoVrijeme);
                        brodskaLuka.luka = luka;
                        ucitaniPodaci++;
                    }
                    catch (Exception e)
                    {
                        Greska.Instanca.IspisiGresku(e, redak);
                    }

                }
                IspisPoruke.Uspjeh($"|===== Učitano {ucitaniPodaci.ToString()} ispravnih redaka iz datoteke {datoteka} ");
            }
            catch
            {
                IspisPoruke.FatalnaGreska($"Datoteku {datoteka} nije moguće pročitati ili ne postoji u direktoriju!");
            }
        }
    }
}
