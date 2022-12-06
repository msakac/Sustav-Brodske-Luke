using msakac_zadaca_1.Aplikacija;
using System.Text.RegularExpressions;

class Zadaca1
{
    private static readonly string _regexDatoteka = @"(^(?:(?:(-l (?<luke>[0-9a-zA-Z_-]{3,}\.csv)\s?){1}()|(-m (?<molovi>[0-9a-zA-Z_-]{3,}\.csv)\s?){1}()|" +
        @"(-k (?<kanali>[0-9a-zA-Z_-]{3,}\.csv)\s?){1}()|(-v (?<vezovi>[0-9a-zA-Z_-]{3,}\.csv)\s?){1}()|(-mv (?<molvez>[0-9a-zA-Z_-]{3,}\.csv)\s?){1}()|" +
        @"(-b (?<brodovi>[0-9a-zA-Z_-]{3,}\.csv)\s?){1}){6}()|(-r (?<raspored>[0-9a-zA-Z_-]{3,}\.csv)\s?){0,1}){1,2}$)";
    private static readonly string _regexNaredba = @"(^((?<status_vezova>I\s?)?()|(?<prekid_rada>Q\s?)?()|(?<vrijeme>VR (\d{2}\.\d{2}\.\d{4}\. \d{2}\:\d{2}\:\d{2})\s?)?()" +
                @"|(?<vezovi_po_vrsti>V (?:PU|PO|OS) (:?S|Z) (\d{2}\.\d{2}\.\d{4}\. \d{2}\:\d{2}\:\d{2}) (\d{2}\.\d{2}\.\d{4}\. \d{2}\:\d{2}\:\d{2})\s?)?()|" +
                @"(?<datoteka_zahtjeva>UR [0-9a-zA-Z_-]{1,}\.csv\s?)?()|(?<kreiranje_rezerviranog_zahtjev>ZD [0-9]{1,}\s?)?()|" +
                @"(?<kreiranje_zahtjeva>ZP [0-9]{1,} [0-9]{1,}\s?)?()|(?<komunikacija_brod_kanal>F [0-9]{1,} [0-9]{1,}( [Q])?\s?)?()|" +
                @"(?<format_ispisa_tablica>T(( P)?()|( Z)?()|( RB)?\s?){1,3})?()|(?<zauzeti_vezovi_prema_vrsti>ZA (\d{2}\.\d{2}\.\d{4}\. \d{2}\:\d{2})\s?)?()|(?<ispis_podataka>VF(( R)?()|( B)?()|( M)?()|( K)?()|( D)?\s?){1,8})?)*$)";
    static public void Main(String[] args)
    {
        string? naredba = KreirajNaredbu(args);
        Regex regex = new Regex(_regexDatoteka);
        Match match = regex.Match(naredba);
        if (!regex.IsMatch(naredba) || naredba.Length == 0)
        {
            IspisPoruke.FatalnaGreska("Uneseni argumenti naredbe nisu ispravni");
        }

        List<KeyValuePair<string, string>> listaRegexGrupaIVrijednosti = DohvatiRegexGrupuIVrijednost(regex, match);
        BrodskaLuka brodskaLuka = BrodskaLuka.Instanca();
        brodskaLuka.InicijalizirajPodatke(listaRegexGrupaIVrijednosti);
        VirtualniSatProxy proxy = new VirtualniSatProxy();
        Timer t = new Timer(proxy.Tick!, null, 0, 1000);

        while (true)
        {
            try
            {
                Console.Write("\nNaredba: ");
                naredba = Console.ReadLine();
                regex = new Regex(_regexNaredba);
                match = regex.Match(naredba!);
                if (!regex.IsMatch(naredba!))
                {
                    throw new Exception($"Naredba '{naredba}' nije ispravnog formata");
                }
                listaRegexGrupaIVrijednosti.Clear();
                listaRegexGrupaIVrijednosti = DohvatiRegexGrupuIVrijednost(regex, match);
                brodskaLuka.ObradiNaredbe(listaRegexGrupaIVrijednosti);
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
}