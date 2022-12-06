using msakac_zadaca_1.Aplikacija;

namespace msakac_zadaca_1.Modeli
{
    public class ZahtjevRezervacije
    {
        public int IdBrod { get; set; }
        public DateTime DatumVrijemeOd { get; set; }
        public int TrajanjePrivezaUSatima { get; set; }

        public ZahtjevRezervacije(int idBrod, DateTime datumVrijemeOd, int trajanjePrivezaUSatima)
        {
            IdBrod = idBrod;
            DatumVrijemeOd = datumVrijemeOd;
            TrajanjePrivezaUSatima = trajanjePrivezaUSatima;
        }
        public void DodajUListuZahtjevaRezervacije()
        {
            BrodskaLuka brodskaLuka = BrodskaLuka.Instanca();
            Brod? brod = brodskaLuka.listaBrodova.Find(brod => brod.Id == this.IdBrod);
            //provjera da li brod postoji
            if (brod == null)
            {
                throw new Exception($"Brod sa ID-om {this.IdBrod} ne postoji u listi brodova!");
            }

            //provjera da je brod vec rezervirao neki vez u terminu koji presječe željeni termin
            DateTime DatumVrijemeDo = DatumVrijemeOd.AddHours(TrajanjePrivezaUSatima);
            Rezervacija? rezervacijaPostoji = brodskaLuka.listaRezervacija.Find(rezervacija => rezervacija.IdBrod == this.IdBrod &&
            Pomagala.PostojiVremenskoPreklapanja(rezervacija.DatumVrijemeOd, rezervacija.DatumVrijemeDo, this.DatumVrijemeOd, DatumVrijemeDo));
            if (rezervacijaPostoji != null)
            {
                throw new Exception($"Brod sa ID-om {this.IdBrod} već ima rezervirani vez ({rezervacijaPostoji.IdVez}) od {rezervacijaPostoji.DatumVrijemeOd} do {rezervacijaPostoji.DatumVrijemeDo} ");
            }
            //Dohvatim sve moguce vezove
            List<Vez> listaMogucihVezova = Pomagala.PronadiMoguceVezove(brod, this.DatumVrijemeOd, DatumVrijemeDo);

            //pronadi najbolji vez prema uvjetima
            Vez? najboljiVez = Pomagala.PronadiOptimalanVez(listaMogucihVezova, brod);
            if (najboljiVez == null)
            {
                throw new Exception($"Nema mogućih vezova za brod sa ID-om {this.IdBrod} u terminu od {this.DatumVrijemeOd} do {DatumVrijemeDo}");
            }
            Rezervacija rezervacija = new Rezervacija(najboljiVez.Id, this.IdBrod, DatumVrijemeOd, DatumVrijemeDo);
            brodskaLuka.listaRezervacija.Add(rezervacija);
            IspisPoruke.Uspjeh($"Zahtjev za rezervaciju | Brod {this.IdBrod} je rezervirao optimalan vez {najboljiVez.Id} od {this.DatumVrijemeOd} do {DatumVrijemeDo} ");
        }
    }
}
