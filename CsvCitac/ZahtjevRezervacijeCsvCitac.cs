using msakac_zadaca_3.Aplikacija;
using msakac_zadaca_3.Modeli;

namespace msakac_zadaca_3.CsvCitac
{
    public class ZahtjevRezervacijeCsvCitac : AbstractCsvCitac
    {
        public override void citajPodatke(string datoteka)
        {
            BrodskaLuka brodskaLuka = BrodskaLuka.Instanca();
            brodskaLuka.ispis!.DodajUpis($"Zahtjevi Rezervacije | Učitavam datoteku: {datoteka}...");
            try
            {
                using StreamReader citac = new StreamReader(brodskaLuka.trenutniDirektorij + datoteka);
                string prviRedak = citac.ReadLine()!;
                int brojAtributa = prviRedak.Split(';').Count();
                int brojPropertija = typeof(ZahtjevRezervacije).GetProperties().Length;
                if (brojAtributa != brojPropertija)
                {
                    brodskaLuka.ispis!.DodajGresku("Broj redaka u datoteci i atributa u klasi su razliciti!");
                }
                string redak;
                int ucitaniPodaci = 0;
                while ((redak = citac.ReadLine()!) != null)
                {
                    try
                    {
                        string[]? podaci = redak?.Split(';');
                        int idBroda = int.Parse(podaci![0]);
                        DateTime datumVrijemeOd = DateTime.Parse(podaci[1]);
                        int trajanjePrivezaUSatima = int.Parse(podaci[2]);

                        ZahtjevRezervacije zahtjevRezervacije = new ZahtjevRezervacije(idBroda, datumVrijemeOd, trajanjePrivezaUSatima);
                        zahtjevRezervacije.DodajUListuZahtjevaRezervacije();
                        ucitaniPodaci++;
                    }
                    catch (Exception e)
                    {
                        Greska.Instanca.IspisiGresku(e, redak);
                    }

                }
                brodskaLuka.ispis!.DodajUpis($"Zahtjevi Rezervacije | Učitano {ucitaniPodaci.ToString()} ispravnih redaka iz datoteke {datoteka} ");
            }
            catch
            {
                brodskaLuka.ispis!.DodajGresku($"Datoteku {datoteka} nije moguće pročitati ili ne postoji u direktoriju!");
            }
        }
    }
}
