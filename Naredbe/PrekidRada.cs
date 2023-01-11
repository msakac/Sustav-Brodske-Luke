using msakac_zadaca_3.Aplikacija;
using msakac_zadaca_3.Modeli;

namespace msakac_zadaca_3.Naredbe
{
    public class PrekidRada : AbstractNaredba
    {
        public override void IzvrsiNaredbu(string naredba)
        {
            BrodskaLuka brodskaLuka = BrodskaLuka.Instanca();
            brodskaLuka.ispis!.DodajUpis("\nPrekidam rad aplikacije!");
            Environment.Exit(0);
        }
    }
}
