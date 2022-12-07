using msakac_zadaca_1.Aplikacija;
using msakac_zadaca_1.Modeli;

namespace msakac_zadaca_1.Naredbe
{
    public class StatusVezovaPoVrsti : AbstractNaredba
    {
        private string formatIspisa = "{0,-4}|{1,-3}|{2,-8}|{3,-5}|{4,-21}|{5,-21}|";
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

            VrstaVeza vrstaVeza = brodskaLuka.DohvatiVrstuVeza(veza);
            List<Rezervacija> listaSvihRezervacijaUPeriodu = Pomagala.DohvatiSveTermineZauzetostiUPeriodu(datumVrijemeOd.AddDays(-1), datumVrijemeDo.AddDays(1));
            List<string[]> listaPodatakaZaIspis = new List<string[]>();
            foreach (Rezervacija r in listaSvihRezervacijaUPeriodu)
            {
                if (Pomagala.PostojiVremenskoPreklapanja(r.DatumVrijemeOd, r.DatumVrijemeDo, datumVrijemeOd, datumVrijemeDo))
                {
                    Vez vez = brodskaLuka.listaVezova.Find(v => v.Id == r.IdVez)!;
                    if (vez.Vrsta == vrstaVeza)
                    {
                        string[] podaciIspisa = { vez.Id.ToString(), vez.OznakaVeza!, vez.Vrsta.oznakaVeza.ToString(), r.DatumVrijemeOd.ToString(), r.DatumVrijemeDo.ToString() };
                        listaPodatakaZaIspis.Add(podaciIspisa);
                    }
                }
            }
            string nazivIspisa = "Lista zauzetih vezova po vrsti u terminu od " + datumVrijemeOd + " do " + datumVrijemeDo;
            string[] naziviStupaca = { "Vez", "Oznaka", "Vrsta", "Rezerviran Od", "Rezerviran Do" };
            Tablica.Instanca.IspisiTablicu(nazivIspisa, naziviStupaca, listaPodatakaZaIspis);
        }
    }
}
