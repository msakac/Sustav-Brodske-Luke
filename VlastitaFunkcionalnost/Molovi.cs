using msakac_zadaca_3.Aplikacija;
using msakac_zadaca_3.Modeli;

namespace msakac_zadaca_3.VlastitaFunkcionalnost
{
    public class Molovi : Ispis
    {
        public override void ObradiZahtjev(string argument)
        {
            if (argument == "M")
            {
                List<string[]> listaPodatakaZaIspis = new List<string[]>();
                foreach (Mol m in BrodskaLuka.Instanca().listaMolova)
                {
                    string[] podaciIspisa = { m.Id.ToString(), m.Naziv };
                    listaPodatakaZaIspis.Add(podaciIspisa);
                }
                string nazivIspisa = $"Lista svih molova";
                string[] naziviStupaca = { "Mol ID", "Naziv" };
                Tablica.Instanca.IspisiTablicu(nazivIspisa, naziviStupaca, listaPodatakaZaIspis, 11);
                Console.WriteLine("\n");
            }
            else
            {
                nasljednik?.ObradiZahtjev(argument);
            }
        }
    }
}
