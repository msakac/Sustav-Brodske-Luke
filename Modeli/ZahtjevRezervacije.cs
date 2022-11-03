using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            DateTime DatumVrijemeDo = DatumVrijemeOd.AddHours(TrajanjePrivezaUSatima);
            Brod? brod = brodskaLuka.listaBrodova.Find(brod => brod.Id == this.IdBrod);
            Rezervacija? rezervacijaPostoji = brodskaLuka.listaRezervacija.Find(rezervacija => rezervacija.IdBrod == this.IdBrod && (rezervacija.DatumVrijemeOd <= DatumVrijemeOd && rezervacija.DatumVrijemeDo >= DatumVrijemeOd));
            DayOfWeek danTjedna = this.DatumVrijemeOd.DayOfWeek;
            TimeOnly zeljenoVrijemePrivezaOd = TimeOnly.Parse(this.DatumVrijemeOd.ToString("HH:mm"));
            TimeOnly zeljenoVrijemePrivezaDo = zeljenoVrijemePrivezaOd.AddHours(this.TrajanjePrivezaUSatima);


            if (brod == null)
            {
                throw new Exception($"Brod sa ID-om {this.IdBrod} ne postoji u listi brodova!");
            }
            if (rezervacijaPostoji != null)
            {
                throw new Exception($"Brod sa ID-om {this.IdBrod} već ima rezervirani vez ({rezervacijaPostoji.IdVez}) od {rezervacijaPostoji.DatumVrijemeOd} do {rezervacijaPostoji.DatumVrijemeDo} ");
            }

            List<Vez> listaMogucihVezova = brodskaLuka.listaVezova.FindAll(vez => vez.Vrsta.oznakaVrsteBroda!.Contains(brod.Vrsta)
            && brod.Gaz <= vez.MaksimalnaDubina
            && brod.Sirina <= vez.MaksimalnaSirina
            && brod.Duljina <= vez.MaksimalnaDuljina);

            List<Vez> listaVezovaSaDobrimVremenom = new List<Vez>();
            bool rezerviran = false;
            foreach (Vez vez in listaMogucihVezova)
            {
                int zauzetURasporedu = brodskaLuka.listaStavkiRasporeda.FindIndex(stavka => stavka.DaniUTjednu.Contains(danTjedna) && stavka.IdVez == vez.Id &&
                ((stavka.VrijemeOd < zeljenoVrijemePrivezaOd && stavka.VrijemeDo > zeljenoVrijemePrivezaDo) ||
                (stavka.VrijemeOd > zeljenoVrijemePrivezaOd && stavka.VrijemeOd < zeljenoVrijemePrivezaDo) ||
                (stavka.VrijemeDo > zeljenoVrijemePrivezaOd && stavka.VrijemeDo < zeljenoVrijemePrivezaDo) ||
                (stavka.VrijemeOd > zeljenoVrijemePrivezaOd && stavka.VrijemeDo > zeljenoVrijemePrivezaOd
                && stavka.VrijemeOd < zeljenoVrijemePrivezaDo && stavka.VrijemeDo < zeljenoVrijemePrivezaDo)
                ));
                int zauzetURezervaciji = brodskaLuka.listaRezervacija.FindIndex(stavka => stavka.IdVez == vez.Id && provjeriInterval(stavka.DatumVrijemeOd, stavka.DatumVrijemeDo, DatumVrijemeOd, DatumVrijemeDo));
                if (zauzetURasporedu == -1 && zauzetURezervaciji == -1)
                {
                    Rezervacija rezervacija = new Rezervacija(vez.Id, this.IdBrod, DatumVrijemeOd, DatumVrijemeDo);
                    brodskaLuka.listaRezervacija.Add(rezervacija);
                    Console.WriteLine($"Zahtjev za rezervaciju veza uspješan! Brod {this.IdBrod} je rezervirao vez {vez.Id} od {this.DatumVrijemeOd} do {DatumVrijemeDo} ");
                    rezerviran = true;
                    break;
                }
            }
            if (!rezerviran) throw new Exception($"Nije moguće rezervirati vez za brod {this.IdBrod} od {this.DatumVrijemeOd} do {DatumVrijemeDo} ");

        }

        private Boolean provjeriInterval(DateTime stavkaVrijemeOd, DateTime stavkaVrijemeDo, DateTime zeljenoVrijemePrivezaOd, DateTime zeljenoVrijemePrivezaDo)
        {
            if ((stavkaVrijemeOd < zeljenoVrijemePrivezaOd && stavkaVrijemeDo > zeljenoVrijemePrivezaDo) ||
            (stavkaVrijemeOd > zeljenoVrijemePrivezaOd && stavkaVrijemeOd < zeljenoVrijemePrivezaDo) ||
            (stavkaVrijemeDo > zeljenoVrijemePrivezaOd && stavkaVrijemeDo < zeljenoVrijemePrivezaDo) ||
            (stavkaVrijemeOd > zeljenoVrijemePrivezaOd && stavkaVrijemeDo > zeljenoVrijemePrivezaOd
            && stavkaVrijemeOd < zeljenoVrijemePrivezaDo && stavkaVrijemeDo < zeljenoVrijemePrivezaDo))
            {
                return true;
            }
            return false;
        }
    }
}
