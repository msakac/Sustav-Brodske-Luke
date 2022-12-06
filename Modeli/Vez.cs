using msakac_zadaca_1.Aplikacija;

namespace msakac_zadaca_1.Modeli
{
    public enum OznakaVrsteVeza
    {
        PU,
        PO,
        OS
    }
    public class Vez
    {
        public int Id { get; set; }
        public string? OznakaVeza { get; set; }
        public Mol? Mol { get; set; }
        public VrstaVeza Vrsta { get; set; }
        public int CijenaVezaPoSatu { get; set; }
        public int MaksimalnaDuljina { get; set; }
        public int MaksimalnaSirina { get; set; }
        public int MaksimalnaDubina { get; set; }

        public Vez(int id, string? oznakaVeza, VrstaVeza vrsta, int cijenaVezaPoSatu, int maksimalnaDuljina, int maksimalnaSirina, int maksimalnaDubina)
        {
            Id = id;
            OznakaVeza = oznakaVeza;
            Vrsta = vrsta;
            CijenaVezaPoSatu = cijenaVezaPoSatu;
            MaksimalnaDuljina = maksimalnaDuljina;
            MaksimalnaSirina = maksimalnaSirina;
            MaksimalnaDubina = maksimalnaDubina;
        }

        public void DodajUListuVezova()
        {
            BrodskaLuka brodskaLuka = BrodskaLuka.Instanca();

            int index = brodskaLuka.listaVezova.FindIndex(vez => vez.Id == this.Id);
            if (index >= 0)
            {
                throw new Exception($"Vez sa ID-om {this.Id} vec postoji u listi");
            }

            int brojVezovaUListi = brodskaLuka.listaVezova.Count(vez => vez.Vrsta == this.Vrsta);
            if (this.Vrsta.oznakaVeza == OznakaVrsteVeza.PU && brojVezovaUListi >= brodskaLuka.luka!.UkupniBrojPutnickihVezova)
            {
                throw new Exception($"Nije moguce kreirati novi 'Putnicki vez' jer u luki već postoji maksimalni broj putnickih vezova ({brojVezovaUListi})");
            }
            if (this.Vrsta.oznakaVeza == OznakaVrsteVeza.PO && brojVezovaUListi >= brodskaLuka.luka!.UkupniBrojPoslovnihVezova)
            {
                throw new Exception($"Nije moguce kreirati novi 'Poslovni vez' jer u luki već postoji maksimalni broj poslovnih vezova ({brojVezovaUListi})");
            }
            if (this.Vrsta.oznakaVeza == OznakaVrsteVeza.OS && brojVezovaUListi >= brodskaLuka.luka!.UkupniBrojOstalihVezova)
            {
                throw new Exception($"Nije moguce kreirati novi 'Ostali vez' jer u luki već postoji maksimalni broj  ostalih vezova ({brojVezovaUListi})");
            }
            if (this.MaksimalnaDubina >= brodskaLuka.luka!.DubinaLuke)
            {
                throw new Exception($"Vez ima preveliku dubinu ({this.MaksimalnaDubina}) za luku dubine '{brodskaLuka.luka!.DubinaLuke}'");
            }

            brodskaLuka.listaVezova.Add(this);
        }
    }
}
