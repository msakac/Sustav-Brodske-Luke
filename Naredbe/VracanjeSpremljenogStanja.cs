using msakac_zadaca_3.Aplikacija;
using msakac_zadaca_3.Modeli;
namespace msakac_zadaca_3.Naredbe
{
    public class VracanjeSpremljenogStanja : AbstractNaredba
    {
        public override void IzvrsiNaredbu(string naredba)
        {
            VirtualniSatProxy proxy = new VirtualniSatProxy();
            BrodskaLuka brodskaLuka = BrodskaLuka.Instanca();
            string[]? argumenti = naredba.Split('"');
            string nazivStanja = argumenti[1];

            // provjerim da li vec postoji spremljeno stanje 
            Stanje? spremljenoStanje = brodskaLuka.listaSpremljenihStanja.Find(ss => ss.nazivStanja == nazivStanja);
            if (spremljenoStanje == null)
            {
                throw new Exception($"Stanje sa nazivom {nazivStanja} ne postoji u listi spremljenih stanja");
            }
            // ako postoji, postavi rezervacije na stanje iz liste spremljenih stanja i virtualno vrijeme na vrijeme iz liste spremljenih stanja
            brodskaLuka.listaRezervacija = spremljenoStanje.listaRezervacija;
            proxy.Postavi(spremljenoStanje.virtualnoVrijeme);
            IspisPoruke.Uspjeh($"Vraceno spremljeno stanje pod nazivom {nazivStanja} i virtualnim vremenom {spremljenoStanje.virtualnoVrijeme}");
        }
    }
}
