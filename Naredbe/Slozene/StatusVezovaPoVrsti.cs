using msakac_zadaca_1.Aplikacija;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using msakac_zadaca_1.Modeli;

namespace msakac_zadaca_1.Naredbe.Slozene
{
    public class StatusVezovaPoVrsti : AbstractSlozenaNaredba
    {
        private string formatIspisa = "|{0,-3}|{1,-11}|{2,-5}|{3,-11}|{4,-10}|{5,-10}|{6,-11}|{7,-6}|";
        public override void IzvrsiNaredbu(string naredba)
        {
            BrodskaLuka brodskaLuka = BrodskaLuka.Instanca();

            string[]? argumenti = naredba.Split(' ');
            string veza = argumenti[1];
            string status = argumenti[2];
            string stringDatumVrijemeOd = argumenti[3] + " " + argumenti[4];
            string stringDatumVrijemeDo = argumenti[5] + " " + argumenti[6];
            DateTime datumVrijemeOd = DateTime.Parse(stringDatumVrijemeOd);
            DateTime datumVrijemeDo = DateTime.Parse(stringDatumVrijemeDo);

            VrstaVeza vrstaVeza = brodskaLuka.DohvatiVrstuVeza(argumenti[1]);
            DayOfWeek danTjednaOd = datumVrijemeOd.DayOfWeek;
            TimeOnly vrijemeOd = TimeOnly.Parse(datumVrijemeOd.ToString("HH:mm"));
            DayOfWeek danTjednaDo = datumVrijemeDo.DayOfWeek;
            TimeOnly vrijemeDo = TimeOnly.Parse(datumVrijemeDo.ToString("HH:mm"));

            foreach (Vez vez in brodskaLuka.listaVezova)
            {
                if (vez.Vrsta == vrstaVeza)
                {
                    List<StavkaRasporeda> stavkeVeza = brodskaLuka.listaStavkiRasporeda.FindAll(
                        stavka => stavka.IdVez == vez.Id &&
                        ((stavka.VrijemeOd <= vrijemeOd && stavka.VrijemeDo >= vrijemeOd && stavka.VrijemeDo <= vrijemeDo) ||
                        (stavka.VrijemeOd >= vrijemeOd && stavka.VrijemeDo <= vrijemeDo) ||
                        (stavka.VrijemeOd <= vrijemeDo && stavka.VrijemeDo >= vrijemeDo && stavka.VrijemeOd >= vrijemeOd)) &&
                         stavka.DaniUTjednu.Exists(dan => dan >= danTjednaOd - 1 && dan <= danTjednaDo + 1)
                        );
                    if (stavkeVeza.Count > 0)
                    {
                        IspisiRedak(vez);
                        foreach (StavkaRasporeda stavka in stavkeVeza)
                        {
                            List<DayOfWeek> listaDana = stavka.DaniUTjednu.FindAll(dan => dan >= danTjednaOd - 1 && dan <= danTjednaDo + 1);

                            DateTime DatumVrijemeStavkeOd = datumVrijemeOd.Subtract(new TimeSpan(1, 0, 0, 0)).Date;
                            DatumVrijemeStavkeOd = DatumVrijemeStavkeOd.Add(new TimeSpan(stavka.VrijemeOd.Hour, stavka.VrijemeOd.Minute, stavka.VrijemeOd.Second));

                            DateTime DatumVrijemeStavkeDo = datumVrijemeDo.Subtract(new TimeSpan(1, 0, 0, 0)).Date;
                            DatumVrijemeStavkeDo = DatumVrijemeStavkeDo.Add(new TimeSpan(stavka.VrijemeDo.Hour, stavka.VrijemeDo.Minute, stavka.VrijemeDo.Second));
                            var razlika = DatumVrijemeStavkeDo - DatumVrijemeStavkeOd;
                            for (int i = 0; i <= listaDana.Count + 1; i++)
                            {
                                razlika = DatumVrijemeStavkeDo - DatumVrijemeStavkeOd;
                                if (((DatumVrijemeStavkeOd <= datumVrijemeOd && DatumVrijemeStavkeDo >= datumVrijemeOd && DatumVrijemeStavkeDo <= datumVrijemeDo) ||
                                   (DatumVrijemeStavkeOd >= datumVrijemeOd && DatumVrijemeStavkeDo <= datumVrijemeDo) ||
                                   (DatumVrijemeStavkeOd <= datumVrijemeDo && DatumVrijemeStavkeDo >= datumVrijemeDo &&
                                   DatumVrijemeStavkeOd >= datumVrijemeOd)) && razlika.Days < 1
                                   && listaDana.Exists(dan => dan == DatumVrijemeStavkeOd.DayOfWeek)
                                   && DatumVrijemeStavkeDo > DatumVrijemeStavkeOd)
                                {
                                    break;
                                }
                                else
                                {
                                    if (i % 2 == 0)
                                    {
                                        DatumVrijemeStavkeDo = DatumVrijemeStavkeDo.Add(new TimeSpan(1, 0, 0, 0));
                                    }
                                    else
                                    {
                                        DatumVrijemeStavkeOd = DatumVrijemeStavkeOd.Add(new TimeSpan(1, 0, 0, 0));

                                    }
                                }
                            }
                            string ispis = String.Format(formatIspisa, "", "", "",
                            DatumVrijemeStavkeOd, DatumVrijemeStavkeDo, stavka.VrijemeOd, stavka.VrijemeDo, "");
                            if (((DatumVrijemeStavkeOd <= datumVrijemeOd && DatumVrijemeStavkeDo >= datumVrijemeOd && DatumVrijemeStavkeDo <= datumVrijemeDo) ||
                                   (DatumVrijemeStavkeOd >= datumVrijemeOd && DatumVrijemeStavkeDo <= datumVrijemeDo) ||
                                   (DatumVrijemeStavkeOd <= datumVrijemeDo && DatumVrijemeStavkeDo >= datumVrijemeDo &&
                                   DatumVrijemeStavkeOd >= datumVrijemeOd)) && razlika.Days < 1
                                   && listaDana.Exists(dan => dan == DatumVrijemeStavkeOd.DayOfWeek)
                                   && DatumVrijemeStavkeDo > DatumVrijemeStavkeOd)
                            {
                                IspisPoruke.Greska(ispis);
                            }
                        }
                    }
                }

            }
        }
        private void IspisiRedak(Vez vez)
        {
            string ispis = String.Format(formatIspisa, vez.Id, vez.OznakaVeza, vez.Vrsta.oznakaVeza,
            vez.CijenaVezaPoSatu + " kn", vez.MaksimalnaDubina + " m", vez.MaksimalnaSirina + " m", vez.MaksimalnaDuljina + " m", "Z");
            IspisPoruke.Uspjeh(ispis);

        }
    }
}
