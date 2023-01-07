using msakac_zadaca_3.Aplikacija;

namespace msakac_zadaca_3.VlastitaFunkcionalnost
{
    public abstract class Ispis
    {
        protected Ispis? nasljednik;
        public void SetNasljednik(Ispis nasljednik)
        {
            this.nasljednik = nasljednik;
        }
        public abstract void ObradiZahtjev(string argument);
    }
}
