using msakac_zadaca_3.Aplikacija;
using msakac_zadaca_3.Memento;
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

            Originator originator = new Originator();
            originator.vratiStanje(nazivStanja);

            Console.WriteLine(brodskaLuka.listaRezervacija.Count);
            brodskaLuka.listaRezervacija.Clear();
            brodskaLuka.listaRezervacija = originator.listaRezervacija!;
            Console.WriteLine(brodskaLuka.listaRezervacija.Count);
            proxy.Postavi(originator.virtualnoVrijeme);
            IspisPoruke.Uspjeh($"Vraceno spremljeno stanje pod nazivom {nazivStanja} i virtualnim vremenom {originator.virtualnoVrijeme}");
        }
    }
}
