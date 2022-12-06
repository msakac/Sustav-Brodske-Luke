using msakac_zadaca_1.Modeli;

namespace msakac_zadaca_1.Aplikacija
{
    static class Pomagala
    {
        //Metoda provjerava preklapanje u 24 satnom formatu
        public static Boolean Postoji24hPreklapanje(TimeOnly vrijemeOd, TimeOnly vrijemeDo, TimeOnly zeljenoOd, TimeOnly zeljenoDo)
        {
            if ((zeljenoOd <= vrijemeDo && vrijemeOd <= zeljenoDo) || ((zeljenoOd > zeljenoDo) && (zeljenoOd <= vrijemeDo || vrijemeOd <= zeljenoDo)))
            {
                return true;
            }
            return false;
        }
        //Metoda provjerava da li se dva termina preklapaju
        public static Boolean PostojiVremenskoPreklapanja(DateTime vrijemeOd, DateTime vrijemeDo, DateTime zeljenoOd, DateTime zeljenoDo)
        {
            if (zeljenoOd <= vrijemeDo && vrijemeOd <= zeljenoDo)
            {
                return true;
            }
            return false;
        }
        //Metoda dohvaca sve rezervacija iz rasporeda i zahtjeva rezervaciji u nekom vremenskom periodu
        public static List<Rezervacija> DohvatiSveTermineZauzetostiUPeriodu(DateTime zeljenoOd, DateTime zeljenoDo)
        {
            TimeOnly zeljenoVrijemeOd = TimeOnly.Parse(zeljenoOd.ToString("HH:mm"));
            TimeOnly zeljenoVrijemeDo = TimeOnly.Parse(zeljenoDo.ToString("HH:mm"));
            BrodskaLuka brodskaLuka = BrodskaLuka.Instanca();
            //Dohvati sve rezervacije koje se preklapaju s zeljenim terminom
            List<Rezervacija> listaRezervacija = brodskaLuka.listaRezervacija.FindAll(x =>
            zeljenoOd <= x.DatumVrijemeDo && x.DatumVrijemeOd <= zeljenoDo);
            int brojDana = (int)(zeljenoDo - zeljenoOd).TotalDays;
            for (int i = 0; i <= brojDana; i++)
            {
                DateTime zeljenoOdPom = zeljenoOd.AddDays(i);
                DayOfWeek danTjednaPom = zeljenoOdPom.DayOfWeek;
                //foreach stavka raspored provjeri dan tjedna i postoji24hpreklapanje
                foreach (StavkaRasporeda stavka in brodskaLuka.listaStavkiRasporeda)
                {
                    // if (stavka.DaniUTjednu.Contains(danTjednaPom) && Postoji24hPreklapanje(stavka.VrijemeOd, stavka.VrijemeDo, zeljenoVrijemeOd, zeljenoVrijemeDo))
                    DateTime stavkaOd = zeljenoOdPom.Date.AddHours(stavka.VrijemeOd.Hour).AddMinutes(stavka.VrijemeOd.Minute);
                    DateTime stavkaDo = new DateTime();
                    if (stavka.DaniUTjednu.Contains(danTjednaPom))
                    {
                        TimeSpan trajanjeRezervacije = stavka.VrijemeOd - stavka.VrijemeDo;
                        if (stavka.VrijemeDo > stavka.VrijemeOd)
                        {
                            trajanjeRezervacije = stavka.VrijemeDo - stavka.VrijemeOd;
                            stavkaDo = stavkaOd.Add(trajanjeRezervacije);
                        }
                        else
                        {
                            TimeOnly pomocno = TimeOnly.Parse("00:00");
                            trajanjeRezervacije = pomocno - stavka.VrijemeOd;
                            stavkaDo = stavkaOd.Add(trajanjeRezervacije).AddHours(stavka.VrijemeDo.Hour).AddMinutes(stavka.VrijemeDo.Minute);
                        }

                        
                        // DateTime stavkaDo = stavkaOd.Add(trajanjeRezervacije).AddHours(stavka.VrijemeDo.Hour).AddMinutes(stavka.VrijemeDo.Minute);
                        listaRezervacija.Add(new Rezervacija(stavka.IdVez, stavka.IdBrod, stavkaOd, stavkaDo));
                    }
                }
            }
            return listaRezervacija;
        }
        //Metoda pronalazi moguce vezove za neki brod na temelju njegovih parametara i zauzetosti vezova u nekom vremenskom periodu
        public static List<Vez> PronadiMoguceVezove(Brod brod, DateTime datumVrijemeOd, DateTime datumVrijemeDo)
        {
            BrodskaLuka brodskaLuka = BrodskaLuka.Instanca();
            //Dohvatim sve moguce termine rezervacija u periodu
            List<Rezervacija> listaSvihRezervacijaUPeriodu = DohvatiSveTermineZauzetostiUPeriodu(datumVrijemeOd, datumVrijemeDo);

            //Svi Vezovi koji zadovoljavaju uvjete broda
            List<Vez> listaMogucihVezova = brodskaLuka.listaVezova.FindAll(vez => vez.Vrsta.oznakaVrsteBroda!.Contains(brod.Vrsta)
                && brod.Gaz <= vez.MaksimalnaDubina
                && brod.Sirina <= vez.MaksimalnaSirina
                && brod.Duljina <= vez.MaksimalnaDuljina);

            //Brisem sve vezove koji su zauzeti a odgovaraju uvjetima broda
            foreach (Rezervacija rez in listaSvihRezervacijaUPeriodu)
            {
                //Dohvaca samo one rezervacije koji su u periodu koji se preklapa sa zeljenim terminom
                if (PostojiVremenskoPreklapanja(rez.DatumVrijemeOd, rez.DatumVrijemeDo, datumVrijemeOd, datumVrijemeDo))
                {
                    Vez? vez = brodskaLuka.listaVezova.Find(vez => vez.Id == rez.IdVez);
                    if (vez != null)
                    {
                        listaMogucihVezova.Remove(vez);
                    }
                }
            }
            //provjera da li ima mogucih vezova
            if (listaMogucihVezova.Count == 0)
            {
                throw new Exception($"Nema mogućih vezova za brod sa ID-om {brod.Id} u terminu od {datumVrijemeOd} do {datumVrijemeDo}");
            }
            return listaMogucihVezova;
        }
        //Metoda pronalazi optimalan vez iz svih mogucih vezova za taj brod
        public static Vez PronadiOptimalanVez(List<Vez> listaMogucihVezova, Brod brod)
        {
            Vez? najboljiVez = null;
            foreach (Vez vez in listaMogucihVezova)
            {
                //izracunavam prazni prostor izmedu broda i veza
                double prazniProstor = vez.MaksimalnaDubina - brod.Gaz + vez.MaksimalnaSirina - brod.Sirina + vez.MaksimalnaDuljina - brod.Duljina;
                //ako je prva iteracija nemam najbolji vez
                if (najboljiVez == null)
                {
                    najboljiVez = vez;
                }
                else
                {
                    double prethodniPrazniProstor = najboljiVez.MaksimalnaDubina - brod.Gaz + najboljiVez.MaksimalnaSirina - brod.Sirina + najboljiVez.MaksimalnaDuljina - brod.Duljina;
                    //provjeri da li je prazni prostor manji od prethodnog
                    if (prazniProstor < prethodniPrazniProstor)
                    {
                        najboljiVez = vez;
                    }
                    //provjeri da li je prazni prostor jednak prethodnom
                    else if (prazniProstor == prethodniPrazniProstor)
                    {
                        if (vez.CijenaVezaPoSatu < najboljiVez.CijenaVezaPoSatu)
                        {
                            najboljiVez = vez;
                        }
                    }
                }
            }
            return najboljiVez!;
        }
    }
}
