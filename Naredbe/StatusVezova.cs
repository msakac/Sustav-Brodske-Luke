using msakac_zadaca_2.Aplikacija;
using msakac_zadaca_2.Modeli;

namespace msakac_zadaca_2.Naredbe
{
    public class StatusVezova : AbstractNaredba
    {
        public override void IzvrsiNaredbu(string naredba)
        {
            BrodskaLuka brodskaLuka = BrodskaLuka.Instanca();
            VirtualniSatProxy proxy = new VirtualniSatProxy();
            DateTime datumVrijemeOd = proxy.Dohvati();
            List<Rezervacija> listaSvihRezervacijaUPeriodu = Pomagala.DohvatiSveTermineZauzetostiUPeriodu(datumVrijemeOd.AddDays(-1), datumVrijemeOd.AddDays(1));
            List<string[]> listaPodatakaZaIspis = new List<string[]>();
            foreach (Vez vez in brodskaLuka.listaVezova)
            {
                int index = listaSvihRezervacijaUPeriodu.FindIndex(r => r.IdVez == vez.Id && r.DatumVrijemeOd <= datumVrijemeOd && r.DatumVrijemeDo >= datumVrijemeOd);
                string status = "Slobodan";
                if (index >= 0)
                {
                    status = "Zauzet";
                }
                string[] podaciIspisa = { vez.Id.ToString(), vez.OznakaVeza!, vez.Vrsta.oznakaVeza.ToString(), vez.CijenaVezaPoSatu.ToString(), vez.MaksimalnaDubina.ToString(),
                    vez.MaksimalnaSirina.ToString(), vez.MaksimalnaDuljina.ToString(), status};
                listaPodatakaZaIspis.Add(podaciIspisa);
            }
            string nazivIspisa = $"Pregled statusa vezova u trenutku {datumVrijemeOd}";
            string[] naziviStupaca = { "ID Vez", "Oznaka Veza", "Vrsta",
                "Cijena po h", "Max dubina", "Max sirina", "Max duljina", "Status" };
            Tablica.Instanca.IspisiTablicu(nazivIspisa, naziviStupaca, listaPodatakaZaIspis, 12);
        }
    }
}
