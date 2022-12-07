namespace msakac_zadaca_2.Aplikacija
{
    public class VirtualniSatProxy : AbstractVirtualniSat
    {
        private VirtualniSat virtualniSat = VirtualniSat.Instanca;
        public override void Postavi(DateTime virtualnoVrijeme)
        {
            virtualniSat.Postavi(virtualnoVrijeme);
        }
        public override void Tick(Object o)
        {
            virtualniSat.Tick(o);
        }
        public override DateTime Dohvati()
        {
            return virtualniSat.Dohvati();
        }
        public override void IspisiVirtualnoVrijeme()
        {
            virtualniSat.IspisiVirtualnoVrijeme();
        }
    }
}
