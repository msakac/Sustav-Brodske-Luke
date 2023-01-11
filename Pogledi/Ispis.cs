using msakac_zadaca_3.Aplikacija;

namespace msakac_zadaca_3.Pogledi
{
    public class Ispis
    {
        private int brojLinijaGornjiDio { get; set; }
        private int brojLinijaDonjiDio { get; set; }
        private bool gornjiDioJeRadni { get; set; } = true;
        private List<string> listaUpisa { get; set; } = new List<string>();
        private List<string> listaGresaka { get; set; } = new List<string>();
        public const string ANSI_ESC = "\x1b[";

        public Ispis(int ukupniBrojLinija, string omjer, string ulogaEkrana)
        {
            double omjerGornjiDio = Double.Parse(omjer.Split(":")[0]) / 100;
            double omjerDonjiDio = Double.Parse(omjer.Split(":")[1]) / 100;

            this.brojLinijaGornjiDio = (int)(ukupniBrojLinija * omjerGornjiDio);
            this.brojLinijaDonjiDio = (int)(ukupniBrojLinija * omjerDonjiDio);

            if (ulogaEkrana == "P:R")
            {
                this.gornjiDioJeRadni = false;
            }
            Console.Clear();
        }

        public void DodajUpis(string poruka)
        {
            listaUpisa.Add(poruka);
            AzurirajEkran();
        }

        public void DodajGresku(string poruka)
        {
            listaGresaka.Add(poruka);
            AzurirajEkran();
        }

        public void PorukaNaredbe()
        {
            int pozicija;
            if (gornjiDioJeRadni)
            {
                pozicija = brojLinijaGornjiDio;
            }
            else
            {
                pozicija = brojLinijaDonjiDio + brojLinijaGornjiDio + 1;
            }
            VirtualniSatProxy proxy = new VirtualniSatProxy();
            Console.Write(ANSI_ESC + $"{pozicija};0f");
            Console.Write(ANSI_ESC + "2K");
            proxy.IspisiVirtualnoVrijeme();
            Console.Write(ANSI_ESC + $"{pozicija + 1};0f");
            Console.Write(ANSI_ESC + "2K");
            Console.Write("=====> Unesi naredbu: ");
        }
        private void AzurirajEkran()
        {
            string linija = string.Concat(Enumerable.Repeat("=", Console.WindowWidth));
            Thread.Sleep(40);
            IspisiLiniju(0);
            string bojaGornja ="32m"; //zelena
            string bojaDonja = "31m"; //crvena
            int petljaGornjiDioDo = brojLinijaGornjiDio - 1;
            if(!gornjiDioJeRadni)
            {
                bojaGornja = "31m";
                bojaDonja = "32m";
                petljaGornjiDioDo = brojLinijaGornjiDio + 1;
            }
            for (int i = 2; i <= petljaGornjiDioDo; i++)
            {
                IspisiGornjiDio(i, bojaGornja);
            }

            IspisiLiniju(brojLinijaGornjiDio + 2);

            for (int i = brojLinijaGornjiDio + 3; i <= brojLinijaGornjiDio + 2 + brojLinijaDonjiDio; i++)
            {
                IspisiDonjiDio(i, bojaDonja);
            }

            IspisiLiniju(brojLinijaGornjiDio + brojLinijaDonjiDio + 3);
        }

        private void IspisiGornjiDio(int i, string boja)
        {
            Console.Write(ANSI_ESC + $"{i};0f");
            string ispis = "";
            try
            {
                if (gornjiDioJeRadni)
                {
                    ispis = listaUpisa[listaUpisa.Count - brojLinijaGornjiDio + i];
                }
                else
                {
                    ispis = listaGresaka[listaGresaka.Count - brojLinijaGornjiDio + i - 2];
                }

            }
            catch
            {
                return;
            }
            Console.Write(ANSI_ESC + "2K");
            Console.Write(ANSI_ESC + boja + ispis + ANSI_ESC + "0m");
        }
        private void IspisiDonjiDio(int i, string boja)
        {
            Console.Write(ANSI_ESC + $"{i};0f");
            string ispis = "";
            try
            {
                if (gornjiDioJeRadni)
                {
                    ispis = listaGresaka[listaGresaka.Count - brojLinijaDonjiDio + i - brojLinijaGornjiDio - 3];
                }
                else
                {
                    ispis = listaUpisa[listaUpisa.Count - brojLinijaDonjiDio + i - brojLinijaGornjiDio - 1];
                }
            }
            catch
            {
                return;
            }
            Console.Write(ANSI_ESC + "2K");
            Console.Write(ANSI_ESC + boja + ispis + ANSI_ESC + "0m");
        }

        private void IspisiLiniju(int pozicija)
        {
            string linija = string.Concat(Enumerable.Repeat("=", Console.WindowWidth));
            Console.Write(ANSI_ESC + $"{pozicija};0f");
            Console.Write(ANSI_ESC + "33m" + linija + ANSI_ESC + "0m");
            Console.Write(ANSI_ESC + $"{pozicija+1};0f");
        }
    }
}




