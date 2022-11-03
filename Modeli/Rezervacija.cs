using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using msakac_zadaca_1.Aplikacija;

namespace msakac_zadaca_1.Modeli
{
    public class Rezervacija
    {
        public int IdVez { get; set; }
        public int IdBrod { get; set; }
        public DateTime DatumVrijemeOd { get; set; }
        public DateTime DatumVrijemeDo { get; set; }

        public Rezervacija(int idVez, int idBrod, DateTime datumVrijemeOd, DateTime datumVrijemeDo)
        {
            IdVez = idVez;
            IdBrod = idBrod;
            DatumVrijemeOd = datumVrijemeOd;
            DatumVrijemeDo = datumVrijemeDo;
        }

    }
}
