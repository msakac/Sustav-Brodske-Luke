using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace msakac_zadaca_1.Aplikacija
{
    public interface IVirtualniSat
    {
        void Postavi(DateTime virtualnoVrijeme);
         void Tick(Object o);
         DateTime Dohvati();
         void IspisiVirtualnoVrijeme();
    }
}
