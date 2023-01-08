using msakac_zadaca_3.Aplikacija;
using msakac_zadaca_3.Modeli;

namespace msakac_zadaca_3.Naredbe
{
    public class SpremanjePostojecegStanja : AbstractNaredba
    {
        public override void IzvrsiNaredbu(string naredba)
        {
            VirtualniSatProxy proxy = new VirtualniSatProxy();
            BrodskaLuka brodskaLuka = BrodskaLuka.Instanca();
            string[]? argumenti = naredba.Split('"');
            string nazivStanja = argumenti[1];
            DateTime trenutnoVrijeme = proxy.Dohvati();
            //provjerim da li vec postoji spremljeno stanje sa istim nazivom u listi
            Stanje? spremljenoStanje = brodskaLuka.listaSpremljenihStanja.Find(ss => ss.nazivStanja == nazivStanja);
            if (spremljenoStanje != null)
            {
                throw new Exception($"Stanje sa nazivom {nazivStanja} vec postoji u listi spremljenih stanja");
            }
            // ako ne postoji, spremi novo stanje u listu
            brodskaLuka.listaSpremljenihStanja.Add(new Stanje(nazivStanja, trenutnoVrijeme,brodskaLuka.listaRezervacija));
            IspisPoruke.Uspjeh($"Spremljeno trenutno stanje svih vezova u trenutku virtualnog vremena nazivom: {nazivStanja}");
        }
    }
}
