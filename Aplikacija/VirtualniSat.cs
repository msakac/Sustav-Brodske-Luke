using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace msakac_zadaca_1.Aplikacija
{
    public class VirtualniSat
    {
        private static VirtualniSat? instanca;
        private DateTime vrijeme = new DateTime();
        public static VirtualniSat Instanca
        {
            get
            {
                if (instanca == null)
                {
                    instanca = new VirtualniSat();
                }
                return instanca;
            }
        }

        public void Postavi(DateTime virtualnoVrijeme) {
            vrijeme = new DateTime(virtualnoVrijeme.Year, virtualnoVrijeme.Month, virtualnoVrijeme.Day, virtualnoVrijeme.Hour, virtualnoVrijeme.Minute, virtualnoVrijeme.Second);
        }
        public void Tick(Object o){
            vrijeme = vrijeme.AddSeconds(1);
        }
        public DateTime Dohvati(){
            return this.vrijeme;
        }
        public void IspisiVirtualnoVrijeme(){
            Console.WriteLine($"Virtualni sat: {this.vrijeme}");
        }
    }
}
