using msakac_zadaca_3.Aplikacija;
using msakac_zadaca_3.Memento;
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

            Originator originator = new Originator();
            Console.WriteLine(brodskaLuka.listaRezervacija.Count);
            originator.listaRezervacija = new List<Rezervacija>(brodskaLuka.listaRezervacija);
            originator.virtualnoVrijeme = trenutnoVrijeme;
            originator.spremiStanje(nazivStanja);
        }
    }
}
