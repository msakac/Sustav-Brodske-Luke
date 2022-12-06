using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using msakac_zadaca_1.Aplikacija;

namespace msakac_zadaca_1.Modeli
{
    public class StavkaDnevnika
    {
        public Brod Brod { get; set; }
        public Boolean ZahtjevOdobren { get; set; }
        public DateTime VrijemeZahtjeva { get; set; }
        public string Razlog { get; set;}

        public StavkaDnevnika(Brod brod, Boolean zahtjevOdobren, DateTime vrijemeZahtjeva, string razlog)
        {
            Brod = brod;
            ZahtjevOdobren = zahtjevOdobren;
            VrijemeZahtjeva = vrijemeZahtjeva;
            Razlog = razlog;
        }
    }
}