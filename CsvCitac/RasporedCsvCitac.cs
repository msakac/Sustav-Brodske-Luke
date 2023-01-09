using msakac_zadaca_3.Aplikacija;
using msakac_zadaca_3.Modeli;

namespace msakac_zadaca_3.CsvCitac
{
    public class RasporedCsvCitac : AbstractCsvCitac
    {
        public override void citajPodatke(string datoteka)
        {
            BrodskaLuka brodskaLuka = BrodskaLuka.Instanca();
            brodskaLuka.ispis!.DodajUpis($"Raspored | Učitavam datoteku: {datoteka}...");
            try
            {
                using StreamReader citac = new StreamReader(brodskaLuka.trenutniDirektorij + datoteka);
                string prviRedak = citac.ReadLine()!;
                int brojAtributa = prviRedak.Split(';').Count();
                int brojPropertija = typeof(StavkaRasporeda).GetProperties().Length;
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
                        int idVez = int.Parse(podaci![0]);
                        int idBrod = int.Parse(podaci![1]);
                        string[]? daniTjedna = podaci[2].Split(',');
                        List<DayOfWeek> daniUTjednu = new List<DayOfWeek>();
                        foreach(string dan in daniTjedna){
                            DayOfWeek danTjedna = StavkaRasporeda.dohvatiDanTjedna(dan);
                            daniUTjednu.Add(danTjedna);
                        }
                        TimeOnly vrijemeOd = TimeOnly.Parse(podaci[3]);
                        TimeOnly vrijemeDo = TimeOnly.Parse(podaci[4]);

                        StavkaRasporeda stavkaRasporeda = new StavkaRasporeda(idVez, idBrod, daniUTjednu, vrijemeOd, vrijemeDo);
                        stavkaRasporeda.DodajURaspored();
                        ucitaniPodaci++;
                    }
                    catch (Exception e)
                    {
                        Greska.Instanca.IspisiGresku(e, redak);
                    }

                }
                brodskaLuka.ispis!.DodajUpis($"Raspored | Učitano {ucitaniPodaci.ToString()} ispravnih redaka iz datoteke {datoteka} ");
            }
            catch
            {
                IspisPoruke.FatalnaGreska($"Datoteku {datoteka} nije moguće pročitati ili ne postoji u direktoriju!");
            }
        }
    }
}
