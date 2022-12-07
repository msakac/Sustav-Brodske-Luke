using msakac_zadaca_1.Aplikacija;
namespace msakac_zadaca_1.Naredbe
{
    public class VirtualnoVrijeme : AbstractNaredba
    {
        public override void IzvrsiNaredbu(string naredba)
        {
            VirtualniSatProxy proxy = new VirtualniSatProxy();
            string[]? argumenti = naredba.Split(' ');
            string novoVirtualnoVrijeme = argumenti[1] + " " + argumenti[2];
            DateTime novoVrijeme = DateTime.Parse(novoVirtualnoVrijeme);
            proxy.Postavi(novoVrijeme);
            IspisPoruke.Uspjeh($"Novo vrijeme virtualnog sata: {proxy.Dohvati()}");
        }
    }
}
