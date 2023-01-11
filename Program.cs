using msakac_zadaca_3.Aplikacija;
using msakac_zadaca_3.Pogledi;
using System.Text.RegularExpressions;

class Zadaca1
{
    private static readonly string _regexDatoteka = @"(^(?:(?:(-l (?<luke>[0-9a-zA-Z_-]{3,}\.csv)\s?){1}()|(-m (?<molovi>[0-9a-zA-Z_-]{3,}\.csv)\s?){1}()|" +
        @"(-k (?<kanali>[0-9a-zA-Z_-]{3,}\.csv)\s?){1}()|(-v (?<vezovi>[0-9a-zA-Z_-]{3,}\.csv)\s?){1}()|(-mv (?<molvez>[0-9a-zA-Z_-]{3,}\.csv)\s?){1}()|" +
        @"(-b (?<brodovi>[0-9a-zA-Z_-]{3,}\.csv)\s?){1}()|(-br (?<broj_redova>2[4-9]|[3-7][0-9]|80)\s?){1}()|(-vt (?<omjer>75:25|50:50|25:75)\s?){1}()|(-pd (?<uloge_ekrana>R:P|P:R)\s?){1}){9}()|(-r (?<raspored>[0-9a-zA-Z_-]{3,}\.csv)\s?){0,1}){1,2}$)";
    private static readonly string _regexNaredba = @"(^((?<status_vezova>I\s?)?()|(?<prekid_rada>Q\s?)?()|(?<vrijeme>VR (\d{2}\.\d{2}\.\d{4}\. \d{2}\:\d{2}\:\d{2})\s?)?()" +
                @"|(?<vezovi_po_vrsti>V (?:PU|PO|OS) (:?S|Z) (\d{2}\.\d{2}\.\d{4}\. \d{2}\:\d{2}\:\d{2}) (\d{2}\.\d{2}\.\d{4}\. \d{2}\:\d{2}\:\d{2})\s?)?()|" +
                @"(?<datoteka_zahtjeva>UR [0-9a-zA-Z_-]{1,}\.csv\s?)?()|(?<kreiranje_rezerviranog_zahtjev>ZD [0-9]{1,}\s?)?()|" +
                @"(?<kreiranje_zahtjeva>ZP [0-9]{1,} [0-9]{1,}\s?)?()|(?<komunikacija_brod_kanal>F [0-9]{1,} [0-9]{1,}( [Q])?\s?)?()|" +
                @"(?<format_ispisa_tablica>T(( P)?()|( Z)?()|( RB)?\s?){1,3})?()|(?<zauzeti_vezovi_prema_vrsti>ZA (\d{2}\.\d{2}\.\d{4}\. \d{2}\:\d{2})\s?)?()|(?<ispis_podataka>VF(( R)?()|( B)?()|( M)?()|( K)?()|( D)?\s?){1,8})?()|(?<spremanje_stanja>SPS ""(.){1,}"")?()|(?<vracanje_stanja>VPS ""(.){1,}"")?)*$)";
    static public void Main(String[] args)
    {
        string? naredba = KreirajNaredbu(args);
        Regex regex = new Regex(_regexDatoteka);
        Match match = regex.Match(naredba);
        if (!regex.IsMatch(naredba) || naredba.Length == 0)
        {
            Console.WriteLine("Uneseni argumenti naredbe nisu ispravni");
        }
        //Dohvatim sve vrijednosti iz unesene naredbe
        List<KeyValuePair<string, string>> listaRegexGrupaIVrijednosti = DohvatiRegexGrupuIVrijednost(regex, match);
        //Dohvatim vrijednosti koje su povezane uz postavke emulatora
        List<KeyValuePair<string, string>> listaKonfiguracijeEmulatora = DohvatiArgumenteKonfiguracija(listaRegexGrupaIVrijednosti);
        //Obrisem postavke emulatora iz liste
        listaRegexGrupaIVrijednosti.RemoveAll(l => l.Key == "broj_redova" || l.Key == "omjer" || l.Key == "uloge_ekrana");

        //kreiram view
        Ispis ispis = KreirajIspis(listaKonfiguracijeEmulatora);

        BrodskaLuka brodskaLuka = BrodskaLuka.Instanca();
        brodskaLuka.ispis = ispis;
        brodskaLuka.InicijalizirajPodatke(listaRegexGrupaIVrijednosti);
        VirtualniSatProxy proxy = new VirtualniSatProxy();
        Timer t = new Timer(proxy.Tick!, null, 0, 1000);

        while (true)
        {
            try
            {
                brodskaLuka.ispis!.PorukaNaredbe();
                naredba = Console.ReadLine();
                regex = new Regex(_regexNaredba);
                match = regex.Match(naredba!);
                if (!match.Success)
                {
                    throw new Exception($"Naredba '{naredba}' nije ispravnog formata");
                }
                List<(string grupa, string komanda, int index)> list = DohvatiISortirajNaredbe(regex, match);
                brodskaLuka.ObradiNaredbe(list);
            }
            catch (Exception e)
            {
                Greska.Instanca.IspisiGresku(e, null);
            }
        }
    }
    private static string KreirajNaredbu(string[] argumenti)
    {
        string naredba = "";
        foreach (string argument in argumenti)
        {
            naredba += argument;
            naredba += " ";
        }
        return naredba;
    }
    private static List<KeyValuePair<string, string>> DohvatiRegexGrupuIVrijednost(Regex regex, Match match)
    {
        GroupCollection grupe = match.Groups;
        List<KeyValuePair<string, string>> listaNazivaGrupaIDatoteka = new List<KeyValuePair<string, string>>();
        foreach (string nazivGrupe in regex.GetGroupNames())
        {
            if (nazivGrupe.Length > 2)
                listaNazivaGrupaIDatoteka.Add(new KeyValuePair<string, string>(nazivGrupe, grupe[nazivGrupe].Value));
        }
        return listaNazivaGrupaIDatoteka;
    }

