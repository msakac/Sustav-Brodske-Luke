using msakac_zadaca_1.Aplikacija;
using msakac_zadaca_1.Naredbe.Jednostavne;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using msakac_zadaca_1.Modeli;


namespace msakac_zadaca_1.Naredbe.Slozene
{
    public class KreiranjeRezerviranogZahtjeva : AbstractSlozenaNaredba
    {
        public override void IzvrsiNaredbu(string naredba)
        {
            string[]? argumenti = naredba.Split(' ');
            int idBrod = int.Parse(argumenti[1]);
            BrodskaLuka brodskaLuka = BrodskaLuka.Instanca();
            Brod? brod = brodskaLuka.listaBrodova.Find(brod => brod.Id == idBrod);
            DateTime virtualniDatumVrijeme = VirtualniSat.Instanca.Dohvati();
            DayOfWeek virtualniDanTjedna = virtualniDatumVrijeme.DayOfWeek;
            TimeOnly virtualnoVrijeme = TimeOnly.Parse(virtualniDatumVrijeme.ToString("HH:mm"));

            if (brod == null)
            {
                throw new Exception($"Brod sa ID-om {brod} ne postoji u listi brodova!");
            }

            StavkaRasporeda? stavka = brodskaLuka.listaStavkiRasporeda.Find(stavka => stavka.IdBrod == brod.Id && stavka.DaniUTjednu.Contains(virtualniDanTjedna));
            if (stavka != null)
            {
                if (stavka.VrijemeOd <= virtualnoVrijeme && stavka.VrijemeDo >= virtualnoVrijeme)
                {
                    IspisPoruke.Uspjeh($"Brod {brod.Id} trazi dozvolu za privez broda na rezervirani vez {stavka.IdVez} u vremenu {virtualniDatumVrijeme.ToString("dd.mm.yyyy. hh:mm:ss")}");
                }
                else
                {
                    IspisPoruke.Greska($"Odbijem zahtjev broda {brod.Id} za privez na vez {stavka.IdVez} u vremenu {virtualniDatumVrijeme.ToString("dd.mm.yyyy. hh:mm:ss")}");

                }
                return;
            }

            Rezervacija? rezervacija = brodskaLuka.listaRezervacija.Find(rezervacija => rezervacija.IdBrod == brod.Id);
            if (rezervacija != null)
            {
                if (rezervacija.DatumVrijemeOd <= virtualniDatumVrijeme && rezervacija.DatumVrijemeDo >= virtualniDatumVrijeme)
                {
                    IspisPoruke.Uspjeh($"Brod {brod.Id} trazi dozvolu za privez broda na rezervirani vez {rezervacija.IdVez} u vremenu {virtualniDatumVrijeme.ToString("dd.mm.yyyy. hh:mm:ss")}");
                }
                else
                {
                    IspisPoruke.Greska($"Odbijem zahtjev broda {brod.Id} za privez na vez {rezervacija.IdVez} u vremenu {virtualniDatumVrijeme.ToString("dd.mm.yyyy. hh:mm:ss")}");
                }
                return;
            }

            throw new Exception($"Brod sa ID-om {brod.Id} nema odobrenu rezervaciju niti fiksan raspored!");

        }
    }
}
