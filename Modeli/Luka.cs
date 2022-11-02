using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using msakac_zadaca_1.Aplikacija;

namespace msakac_zadaca_1.Modeli
{
    public class Luka
    {
        public string Naziv { get; set; }
        public long GPS_Sirina { get; set; }
        public long GPS_Visina { get; set; }
        public int DubinaLuke { get; set; }
        public int UkupniBrojPutnickihVezova { get; set; }
        public int UkupniBrojPoslovnihVezova { get; set; }
        public int UkupniBrojOstalihVezova { get; set; }
        public DateTime VirtualnoVrijeme { get; set; }

        public Luka(string naziv, long gps_sirina, long gps_visina, int dubinaLuke, int ukupniBrojPutnickihVezova,
        int ukupniBrojPoslovnihVezova, int ukupniBrojOstalihVezova, DateTime virtualnoVrijeme)
        {
            Naziv = naziv;
            GPS_Sirina = gps_sirina;
            GPS_Visina = gps_visina;
            DubinaLuke = dubinaLuke;
            UkupniBrojPutnickihVezova = ukupniBrojPutnickihVezova;
            UkupniBrojOstalihVezova = ukupniBrojOstalihVezova;
            UkupniBrojPoslovnihVezova = ukupniBrojPoslovnihVezova;
            VirtualnoVrijeme = virtualnoVrijeme;
        }
    }
}
