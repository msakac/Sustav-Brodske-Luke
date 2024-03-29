using msakac_zadaca_3.Aplikacija;
using msakac_zadaca_3.Modeli;

namespace msakac_zadaca_3.VlastitaFunkcionalnost
{
    public class Kanali : Ispis
    {
        public override void ObradiZahtjev(string argument)
        {
            if (argument == "K")
            {
                List<string[]> listaPodatakaZaIspis = new List<string[]>();
                foreach (Kanal k in BrodskaLuka.Instanca().listaKanala)
                {
                    string[] podaciIspisa = { k.Id.ToString(), k.Frekvecija.ToString()+" MHz", k.MaksimalanBroj.ToString() };
                    listaPodatakaZaIspis.Add(podaciIspisa);
                }
                string nazivIspisa = $"Lista svih kanala";
                string[] naziviStupaca = { "Kanal ID", "Frekvencija", "Max broj" };
                Tablica.Instanca.IspisiTablicu(nazivIspisa, naziviStupaca, listaPodatakaZaIspis, 12);
            }
            else
            {
                nasljednik?.ObradiZahtjev(argument);
            }
        }
    }
}
