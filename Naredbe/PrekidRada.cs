using msakac_zadaca_2.Aplikacija;
using msakac_zadaca_2.Modeli;

namespace msakac_zadaca_2.Naredbe
{
    public class PrekidRada : AbstractNaredba
    {
        public override void IzvrsiNaredbu(string naredba)
        {
            IspisPoruke.Uspjeh("\nPrekidam rad aplikacije!");
            Environment.Exit(0);
        }
    }
}
