using msakac_zadaca_1.Aplikacija;
using msakac_zadaca_1.CsvCitac;

namespace msakac_zadaca_1.Naredbe
{
    public class UcitajDatotekuZahtjeva : AbstractNaredba
    {
        public override void IzvrsiNaredbu(string naredba)
        {
            string[]? argumenti = naredba.Split(' ');
            CsvCitacCreator objekt = new CsvCitacConcreteCreator();
            AbstractCsvCitac csvCitac = objekt.KreirajCitac("rezervacije");
            csvCitac.citajPodatke(argumenti[1]);

            BrodskaLuka brodskaLuka = BrodskaLuka.Instanca();
            
        }
    }
}
