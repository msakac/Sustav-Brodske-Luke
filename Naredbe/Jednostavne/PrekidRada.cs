using msakac_zadaca_1.Aplikacija;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using msakac_zadaca_1.Modeli;

namespace msakac_zadaca_1.Naredbe.Jednostavne
{
    public class PrekidRada : AbstractJednostavnaNaredba
    {
        public override void IzvrsiNaredbu()
        {
            // IspisPoruke.Uspjeh("\nPrekidam rad aplikacije!");
            // Environment.Exit(0);

            //Ispis svih stavki rasporeda
            BrodskaLuka brodskaLuka = BrodskaLuka.Instanca();
            IspisPoruke.Greska($"\nIspis {brodskaLuka.listaStavkiRasporeda.Count} stavki rasporeda:");
            string prviRedak = String.Format("|{0,-3}|{1,-3}|{2,-4}|{3,-12}|{4,-5}|{5,-5}|", "Rbr", "Vez", "Brod", "Dani", "V Od", "V Do");
            IspisPoruke.Greska(prviRedak + "\n|---|---|----|------------|-----|-----|");
            List<StavkaRasporeda> sortiranaListaStavkiRasporeda = brodskaLuka.listaStavkiRasporeda.OrderBy(o => o.IdVez).ThenBy(o => o.VrijemeOd).ToList();
            int brojac = 0;
            foreach (StavkaRasporeda stavka in brodskaLuka.listaStavkiRasporeda)
            {
                brojac++;
                string dani = "";
                foreach (DayOfWeek dan in stavka.DaniUTjednu)
                {
                    int indexDana = (int)dan;
                    dani += indexDana + ",";
                }
                string ispis = String.Format("|{0,-3}|{1,-3}|{2,-4}|{3,-12}|{4,-5}|{5,-5}|", brojac + ".", stavka.IdVez, stavka.IdBrod, dani, stavka.VrijemeOd, stavka.VrijemeDo);
                IspisPoruke.Greska(ispis);
            }
            //Nekaj kao V komanda
            List<Rezervacija> rezervacije = Pomagala.DohvatiSveTermineZauzetostiUPeriodu(DateTime.Parse("11.10.2022. 11:43:20"), DateTime.Parse("12.10.2022. 11:43:20"));
            Console.WriteLine("\n\n");
            List<string[]> listaPodatakaZaIspis = new List<string[]>();
            foreach (Rezervacija rezervacija in rezervacije)
            {
                //Dohvaca samo one koji su u vremenskom preklapanju
                // if (Pomagala.PostojiVremenskoPreklapanja(rezervacija.DatumVrijemeOd, rezervacija.DatumVrijemeDo, DateTime.Now, DateTime.Now.AddHours(5)))
                // {
                //     brojac++;
                //     string ispis = String.Format("|{0,-3}|{1,-3}|{2,-3}|{3,-20}|{4,-20}|", brojac + ".", rezervacija.IdVez, rezervacija.IdBrod, rezervacija.DatumVrijemeOd, rezervacija.DatumVrijemeDo);
                //     IspisPoruke.Greska(ispis);
                // }
                brojac++;
                string[] podaciIspisa = { rezervacija.IdVez.ToString(), rezervacija.IdBrod.ToString(), rezervacija.DatumVrijemeOd.ToString(), rezervacija.DatumVrijemeDo.ToString() };
                listaPodatakaZaIspis.Add(podaciIspisa);
                string ispis = String.Format("|{0,-3}|{1,-3}|{2,-3}|{3,-20}|{4,-20}|", brojac + ".", rezervacija.IdVez, rezervacija.IdBrod, rezervacija.DatumVrijemeOd, rezervacija.DatumVrijemeDo);
                IspisPoruke.Greska(ispis);
            }
            //Ispis rezervacija
            brojac = 0;
            IspisPoruke.Uspjeh($"\nIspis {brodskaLuka.listaRezervacija.Count} rezervacija:");

            foreach (Rezervacija rezervacija in brodskaLuka.listaRezervacija)
            {
                brojac++;
                string ispis = String.Format("|{0,-3}|{1,-3}|{2,-3}|{3,-20}|{4,-20}|", brojac + ".", rezervacija.IdVez, rezervacija.IdBrod, rezervacija.DatumVrijemeOd, rezervacija.DatumVrijemeDo);
                IspisPoruke.Uspjeh(ispis);

            }
            string nazivIspisa = "Lista svih rezervacija";
            string[] naziviStupaca = { "Vez", "Brod", "Datum Vrijeme Od", "Datum Vrijeme Do" };
            Tablica.Instanca.IspisiTablicu(nazivIspisa, naziviStupaca, listaPodatakaZaIspis);
        }
    }
}
