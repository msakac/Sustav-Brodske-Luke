using msakac_zadaca_3.Aplikacija;

namespace msakac_zadaca_3.VlastitaFunkcionalnost
{
    public class Raspored : Ispis
    {
        public override void ObradiZahtjev(string argument)
        {
            if (argument == "R")
            {
                Console.WriteLine("Raspored");
            }
            else
            {
                nasljednik?.ObradiZahtjev(argument);
            }
        }
    }
}
