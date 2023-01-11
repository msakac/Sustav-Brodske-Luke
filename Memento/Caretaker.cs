using msakac_zadaca_3.Aplikacija;

namespace msakac_zadaca_3.Memento
{
    public class Caretaker
    {
        private List<KeyValuePair<string, Stanje>> listaStanja = new List<KeyValuePair<string, Stanje>>();

        public void dodaj(string naziv, Stanje stanje)
        {
            BrodskaLuka brodskaLuka = BrodskaLuka.Instanca();
            if (pretraziKljuceve(naziv) != null)
            {
                throw new Exception($"Stanje sa nazivom {naziv} vec postoji u listi spremljenih stanja");
            }
            listaStanja.Add(new KeyValuePair<string, Stanje>(naziv, stanje));
            brodskaLuka.ispis!.DodajUpis($"Spremljeno trenutno stanje svih vezova u trenutku virtualnog vremena nazivom: {naziv}");
        }

        public Stanje? dohvati(string naziv)
        {
            Stanje? stanje = pretraziKljuceve(naziv);
            if (stanje == null)
            {
                throw new Exception($"Stanje sa nazivom {naziv} ne postoji u listi spremljenih stanja");
            }
            return stanje;
        }

        private Stanje? pretraziKljuceve(string kljuc)
        {
            foreach (KeyValuePair<string, Stanje> stanje in listaStanja)
            {
                if (stanje.Key == kljuc)
                {
                    return stanje.Value;
                }
            }
            return null;
        }
    }
}
