using msakac_zadaca_3.Aplikacija;
using msakac_zadaca_3.Modeli;

namespace msakac_zadaca_3.Naredbe
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
