using msakac_zadaca_3.Aplikacija;
using msakac_zadaca_3.Modeli;

namespace msakac_zadaca_3.CsvCitac
{
    public class VezoviCsvCitac : AbstractCsvCitac
    {
        public override void citajPodatke(string datoteka)
        {
            BrodskaLuka brodskaLuka = BrodskaLuka.Instanca();
            brodskaLuka.ispis!.DodajUpis($"Vezovi | Učitavam datoteku: {datoteka}...");
            try
            {
                using StreamReader citac = new StreamReader(brodskaLuka.trenutniDirektorij + datoteka);
                string prviRedak = citac.ReadLine()!;
                int brojAtributa = prviRedak.Split(';').Count();
                int brojPropertija = typeof(Vez).GetProperties().Length - 1;
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
                        string oznakaVeza = podaci[1];
                        VrstaVeza vrstaVeze = brodskaLuka.DohvatiVrstuVeza(podaci[2]);
                        int cijenaVezaPoSatu = int.Parse(podaci![3]);
                        int maxDuljina = int.Parse(podaci[4]);
                        int maxSirina = int.Parse(podaci[5]);
                        int maxDubina = int.Parse(podaci[6]);

                        Vez vez = new Vez(id, oznakaVeza, vrstaVeze, cijenaVezaPoSatu, maxDuljina, maxSirina, maxDubina);
                        vez.DodajUListuVezova();
                        ucitaniPodaci++;
                    }
                    catch (Exception e)
                    {
                        Greska.Instanca.IspisiGresku(e, redak);
                    }

                }
                brodskaLuka.ispis!.DodajUpis($"Vezovi | Učitano {ucitaniPodaci.ToString()} ispravnih redaka iz datoteke {datoteka} ");
            }
            catch
            {
                IspisPoruke.FatalnaGreska($"Datoteku {datoteka} nije moguće pročitati ili ne postoji u direktoriju!");
            }
        }
    }
}
