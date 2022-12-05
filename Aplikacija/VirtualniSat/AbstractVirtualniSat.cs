using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace msakac_zadaca_1.Aplikacija
{
    public abstract class AbstractVirtualniSat
    {
        public abstract void Postavi(DateTime virtualnoVrijeme);
         public abstract void Tick(Object o);
         public abstract DateTime Dohvati();
         public abstract void IspisiVirtualnoVrijeme();
    }
}
