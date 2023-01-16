using msakac_zadaca_3.Modeli;

namespace msakac_zadaca_3.Memento
{
    
    public class Stanje
    {
        private DateTime virtualnoVrijeme { get; set; }
        private List<Rezervacija> listaRezervacija { get; set; }
        public Stanje(DateTime virtualnoVrijeme, List<Rezervacija> listaRezervacija)
        {
            this.virtualnoVrijeme = virtualnoVrijeme;
            this.listaRezervacija = listaRezervacija;
        }
        public DateTime dohvatiVirtualnoVrijeme(){
            return this.virtualnoVrijeme;
        }

        public List<Rezervacija> dohvatiListuRezervacija(){
            return this.listaRezervacija;
        }
    }
}
