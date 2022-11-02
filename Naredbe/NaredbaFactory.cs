using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using msakac_zadaca_1.Aplikacija;
using msakac_zadaca_1.Naredbe.Jednostavne;
using msakac_zadaca_1.Naredbe.Slozene;

namespace msakac_zadaca_1.Naredbe
{
    public abstract class NaredbaFactory
    {
        public abstract AbstractJednostavnaNaredba KreirajJednostavnuNaredbu(string tip);
        public abstract AbstractSlozenaNaredba KreirajSlozenuNaredbu(string tip);
    }
}
