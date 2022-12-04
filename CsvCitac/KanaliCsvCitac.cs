using msakac_zadaca_1.Aplikacija;
using msakac_zadaca_1.Modeli;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace msakac_zadaca_1.CsvCitac
{
    public class KanaliCsvCitac : AbstractCsvCitac
    {
        public override void citajPodatke(string datoteka)
        {
            Console.WriteLine($"\nKanali | Učitavam datoteku: {datoteka}...");
            BrodskaLuka brodskaLuka = BrodskaLuka.Instanca();
            try
            {
                using StreamReader citac = new StreamReader(brodskaLuka.trenutniDirektorij + datoteka);
                string prviRedak = citac.ReadLine()!;
                int brojAtributa = prviRedak.Split(';').Count();
                int brojPropertija = typeof(Kanal).GetProperties().Length - 1;
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
                        int frekvecija = int.Parse(podaci![1]);
                        int maksimalanBroj = int.Parse(podaci![2]);
                        Kanal kanal = new Kanal(id, frekvecija, maksimalanBroj);
                        kanal.DodajUListuKanala();
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
