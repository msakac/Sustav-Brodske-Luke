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
                         stavka.DaniUTjednu.Exists(dan => dan >= danTjednaOd && dan <= danTjednaDo)
                        );
                    if (stavkeVeza.Count > 0)
                    {
                        IspisiRedak(vez);
                        foreach (StavkaRasporeda stavka in stavkeVeza)
                        {
                            List<DayOfWeek> listaDana = stavka.DaniUTjednu.FindAll(dan => dan >= danTjednaOd-1 && dan <= danTjednaDo+1);

                            //tu nekaj jebe z tim datumom zauzetosti jer ga ne resetiram svaki put
                            DateTime datumZauzetosti = datumVrijemeOd;
                            foreach (DayOfWeek dan in listaDana)
                            {
                                datumZauzetosti = datumVrijemeOd;
                                DayOfWeek danUpita = datumZauzetosti.DayOfWeek;
                                while (dan != danUpita)
                                {
                                    datumZauzetosti = datumZauzetosti.AddDays(1);
                                    danUpita = datumZauzetosti.DayOfWeek;
                                }
                            }
                            if(stavka.VrijemeOd > stavka.VrijemeDo){
                                Console.WriteLine("manji");
                                datumZauzetosti.AddDays(-1);
                            }
                            string ispis = String.Format(formatIspisa, "", "", "",
                            datumZauzetosti.ToString("dd.MM.yyyy."), "Zauzet od", stavka.VrijemeOd, stavka.VrijemeDo, "");

                            IspisPoruke.Greska(ispis);
                        }
                    }
                    //zvadim van dan u tjednu i zvadim z trenutkog datuma od i trenutni datum povecam i opet vadim van dan dok ne budu isti
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
//Za svaki vez iz rasporeda zvaditi sve vezove kojima odgovara isti id, vrsta koju trazim i vrijeme
//Za vrijeme moram znati koji su moji dani od do 
