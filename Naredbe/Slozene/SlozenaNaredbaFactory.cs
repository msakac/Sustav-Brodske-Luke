using msakac_zadaca_1.Naredbe.Jednostavne;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace msakac_zadaca_1.Naredbe.Slozene
{
    public class SlozenaNaredbaFactory : NaredbaFactory
    {
        public override AbstractSlozenaNaredba KreirajSlozenuNaredbu(string tip)
        {
            switch (tip)
            {
                case "kreiranje_rezerviranog_zahtjev":
                    return new KreiranjeRezerviranogZahtjeva();
                case "kreiranje_zahtjeva":
                    return new KreiranjeZahtjeva();
                case "vezovi_po_vrsti":
                    return new StatusVezovaPoVrsti();
                case "datoteka_zahtjeva":
                    return new UcitajDatotekuZahtjeva();
                case "vrijeme":
                    return new VirtualnoVrijeme();
                default:
                    throw new Exception($"Slozena naredba {tip} nije moguca!");
            }
        }
        public override AbstractJednostavnaNaredba KreirajJednostavnuNaredbu(string tip)
        {
            throw new NotImplementedException();
        }
    }
}

