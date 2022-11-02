using msakac_zadaca_1.Aplikacija;
using msakac_zadaca_1.Naredbe.Jednostavne;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace msakac_zadaca_1.Naredbe.Slozene
{
    public class KreiranjeRezerviranogZahtjeva : AbstractSlozenaNaredba
    {
        public override void IzvrsiNaredbu(string naredba)
        {
            IspisPoruke.Uspjeh("Izvrsena naredba KREIRANJE REZERVIRANOG ZAHTJEVA");
        }
    }
}
