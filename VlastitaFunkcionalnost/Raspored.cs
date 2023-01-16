using msakac_zadaca_3.Aplikacija;
using msakac_zadaca_3.Modeli;


namespace msakac_zadaca_3.VlastitaFunkcionalnost
{
    public class Raspored : Ispis
    {
        public override void ObradiZahtjev(string argument)
        {
            if (argument == "R")
            {
                List<string[]> listaPodatakaZaIspis = new List<string[]>();
                foreach (StavkaRasporeda sr in BrodskaLuka.Instanca().listaStavkiRasporeda)
                {
                    string daniUTjednu = string.Join(", ", sr.DaniUTjednu);
                    string[] podaciIspisa = { sr.IdVez.ToString(), sr.IdBrod.ToString(), sr.VrijemeOd.ToString(), sr.VrijemeDo.ToString(), daniUTjednu};
                    listaPodatakaZaIspis.Add(podaciIspisa);
                }
                string nazivIspisa = $"Lista stavki rasporeda";
                string[] naziviStupaca = { "Vez ID", "Brod ID", "Vrijeme Od", "Vrijeme Do", "Dani"};
                Tablica.Instanca.IspisiTablicu(nazivIspisa, naziviStupaca, listaPodatakaZaIspis, 14);
            }
            else
            {
                nasljednik?.ObradiZahtjev(argument);
            }
        }
    }
}
