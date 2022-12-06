using msakac_zadaca_1.Naredbe.Jednostavne;
using msakac_zadaca_1.Naredbe.Slozene;

namespace msakac_zadaca_1.Naredbe
{
    public abstract class NaredbaFactory
    {
        public abstract AbstractJednostavnaNaredba KreirajJednostavnuNaredbu(string tip);
        public abstract AbstractSlozenaNaredba KreirajSlozenuNaredbu(string tip);
    }
}
