using msakac_zadaca_3.Aplikacija;
namespace msakac_zadaca_3.Naredbe
{
    public class VirtualnoVrijeme : AbstractNaredba
    {
        public override void IzvrsiNaredbu(string naredba)
        {
            BrodskaLuka brodskaLuka = BrodskaLuka.Instanca();
            VirtualniSatProxy proxy = new VirtualniSatProxy();
            string[]? argumenti = naredba.Split(' ');
            string novoVirtualnoVrijeme = argumenti[1] + " " + argumenti[2];
            DateTime novoVrijeme = DateTime.Parse(novoVirtualnoVrijeme);
            proxy.Postavi(novoVrijeme);
            brodskaLuka.ispis!.DodajUpis($"Novo vrijeme virtualnog sata: {proxy.Dohvati()}");
        }
    }
}
