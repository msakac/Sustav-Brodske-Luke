using msakac_zadaca_3.Aplikacija;
using msakac_zadaca_3.Modeli;

namespace msakac_zadaca_3.VlastitaFunkcionalnost
{
    public class Brodovi : Ispis
    {
        public override void ObradiZahtjev(string argument)
        {
            if (argument == "B")
            {
                List<string[]> listaPodatakaZaIspis = new List<string[]>();
                foreach (Brod b in BrodskaLuka.Instanca().listaBrodova)
                {
                    string[] podaciIspisa = { b.Id.ToString(), b.OznakaBroda, b.Naziv, b.Vrsta.ToString(), b.Duljina.ToString()+" m", b.Sirina.ToString()+" m", b.Gaz.ToString()+" m",
                    b.MaksimalnaBrzina+" cvora",b.KapacitetPutnika.ToString(), b.KapacitetOsobnihVozila.ToString(), b.KapacitetTereta.ToString()};
                    listaPodatakaZaIspis.Add(podaciIspisa);
                }
                string nazivIspisa = $"Lista svih brodova";
                string[] naziviStupaca = { "Brod ID", "Oznaka", "Naziv", "Vrsta", "Duljina", "Sirina", "Gaz", "Max brzina", "Max putnika", "Max vozila", "Max tereta" };
                Tablica.Instanca.IspisiTablicu(nazivIspisa, naziviStupaca, listaPodatakaZaIspis, 14);
                Console.WriteLine("\n");
            }
            else
            {
                nasljednik?.ObradiZahtjev(argument);
            }
        }
    }
}