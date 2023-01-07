using msakac_zadaca_3.Aplikacija;
using msakac_zadaca_3.CsvCitac;

namespace msakac_zadaca_3.Naredbe
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
