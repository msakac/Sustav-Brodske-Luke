using msakac_zadaca_1.Aplikacija;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using msakac_zadaca_1.CsvCitac;

namespace msakac_zadaca_1.Naredbe.Slozene
{
    public class UcitajDatotekuZahtjeva : AbstractSlozenaNaredba
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
