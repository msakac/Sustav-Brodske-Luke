using msakac_zadaca_3.Aplikacija;
using msakac_zadaca_3.Modeli;


namespace msakac_zadaca_3.Naredbe
{
    public class KreiranjeZahtjeva : AbstractNaredba
    {
        public override void IzvrsiNaredbu(string naredba)
        {
            VirtualniSatProxy proxy = new VirtualniSatProxy();
            string[]? argumenti = naredba.Split(' ');
            int idBrod = int.Parse(argumenti[1]);
            int brojSati = int.Parse(argumenti[2]);
            DateTime DatumVrijemeOd = proxy.Dohvati();
            DateTime DatumVrijemeDo = DatumVrijemeOd.AddHours(brojSati);

            BrodskaLuka brodskaLuka = BrodskaLuka.Instanca();
            Brod? brod = brodskaLuka.listaBrodova.Find(brod => brod.Id == idBrod);
            //provjera da li brod postoji
            if (brod == null)
            {
                throw new Exception($"Brod sa ID-om {idBrod} ne postoji u listi brodova!");
            }

            if (brod.aktivniKanal == null)
            {
                throw new Exception($"Brod sa ID-om {brod.Id} nije spojen na neki kanal!");
            }
            //dohvacam sve termine koji su zauzeti u tom periodu
            List<Rezervacija> listaSvihRezervacijaUPeriodu = Pomagala.DohvatiSveTermineZauzetostiUPeriodu(DatumVrijemeOd, DatumVrijemeDo);

            //provjera da li je brod vec rezerviran u tom periodu
            Rezervacija? rp = listaSvihRezervacijaUPeriodu.Find(r => r.IdBrod == brod.Id
            && Pomagala.PostojiVremenskoPreklapanja(r.DatumVrijemeOd, r.DatumVrijemeDo, DatumVrijemeOd, DatumVrijemeDo));
            string poruka = "";
            if (rp != null)
            {
                poruka = $"Brod sa ID-om {brod.Id} već ima vez u rezervaciji ({rp.IdVez}) od {rp.DatumVrijemeOd} do {rp.DatumVrijemeDo}";
                brodskaLuka.ispis!.DodajGresku(poruka);
                brod.aktivniKanal.PosaljiPorukuBrodovima(poruka, brod);
                brodskaLuka.listaStavkiDnevnika.Add(new StavkaDnevnika(brod, false, DatumVrijemeOd, poruka));
                return;
            }
            //Dohvati sve vezove koji odgovaraju brodu i terminima
            List<Vez> listaMogucihVezova = Pomagala.PronadiMoguceVezove(brod, DatumVrijemeOd, DatumVrijemeDo);
            //pronadi najbolji vez prema uvjetima
            Vez? najboljiVez = Pomagala.PronadiOptimalanVez(listaMogucihVezova, brod);
            if (najboljiVez == null)
            {
                poruka = $"Trenutno nema slobodnih vezova za brod sa ID-om {brod.Id} u terminu od {DatumVrijemeOd} do {DatumVrijemeDo}";
                brodskaLuka.ispis!.DodajGresku(poruka);
                brod.aktivniKanal.PosaljiPorukuBrodovima(poruka, brod);
                brodskaLuka.listaStavkiDnevnika.Add(new StavkaDnevnika(brod, false, DatumVrijemeOd, poruka));
                return;
            }
            Rezervacija rezervacija = new Rezervacija(najboljiVez.Id, brod.Id, DatumVrijemeOd, DatumVrijemeDo);
            brodskaLuka.listaRezervacija.Add(rezervacija);
            poruka = $"Zahtjev za privez | Brod {brod.Id} koji nema rezervirani vez trazi privez na optimalan vez {najboljiVez.Id} od {DatumVrijemeOd} do {DatumVrijemeDo}";
            brodskaLuka.ispis!.DodajUpis(poruka);
            brod.aktivniKanal.PosaljiPorukuBrodovima(poruka, brod);
            brodskaLuka.listaStavkiDnevnika.Add(new StavkaDnevnika(brod, true, DatumVrijemeOd, poruka));

        }
    }
}

