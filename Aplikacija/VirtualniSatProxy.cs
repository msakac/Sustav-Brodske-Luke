using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace msakac_zadaca_1.Aplikacija
{
    public class VirtualniSatProxy : IVirtualniSat
    {
        private VirtualniSat virtualniSat = VirtualniSat.Instanca;
        public void Postavi(DateTime virtualnoVrijeme)
        {
            virtualniSat.Postavi(virtualnoVrijeme);
        }
        public void Tick(Object o)
        {
            virtualniSat.Tick(o);
        }
        public DateTime Dohvati()
        {
            return virtualniSat.Dohvati();
        }
        public void IspisiVirtualnoVrijeme()
        {
            virtualniSat.IspisiVirtualnoVrijeme();
        }
    }
}
