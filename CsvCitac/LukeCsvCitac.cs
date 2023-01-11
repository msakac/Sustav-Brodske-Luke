using msakac_zadaca_3.Aplikacija;
using msakac_zadaca_3.Modeli;

namespace msakac_zadaca_3.CsvCitac
{
    public class LukeCsvCitac : AbstractCsvCitac
    {
        public override void citajPodatke(string datoteka)
        {
            BrodskaLuka brodskaLuka = BrodskaLuka.Instanca();
            brodskaLuka.ispis!.DodajUpis($"Luke | Učitavam datoteku: {datoteka}...");
            try
            {
                using StreamReader citac = new StreamReader(brodskaLuka.trenutniDirektorij + datoteka);
                string prviRedak = citac.ReadLine()!;
                int brojAtributa = prviRedak.Split(';').Count();
                int brojPropertija = typeof(Luka).GetProperties().Length;
                if (brojAtributa != brojPropertija)
                {
                    brodskaLuka.ispis!.DodajGresku("Broj stupaca u datoteci i atributa u klasi su razliciti!");
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
                brodskaLuka.ispis!.DodajUpis($"Luke | Učitano {ucitaniPodaci.ToString()} ispravnih redaka iz datoteke {datoteka} ");
            }
            catch
            {
                brodskaLuka.ispis!.DodajGresku($"Datoteku {datoteka} nije moguće pročitati ili ne postoji u direktoriju!");
            }
        }
    }
}
