using msakac_zadaca_1.Aplikacija;
using msakac_zadaca_1.Modeli;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace msakac_zadaca_1.CsvCitac
{
    public class BrodoviCsvCitac : ICsvCitac
    {
        public void citajPodatke(string datoteka)
        {
            Console.WriteLine($"\nBrodovi | Učitavam datoteku: {datoteka}...");
            var trenutniDirektorij = System.AppContext.BaseDirectory;
            BrodskaLuka brodskaLuka = BrodskaLuka.Instanca();
            try
            {
                using StreamReader citac = new StreamReader(trenutniDirektorij + datoteka);
                string prviRedak = citac.ReadLine()!;
                int brojAtributa = prviRedak.Split(';').Count();
                int brojPropertija = typeof(Brod).GetProperties().Length;
                if (brojAtributa != brojPropertija)
                {
                    IspisPoruke.FatalnaGreska("Broj redaka u datoteci i atributa u klasi su razliciti!");
                }
                string redak;
                int ucitaniPodaci = 0;
                while ((redak = citac.ReadLine()!) != null)
                {
                    try
                    {
                        string[]? podaci = redak?.Split(';');
                        int id = int.Parse(podaci![0]);
                        string oznakaBroda = podaci[1];
                        string naziv = podaci[2];
                        OznakaVrsteBroda vrstaBroda = Brod.dohvatiOznakuVrsteBroda(podaci[3]);
                        double duljina = double.Parse(podaci![4]);
                        double sirina = double.Parse(podaci[5]);
                        double gaz = double.Parse(podaci[6]);
                        double maxBrzina = double.Parse(podaci[7]);
                        int kapPutnika = int.Parse(podaci[8]);
                        int kapVozila = int.Parse(podaci[9]);
                        int kapTereta = int.Parse(podaci[10]);

                        Brod brod = new Brod(id, oznakaBroda, naziv, vrstaBroda, duljina, sirina, gaz, maxBrzina, kapPutnika, kapVozila, kapTereta);
                        brod.DodajUListuBrodova();
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
