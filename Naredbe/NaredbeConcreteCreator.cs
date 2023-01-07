namespace msakac_zadaca_3.Naredbe
{
    public class NaredbeConcreteCreator : NaredbaCreator
    {
        public override AbstractNaredba KreirajNaredbu(string tip)
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
                case "ispis_podataka":
                    return new IspisPodataka();
                case "status_vezova":
                    return new StatusVezova();
                case "prekid_rada":
                    return new PrekidRada();
                default:
                    throw new Exception($"Naredba {tip} nije moguca!");
            }
        }
    }
}
