using msakac_zadaca_1.Aplikacija;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using msakac_zadaca_1.Modeli;

namespace msakac_zadaca_1.Naredbe.Jednostavne
{
    public class StatusVezova : AbstractJednostavnaNaredba
    {
        public override void IzvrsiNaredbu()
        {
            BrodskaLuka brodskaLuka = BrodskaLuka.Instanca();
            string prviRedak = String.Format("\n|{0,-3}|{1,-11}|{2,-5}|{3,-11}|{4,-10}|{5,-10}|{6,-11}|{7,-6}|", "ID", "Oznaka Veza", "Vrsta",
                "Cijena po h", "Max dubina", "Max sirina", "Max duljina", "Status");
            IspisPoruke.Uspjeh(prviRedak + "\n|---|-----------|-----|-----------|----------|----------|-----------|------|");

            DateTime datumVrijeme = VirtualniSat.Instanca.Dohvati();
            DayOfWeek danTjedna = datumVrijeme.DayOfWeek;
            TimeOnly vrijeme = TimeOnly.Parse(datumVrijeme.ToString("HH:mm"));

            foreach (Vez vez in brodskaLuka.listaVezova)
            {
                int index = brodskaLuka.listaStavkiRasporeda.FindIndex(
                    stavka => stavka.IdVez == vez.Id &&
                    stavka.DaniUTjednu.Exists(dan => dan == danTjedna) &&
                    stavka.VrijemeOd <= vrijeme &&
                    stavka.VrijemeDo >= vrijeme);
                IspisiRedak(vez, index);
            }
        }

        private void IspisiRedak(Vez vez, int index)
        {
            string status = "S";
            if (index >= 0)
            {
                status = "Z";
            }
            string ispis = String.Format("|{0,-3}|{1,-11}|{2,-5}|{3,-11}|{4,-10}|{5,-10}|{6,-11}|{7,-6}|", vez.Id, vez.OznakaVeza, vez.Vrsta.oznakaVeza,
            vez.CijenaVezaPoSatu + " kn", vez.MaksimalnaDubina + " m", vez.MaksimalnaSirina + " m", vez.MaksimalnaDuljina + " m", status);
            if (index >= 0)
            {
                status = "Z";
                IspisPoruke.Greska(ispis);
            }
            else
            {
                IspisPoruke.Uspjeh(ispis);
            }

        }
    }
}
