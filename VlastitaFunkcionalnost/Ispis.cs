using msakac_zadaca_1.Aplikacija;

namespace msakac_zadaca_1.VlastitaFunkcionalnost
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
