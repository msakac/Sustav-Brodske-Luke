namespace msakac_zadaca_2.Aplikacija
{
    class Tablica
    {
        private static Tablica? instanca;
        private Boolean Zaglavlje = false;
        private Boolean Podnozje = false;
        private Boolean RedniBroj = false;
        public static Tablica Instanca
        {
            get
            {
                if (instanca == null)
                {
                    instanca = new Tablica();
                }
                return instanca;
            }
        }
        public void UrediIspisPodataka(string[] opcije)
        {
            foreach (string opcija in opcije)
            {
                if (opcija == "Z")
                {
                    Zaglavlje = !Zaglavlje;
                }
                if (opcija == "P")
                {
                    Podnozje = !Podnozje;
                }
                if (opcija == "RB")
                {
                    RedniBroj = !RedniBroj;
                }
            }
            IspisPoruke.Uspjeh($"Ispis tablica sa elementima: \n\tZaglavlje: " +
                $"{VratiVrijednost(Zaglavlje)}, \n\tPodnozje: {VratiVrijednost(Podnozje)}, \n\tRedni broj: {VratiVrijednost(RedniBroj)} ");
        }

        public void IspisiTablicu(string nazivTablice, string[] naziviStupaca, List<String[]> podaci, int sirinaStupca = 25)
        {
            // Boolean redniBrojAktivan = false;
            //ako je redni broj aktivan onda imam jedan više stupac i redni broj u nazivima stupaca
            int brojStupaca = naziviStupaca.Count();
            string formatRedka = "";
            string[] podatak = podaci[0];
            if (RedniBroj)
            {
                brojStupaca++;
                naziviStupaca = naziviStupaca.Prepend("Redni broj").ToArray();
                podatak = podatak.Prepend("1.").ToArray();
            }
            //kreiran format ispisa za redak
            for (int i = 0; i < brojStupaca; i++)
            {
                string predznak = "-";
                bool jeNumerican = double.TryParse(podatak[i], out double n);
                if (jeNumerican)
                {
                    predznak = "";
                }
                formatRedka += "|{" + i + "," + predznak + sirinaStupca + "}";
            }
            formatRedka += "|";

            //sirinaStupca za svaki stupac puta broj stupaca + broj stupaca + 1 za znak |
            int brojZnakova = sirinaStupca * brojStupaca + brojStupaca + 1;
            //-2 jer je | na početku i na kraju
            int brojZnakovaZaNaslov = brojZnakova - 2;
            string linija = "|" + string.Concat(Enumerable.Repeat("=", brojZnakova - 2)) + "|";
            //Ispisujem zaglavlje ako je aktivan
            if (Zaglavlje)
            {
                IspisPoruke.Uspjeh(linija);
                string ispisNaslovaTablice = String.Format("|{0}|", centrirajIspis(nazivTablice, brojZnakovaZaNaslov));
                IspisPoruke.PorukaKanala(ispisNaslovaTablice);
                IspisPoruke.Uspjeh(linija);
                string ispisNaslovaRedaka = String.Format(formatRedka, naziviStupaca);
                IspisPoruke.Uspjeh(ispisNaslovaRedaka);
                IspisPoruke.Uspjeh(linija);
            }
            //Ispis svih podataka
            int brojac = 0;
            foreach (string[] redak in podaci)
            {
                string[] redakZaIspis = redak;
                brojac++;
                if (RedniBroj)
                {
                    redakZaIspis = redak.Prepend(brojac.ToString() + ".").ToArray();
                }
                try
                {
                    IspisPoruke.Uspjeh(string.Format(formatRedka, redakZaIspis));
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }

            }

            //ispis podnozja ako je aktivan
            if (Podnozje)
            {
                IspisPoruke.Uspjeh(linija);
                string IspisPodnozjaTablice = String.Format("|{0}|", centrirajIspis($"Ukupan broj podataka: {brojac}", brojZnakovaZaNaslov));
                IspisPoruke.PorukaKanala(IspisPodnozjaTablice);
                IspisPoruke.Uspjeh(linija);
            }
        }

        private string centrirajIspis(string s, int velicina)
        {
            if (s.Length >= velicina)
            {
                return s;
            }

            int lijevoPomak = (velicina - s.Length) / 2;
            int desnoPomak = velicina - s.Length - lijevoPomak;

            return new string(' ', lijevoPomak) + s + new string(' ', desnoPomak);
        }

        //ako je true vrati uključeno inače isključeno
        private string VratiVrijednost(Boolean b)
        {
            if (b)
            {
                return "Uključeno";
            }
            else
            {
                return "Isključeno";
            }
        }
    }
}
