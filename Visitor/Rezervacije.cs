using msakac_zadaca_3.Modeli;

namespace msakac_zadaca_3.Visitor
{
    public class Rezervacije
    {
        private List<Rezervacija> _rezervacije = new List<Rezervacija>();


        public void DodajRezervaciju(Rezervacija rezervacija)
        {
            _rezervacije.Add(rezervacija);
        }

        public void IzbrisiRezervaciju(Rezervacija rezervacija)
        {
            _rezervacije.Remove(rezervacija);
        }

        public List<Vez> Prihvati(IVisitor visitor)
        {
            List<Vez> zauzetiVezovi = new List<Vez>();
            foreach (Rezervacija rezervacija in _rezervacije)
            {
                Vez? v = rezervacija.Accept(visitor);
                if(v != null){
                    zauzetiVezovi.Add(v);
                }
            }
            return zauzetiVezovi;
        }
    }
}
