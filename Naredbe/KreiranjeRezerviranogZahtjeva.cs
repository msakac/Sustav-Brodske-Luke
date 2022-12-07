using msakac_zadaca_2.Aplikacija;
using msakac_zadaca_2.Modeli;


namespace msakac_zadaca_2.Naredbe
{
    public class KreiranjeRezerviranogZahtjeva : AbstractNaredba
    {
        public override void IzvrsiNaredbu(string naredba)
        {
            VirtualniSatProxy proxy = new VirtualniSatProxy();
            string[]? argumenti = naredba.Split(' ');
            int idBrod = int.Parse(argumenti[1]);
            BrodskaLuka brodskaLuka = BrodskaLuka.Instanca();
            DateTime DatumVrijemeOd = proxy.Dohvati();

            Brod? brod = brodskaLuka.listaBrodova.Find(brod => brod.Id == idBrod);
            //provjera da li brod postoji
            if (brod == null)
            {
                throw new Exception($"Brod sa ID-om {brod} ne postoji u listi brodova!");
            }

            if (brod.aktivniKanal == null)
            {
                throw new Exception($"Brod sa ID-om {brod.Id} nije spojen na neki kanal!");
            }
            //dohvacam sve termine koji su zauzeti u tom periodu
            List<Rezervacija> listaSvihRezervacijaUPeriodu = Pomagala.DohvatiSveTermineZauzetostiUPeriodu(DatumVrijemeOd.AddDays(-1), DatumVrijemeOd.AddDays(1));

            //provjera da li je brod vec rezerviran u tom periodu
            Rezervacija? rp = listaSvihRezervacijaUPeriodu.Find(r => r.IdBrod == brod.Id
            && r.DatumVrijemeOd <= DatumVrijemeOd && r.DatumVrijemeDo >= DatumVrijemeOd);
            string poruka = "";
            if (rp == null)
            {
                poruka = $"Brod sa ID-om {brod.Id} koji trazi dozvolu za privez nema rezerviran vez u virtualnom vremenu {DatumVrijemeOd}";
                IspisPoruke.Greska(poruka);
                brod.aktivniKanal.PosaljiPorukuBrodovima(poruka, brod);
                brodskaLuka.listaStavkiDnevnika.Add(new StavkaDnevnika(brod, false, DatumVrijemeOd, poruka));

                return;
            }
            poruka = $"Brod sa ID-om {brod.Id} trazi dozvolu za privez broda na rezervirani vez {rp!.IdVez} ({rp.DatumVrijemeOd} - {rp.DatumVrijemeDo}) u virtualnom vremenu {DatumVrijemeOd}";
            IspisPoruke.Uspjeh(poruka);
            brod.aktivniKanal.PosaljiPorukuBrodovima(poruka, brod);
            brodskaLuka.listaStavkiDnevnika.Add(new StavkaDnevnika(brod, true, DatumVrijemeOd, poruka));

        }
    }
}
