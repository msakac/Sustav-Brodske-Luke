namespace msakac_zadaca_3.Aplikacija
{
    public abstract class AbstractVirtualniSat
    {
        public abstract void Postavi(DateTime virtualnoVrijeme);
         public abstract void Tick(Object o);
         public abstract DateTime Dohvati();
         public abstract void IspisiVirtualnoVrijeme();
    }
}
