using msakac_zadaca_1.Aplikacija;
using msakac_zadaca_1.Modeli;


namespace msakac_zadaca_1.VlastitaFunkcionalnost
{
    public class DnevnikRada : Ispis
    {
        public override void ObradiZahtjev(string argument)
        {
            if (argument == "D")
            {
                List<string[]> listaPodatakaZaIspis = new List<string[]>();
                List<StavkaDnevnika> listaStavkiDnevnika = BrodskaLuka.Instanca().listaStavkiDnevnika;
                if(listaStavkiDnevnika.Count == 0)
                {
                    IspisPoruke.Greska("Dnevnik rada je prazan!");
                    return;
                }
                foreach (StavkaDnevnika s in listaStavkiDnevnika)
                {
                    string[] podaciIspisa = { s.Brod.Id.ToString(), s.Brod.Naziv, s.VrijemeZahtjeva.ToString(), VratiVrijednost(s.ZahtjevOdobren) };
                    listaPodatakaZaIspis.Add(podaciIspisa);
                }
                string nazivIspisa = $"Lista svih priveza u dnevniku rada";
                string[] naziviStupaca = { "Brod ID", "Naziv Broda", "Vrijeme zahtjeva", "Status" };
                Tablica.Instanca.IspisiTablicu(nazivIspisa, naziviStupaca, listaPodatakaZaIspis, 20);
                Console.WriteLine("\n");
            }
            else
            {
                nasljednik?.ObradiZahtjev(argument);
            }
        }

        private string VratiVrijednost(Boolean b)
        {
            if (b)
            {
                return "Odobren";
            }
            else
            {
                return "Odbijen";
            }
        }
    }
}
