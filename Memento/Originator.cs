using msakac_zadaca_3.Modeli;
using msakac_zadaca_3.Aplikacija;

namespace msakac_zadaca_3.Memento
{

    // Predstavlja Memento klasu
    public class Originator
    {
        public DateTime virtualnoVrijeme { get; set; }
        public List<Rezervacija>? listaRezervacija { get; set; }

        public void spremiStanje(string nazivStanja)
        {
            BrodskaLuka brodskaLuka = BrodskaLuka.Instanca();
            brodskaLuka.caretaker.dodaj(nazivStanja, new Stanje(virtualnoVrijeme, listaRezervacija!));
        }

        public void vratiStanje(string nazivStanje)
        {
            BrodskaLuka brodskaLuka = BrodskaLuka.Instanca();
            Stanje? stanje = brodskaLuka.caretaker.dohvati(nazivStanje);
            this.listaRezervacija = new List<Rezervacija>(stanje!.dohvatiListuRezervacija());
            this.virtualnoVrijeme = stanje.dohvatiVirtualnoVrijeme();
        }
    }
}
