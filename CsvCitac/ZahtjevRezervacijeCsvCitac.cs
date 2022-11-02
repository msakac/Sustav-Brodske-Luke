using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace msakac_zadaca_1.CsvCitac
{
    public class ZahtjevRezervacijeCsvCitac : ICsvCitac
    {
        public void citajPodatke(string datoteka)
        {
            Console.WriteLine($"Citam podatke datoteke: {datoteka}");
        }
    }
}
