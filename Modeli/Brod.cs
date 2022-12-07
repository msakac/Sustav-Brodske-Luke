using msakac_zadaca_2.Aplikacija;

namespace msakac_zadaca_2.Modeli
{
    public enum OznakaVrsteBroda
    {
        TR,
        KA,
        KL,
        KR,
        RI,
        TE,
        JA,
        BR,
        RO
    }
    public class Brod
    {
        public int Id { get; set; }
        public string OznakaBroda { get; set; }
        public string Naziv { get; set; }
        public OznakaVrsteBroda Vrsta { get; set; }
        public double Duljina { get; set; }
        public double Sirina { get; set; }
        public double Gaz { get; set; }
        public double MaksimalnaBrzina { get; set; }
        public int KapacitetPutnika { get; set; }
        public int KapacitetOsobnihVozila { get; set; }
        public int KapacitetTereta { get; set; }
        public Kanal? aktivniKanal { get; set; }

        public Brod(int id, string oznakaBroda, string naziv, OznakaVrsteBroda vrsta, double duljina, double sirina, double gaz, double maksimalnaBrzina, int kapacitetPutnika, int kapacitetOsobnihVozila, int kapacitetTereta)
        {
            Id = id;
            OznakaBroda = oznakaBroda;
            Naziv = naziv;
            Vrsta = vrsta;
            Duljina = duljina;
            Sirina = sirina;
            Gaz = gaz;
            MaksimalnaBrzina = maksimalnaBrzina;
            KapacitetPutnika = kapacitetPutnika;
            KapacitetOsobnihVozila = kapacitetOsobnihVozila;
            KapacitetTereta = kapacitetTereta;
        }

        public void DodajUListuBrodova()
        {
            BrodskaLuka brodskaLuka = BrodskaLuka.Instanca();
            int index = brodskaLuka.listaBrodova.FindIndex(vez => vez.Id == this.Id);
            if (index >= 0)
            {
                throw new Exception($"Brod sa ID-om {this.Id} vec postoji u listi");
            }
            if (KapacitetPutnika == 0 && KapacitetOsobnihVozila == 0 && KapacitetTereta == 0)
            {
                throw new Exception($"Brod ne moze imati sve kapacitete jednake nuli");
            }
            brodskaLuka.listaBrodova.Add(this);
        }

        public static OznakaVrsteBroda dohvatiOznakuVrsteBroda(string oznakaVrsteBroda)
        {
            try
            {
                OznakaVrsteBroda vrstaBroda = (OznakaVrsteBroda)Enum.Parse(typeof(OznakaVrsteBroda), oznakaVrsteBroda);
                return vrstaBroda;
            }
            catch
            {
                throw new Exception(message: $"Oznaka vrste broda '{oznakaVrsteBroda}' ne postoji!");
            }
        }
    }
}
