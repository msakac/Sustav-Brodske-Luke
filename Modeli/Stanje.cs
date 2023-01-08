namespace msakac_zadaca_3.Modeli
{
    public class Stanje
    {
        public string nazivStanja { get; set; }
        public DateTime virtualnoVrijeme { get; set; }
        public List<Rezervacija> listaRezervacija { get; set; }
        public Stanje(string nazivStanja, DateTime virtualnoVrijeme, List<Rezervacija> listaRezervacija)
        {
            this.nazivStanja = nazivStanja;
            this.virtualnoVrijeme = virtualnoVrijeme;
            this.listaRezervacija = listaRezervacija;
        }
    }
}
