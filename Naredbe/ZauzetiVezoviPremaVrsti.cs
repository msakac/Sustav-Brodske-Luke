using msakac_zadaca_2.Visitor;
using msakac_zadaca_2.Modeli;
using msakac_zadaca_2.Aplikacija;

namespace msakac_zadaca_2.Naredbe
{
    public class ZauzetiVezoviPremaVrsti : AbstractNaredba
    {
        public override void IzvrsiNaredbu(string naredba)
        {
            string[]? argumenti = naredba.Split(' ');
            string argumentDatumVrijeme = argumenti[1] + " " + argumenti[2];
            DateTime datumVrijemeProvjere = DateTime.Parse(argumentDatumVrijeme);

            List<Rezervacija> listaSvihRezervacijaUPeriodu = Pomagala.DohvatiSveTermineZauzetostiUPeriodu(datumVrijemeProvjere.AddDays(-1), datumVrijemeProvjere.AddDays(1));

            Rezervacije rezervacije = new Rezervacije();
            foreach (Rezervacija r in listaSvihRezervacijaUPeriodu)
            {
                rezervacije.DodajRezervaciju(r);
            }
            List<Vez> putnicki = rezervacije.Prihvati(new PutnickiVisitor(datumVrijemeProvjere));
            List<Vez> poslovni = rezervacije.Prihvati(new PoslovniVisitor(datumVrijemeProvjere));
            List<Vez> ostali = rezervacije.Prihvati(new OstaliVisitor(datumVrijemeProvjere));
            IspisiTablicu(putnicki, "putnickih", datumVrijemeProvjere);
            IspisiTablicu(poslovni, "poslovnih", datumVrijemeProvjere);
            IspisiTablicu(ostali, "ostalih", datumVrijemeProvjere);

            int ukupno = putnicki.Count + poslovni.Count + ostali.Count;
            IspisPoruke.Uspjeh($"Ukupno zauzetih vezova u {datumVrijemeProvjere} je {ukupno}");
        }

        private void IspisiTablicu(List<Vez> lista, string vrstaVeza, DateTime vrijeme)
        {
            List<string[]> listaPodatakaZaIspis = new List<string[]>();
            if(lista.Count == 0)
            {
                IspisPoruke.Greska($"Nema zauzetih {vrstaVeza} vezova u {vrijeme}");
                return;
            }
            foreach (Vez v in lista)
            {
                string[] podaciIspisa = { v.Id.ToString(), v.OznakaVeza!, v.Vrsta.nazivVeza!, v.Mol!.Naziv };
                listaPodatakaZaIspis.Add(podaciIspisa);
            }
            string nazivIspisa = $"Lista zauzetih {vrstaVeza} vezova u {vrijeme}";
            string[] naziviStupaca = { "Vez ID", "Oznaka", "Vrsta", "Mol" };
            Tablica.Instanca.IspisiTablicu(nazivIspisa, naziviStupaca, listaPodatakaZaIspis, 14);
            Console.WriteLine("\n");
        }
    }
}
