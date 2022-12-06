using msakac_zadaca_1.Aplikacija;

namespace msakac_zadaca_1.VlastitaFunkcionalnost
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
