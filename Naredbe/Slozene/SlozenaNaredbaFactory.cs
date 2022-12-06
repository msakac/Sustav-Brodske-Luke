using msakac_zadaca_1.Naredbe.Jednostavne;
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
                case "komunikacija_brod_kanal":
                    return new KomunikacijaBrodKanal();
                case "format_ispisa_tablica":
                    return new FormatIspisaTablica();
                case "zauzeti_vezovi_prema_vrsti":
                    return new ZauzetiVezoviPremaVrsti();
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

