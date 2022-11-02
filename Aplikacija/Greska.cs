using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace msakac_zadaca_1.Aplikacija
{
    class Greska
    {
        private static Greska? instanca;
        private int brojacGreski = 0;
        public static Greska Instanca
        {
            get
            {
                if (instanca == null)
                {
                    instanca = new Greska();
                }
                return instanca;
            }
        }
        public void IspisiGresku(Exception e, string? redak)
        {
            string porukaGreske = e.Message;
            brojacGreski++;
            if (e is FormatException)
            {
                porukaGreske = "Format nekog argumenta u redku je neispravan!";
            }
            string poruka = $"Greska {brojacGreski}: {porukaGreske}.";
            if (redak != null)
            {
                poruka = $"Greska {brojacGreski}: {porukaGreske}. Redak: {redak}";
            }

            IspisPoruke.Greska(poruka);
        }
    }
}
