using msakac_zadaca_1.Aplikacija;
using msakac_zadaca_1.Modeli;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace msakac_zadaca_1.CsvCitac
{
    public class MolVezoviCsvCitac : AbstractCsvCitac
    {
        public override void citajPodatke(string datoteka)
        {
            Console.WriteLine($"\nMol-Vezovi | Učitavam datoteku: {datoteka}...");
        }
    }
}