    private static List<KeyValuePair<string, string>> DohvatiArgumenteKonfiguracija(List<KeyValuePair<string, string>> listaRegexGrupaIVrijednosti)
    {
        List<KeyValuePair<string, string>> listaKonfiguracijeEmulatora = new List<KeyValuePair<string, string>>();
        foreach (KeyValuePair<string, string> par in listaRegexGrupaIVrijednosti)
        {
            if (par.Key == "broj_redova" || par.Key == "omjer" || par.Key == "uloge_ekrana")
            {
                listaKonfiguracijeEmulatora.Add(par);
            }
        }
        return listaKonfiguracijeEmulatora;
    }

    private static Ispis KreirajIspis(List<KeyValuePair<string, string>> listaKonfiguracijeEmulatora)
    {
        int brojRedova = 0;
        string omjer = "", ulogaEkrana = "";
        foreach (KeyValuePair<string, string> par in listaKonfiguracijeEmulatora)
        {
            if (par.Key == "broj_redova")
            {
                brojRedova = int.Parse(par.Value);
            }
            else if (par.Key == "omjer")
            {
                omjer = par.Value;
            }
            else if (par.Key == "uloge_ekrana")
            {
                ulogaEkrana = par.Value;
            }
        }
        Ispis ispis = new Ispis(brojRedova, omjer, ulogaEkrana);
        return ispis;
    }

    private static List<(string grupa, string komanda, int index)> DohvatiISortirajNaredbe(Regex regex, Match match)
    {
        List<(string grupa, string komanda, int index)> list = new List<(string grupa, string komanda, int index)>();
        foreach (Group group in match.Groups)
        {
            // that have captures and names which are not numbers
            if (group.Success && !int.TryParse(group.Name, out int ignore))
            {
                // Add all Captures with group name, match value and match index
                foreach (Capture capture in group.Captures)
                {
                    list.Add((group.Name, capture.Value, capture.Index));
                }
            }
        }
        list = list.OrderBy(l => l.index).ToList();
        return list;
    }
}