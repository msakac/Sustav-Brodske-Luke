namespace msakac_zadaca_3.Aplikacija
{
    public class VirtualniSat : AbstractVirtualniSat
    {
        private static VirtualniSat? instanca;
        private DateTime vrijeme = new DateTime();
        public static VirtualniSat Instanca
        {
            get
            {
                if (instanca == null)
                {
                    instanca = new VirtualniSat();
                }
                return instanca;
            }
        }

        public override void  Postavi(DateTime virtualnoVrijeme) {
            vrijeme = new DateTime(virtualnoVrijeme.Year, virtualnoVrijeme.Month, virtualnoVrijeme.Day, virtualnoVrijeme.Hour, virtualnoVrijeme.Minute, virtualnoVrijeme.Second);
        }
        public override void  Tick(Object o){
            vrijeme = vrijeme.AddSeconds(1);
        }
        public override DateTime Dohvati(){
            return this.vrijeme;
        }
        public override void IspisiVirtualnoVrijeme(){
            Console.Write($"=====> Virtualni sat: {this.vrijeme}");
        }
    }
}
