using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace msakac_zadaca_1.CsvCitac
{
    public abstract class CsvCitacCreator
    {
        public abstract AbstractCsvCitac KreirajCitac(string tip);
    }
}
