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

        private void AzurirajEkran()
        {
            Thread.Sleep(50);
            Console.Write(ANSI_ESC + "2J");
            for (int i = 0; i <= brojLinijaGornjiDio; i++)
            {
                Console.Write(ANSI_ESC + $"{i};0f");
                string ispis = "";
                try
                {
                    ispis = listaUpisa[listaUpisa.Count - brojLinijaGornjiDio + i - 1];
                }
                catch
                {
                    continue;
                }
                Console.Write(ANSI_ESC + "32m" + ispis + ANSI_ESC + "0m");
            }
            for (int i = brojLinijaGornjiDio + 1; i <= brojLinijaGornjiDio + brojLinijaDonjiDio; i++)
            {
                Console.Write(ANSI_ESC + $"{i};0f");
                string ispis = "";
                try
                {
                    ispis = listaGresaka[listaGresaka.Count - 1 - brojLinijaDonjiDio + i - brojLinijaGornjiDio];
                }
                catch
                {
                    continue;
                }
                Console.Write(ANSI_ESC + "31m" + ispis + ANSI_ESC + "0m");
            }
        }
    }
}

//Trebam imati dvije liste stringova, jedna lista je za sve upise i uspjesne ispise i druga lista za sve greske
//Na ekranu prikazujem onoliko poruka koliko ih stane prema konfiguracijama0
//Tri argumenta
//1. -br {x} 24<=x<=80            => ukupni broj linija na ekranu
//2. -vt {50:50 | 25:75 | 75:25}  => odnos izmedju linija za upise i linija za greske
//3. -pd {R:P | P:R}              => koji dio ekrana sluzi za upise i koji za greske

//Jedna metoda koja sadrzi listu u koju dodajem upise i uspjesne ispise
//Druga metoda koja sadrzi listu u koju dodajem greske
//I zadnja metoda koja jednostavno sluzi za ispis i prikazuje onolko podatki kolko je parametrima odredeno


