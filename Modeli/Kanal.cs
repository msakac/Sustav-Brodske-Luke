using msakac_zadaca_3.Aplikacija;

namespace msakac_zadaca_3.Modeli
{
    public class Kanal
    {
        public int Id { get; set; }
        public int Frekvecija { get; set; }
        public int MaksimalanBroj { get; set; }
        public List<Brod>? SpojeniBrodovi { get; set; }
        private BrodskaLuka brodskaLuka = BrodskaLuka.Instanca();

        public Kanal(int id, int frekvecija, int maksimalanBroj)
        {
            Id = id;
            Frekvecija = frekvecija;
            MaksimalanBroj = maksimalanBroj;
        }

        public void DodajUListuKanala()
        {
            int index = brodskaLuka.listaKanala.FindIndex(kanal => kanal.Id == this.Id);
            if (index >= 0)
            {
                throw new Exception($"Kanal sa ID-om {this.Id} vec postoji u listi");
            }
            index = brodskaLuka.listaKanala.FindIndex(kanal => kanal.Frekvecija == this.Frekvecija);
            if (index >= 0)
            {
                throw new Exception($"Kanal sa frekvencijom {this.Frekvecija} vec postoji u listi");
            }
            brodskaLuka.listaKanala.Add(this);
        }

        public void SpojiBrodNaKanal(Brod brod)
        {
            if (this.SpojeniBrodovi == null)
            {
                this.SpojeniBrodovi = new List<Brod>();
            }
            // provjeri da li je brod vec spojen na kanal
            if (brod.aktivniKanal != null)
            {
                throw new Exception($"Brod sa ID-om {brod.Id} je vec spojen na kanal sa frekvencijom {brod.aktivniKanal.Frekvecija}");
            }
            // provjeri da li kanal ima slobodnih mjesta
            if (this.SpojeniBrodovi.Count >= this.MaksimalanBroj)
            {
                throw new Exception($"Kanal frekvencije {this.Frekvecija} je popunjen");
            }
            // ako je sve ok, spoji brod na kanal
            this.SpojeniBrodovi.Add(brod);
            brodskaLuka.listaBrodova.First(b => b.Id == brod.Id).aktivniKanal = this;
            brodskaLuka.ispis!.DodajUpis($"Brod sa ID-om {brod.Id} je uspjesno spojen na kanal sa frekvencijom {this.Frekvecija}");
        }

        public void OdjaviBrodSaKanala(Brod brod)
        {
            if (this.SpojeniBrodovi == null)
            {
                this.SpojeniBrodovi = new List<Brod>();
            }
            // provjeri da li brod postoji u listi spojenih brodova
            Brod? b = this.SpojeniBrodovi.Find(br => br.Id == brod.Id);
            if (b == null)
            {
                throw new Exception($"Brod sa ID-om {brod.Id} nije spojen na kanal sa frekvencijom {this.Frekvecija}");
            }
            // ako postoji, makni ga iz liste i azuriraj u listi brodova
            this.SpojeniBrodovi.Remove(b);
            brodskaLuka.listaBrodova.First(b => b.Id == brod.Id).aktivniKanal = null;
            brodskaLuka.ispis!.DodajUpis($"Brod sa ID-om {brod.Id} je odjavljen sa kanala sa frekvencijom {this.Frekvecija}");
        }

        public void PosaljiPorukuBrodovima(string poruka, Brod brod)
        {
            if (this.SpojeniBrodovi == null)
            {
                this.SpojeniBrodovi = new List<Brod>();
            }
            foreach (Brod b in this.SpojeniBrodovi)
            {
                if (b.Id != brod.Id)
                {
                    brodskaLuka.ispis!.DodajUpis($"Kanal {this.Frekvecija}, Brod {b.Id} prima poruku: {poruka}");
                }
            }
        }
    }
}
