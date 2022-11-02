using msakac_zadaca_1.Aplikacija;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace msakac_zadaca_1.Naredbe.Slozene
{
    public class UcitajDatotekuZahtjeva : AbstractSlozenaNaredba
    {
        public override void IzvrsiNaredbu(string naredba)
        {
            IspisPoruke.Uspjeh("Izvrsena naredba UCITAJ DATOTEKU ZAHTJEVA");
        }
    }
}
