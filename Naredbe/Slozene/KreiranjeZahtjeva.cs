using msakac_zadaca_1.Aplikacija;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using msakac_zadaca_1.Modeli;


namespace msakac_zadaca_1.Naredbe.Slozene
{
    public class KreiranjeZahtjeva : AbstractSlozenaNaredba
    {
        public override void IzvrsiNaredbu(string naredba)
        {
            VirtualniSatProxy proxy = new VirtualniSatProxy();
            string[]? argumenti = naredba.Split(' ');
            int idBrod = int.Parse(argumenti[1]);
            int brojSati = int.Parse(argumenti[2]);
            BrodskaLuka brodskaLuka = BrodskaLuka.Instanca();
            Brod? brod = brodskaLuka.listaBrodova.Find(brod => brod.Id == idBrod);
            DateTime DatumVrijemeOd = proxy.Dohvati();
            DateTime DatumVrijemeDo = DatumVrijemeOd.AddHours(brojSati);

            DayOfWeek virtualniDanTjedna = DatumVrijemeOd.DayOfWeek;
            TimeOnly VrijemeOd = TimeOnly.Parse(DatumVrijemeOd.ToString("HH:mm"));
            TimeOnly VrijemeDo = TimeOnly.Parse(DatumVrijemeDo.ToString("HH:mm"));

            if (brod == null)
            {
                throw new Exception($"Brod sa ID-om {brod} ne postoji u listi brodova!");
            }

            StavkaRasporeda? stavka = brodskaLuka.listaStavkiRasporeda.Find(stavka => stavka.IdBrod == brod.Id && stavka.DaniUTjednu.Contains(virtualniDanTjedna)
            && stavka.VrijemeOd <= VrijemeOd && stavka.VrijemeDo >= VrijemeOd);
            if (stavka != null)
            {
                IspisPoruke.Greska($"Brod sa ID-om {idBrod} već ima vez u rasporedu ({stavka.IdVez}) od {stavka.VrijemeOd} do {stavka.VrijemeDo} ");
                return;
            }

            Rezervacija? rezervacija = brodskaLuka.listaRezervacija.Find(rezervacija => rezervacija.IdBrod == brod.Id
            && rezervacija.DatumVrijemeOd <= DatumVrijemeOd && rezervacija.DatumVrijemeDo >= DatumVrijemeOd);
            if (rezervacija != null)
            {
                IspisPoruke.Greska($"Brod sa ID-om {idBrod} već ima rezerviran vez ({rezervacija.IdVez}) od {rezervacija.DatumVrijemeOd} do {rezervacija.DatumVrijemeDo} ");
                return;
            }
            KreirajZahtjev(brodskaLuka, brod, idBrod, virtualniDanTjedna, VrijemeOd, VrijemeDo, DatumVrijemeOd, DatumVrijemeDo);
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

        private void KreirajZahtjev(BrodskaLuka brodskaLuka, Brod brod, int idBrod, DayOfWeek virtualniDanTjedna,
        TimeOnly VrijemeOd, TimeOnly VrijemeDo, DateTime DatumVrijemeOd, DateTime DatumVrijemeDo)
        {
            List<Vez> listaMogucihVezova = brodskaLuka.listaVezova.FindAll(vez => vez.Vrsta.oznakaVrsteBroda!.Contains(brod.Vrsta)
            && brod.Gaz <= vez.MaksimalnaDubina
            && brod.Sirina <= vez.MaksimalnaSirina
            && brod.Duljina <= vez.MaksimalnaDuljina);
            List<Vez> listaVezovaSaDobrimVremenom = new List<Vez>();

            bool privezan = false;
            foreach (Vez vez in listaMogucihVezova)
            {
                int zauzetURasporedu = brodskaLuka.listaStavkiRasporeda.FindIndex(stavka => stavka.DaniUTjednu.Contains(virtualniDanTjedna) && stavka.IdVez == vez.Id &&
                ((stavka.VrijemeOd < VrijemeOd && stavka.VrijemeDo > VrijemeDo) ||
                (stavka.VrijemeOd > VrijemeOd && stavka.VrijemeOd < VrijemeDo) ||
                (stavka.VrijemeDo > VrijemeOd && stavka.VrijemeDo < VrijemeDo) ||
                (stavka.VrijemeOd > VrijemeOd && stavka.VrijemeDo > VrijemeOd
                && stavka.VrijemeOd < VrijemeDo && stavka.VrijemeDo < VrijemeDo)
                ));
                int zauzetURezervaciji = brodskaLuka.listaRezervacija.FindIndex(stavka => stavka.IdVez == vez.Id && provjeriInterval(stavka.DatumVrijemeOd, stavka.DatumVrijemeDo, DatumVrijemeOd, DatumVrijemeDo));
                if (zauzetURasporedu == -1 && zauzetURezervaciji == -1)
                {
                    IspisPoruke.Uspjeh($"Brod {idBrod} trazi dozvolu za privez na {vez.Id} od {DatumVrijemeOd} do {DatumVrijemeDo} ");
                    privezan = true;
                    break;
                }
            }
            if (!privezan) throw new Exception($"Nije moguće privezati brod {idBrod} od {DatumVrijemeOd} do {DatumVrijemeDo} ");
        }
    }
}

//Brod koji nema rezerviran vez
//Brod mora odgovarati vezu
//Vez mora biti slobodan x brojSati
