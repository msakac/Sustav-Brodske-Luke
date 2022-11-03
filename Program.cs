using msakac_zadaca_1.Aplikacija;
using System.Security.Cryptography;
using System.Text.RegularExpressions;

class Zadaca1
{
    static public void Main(String[] args)
    {
        string? naredba = KreirajNaredbu(args);
        string regexIzraz = @"(^(?:(?:(-l (?<luke>[0-9a-zA-Z_-]{3,}\.csv)\s?){1}()|(-v (?<vezovi>[0-9a-zA-Z_-]{3,}\.csv)\s?){1}()|" +
        @"(-b (?<brodovi>[0-9a-zA-Z_-]{3,}\.csv)\s?){1}){3}()|(-r (?<raspored>[0-9a-zA-Z_-]{3,}\.csv)\s?){0,1}){1,2}$)";
        Regex regex = new Regex(regexIzraz);
        Match match = regex.Match(naredba);
        if (!regex.IsMatch(naredba))
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
                regexIzraz = @"(^((?<status_vezova>I\s?)?()|(?<prekid_rada>Q\s?)?()|(?<vrijeme>VR (\d{2}\.\d{2}\.\d{4}\. \d{2}\:\d{2}\:\d{2})\s?)?()|(?<vezovi_po_vrsti>V (?:PU|PO|OS) (:?S|Z) (\d{2}\.\d{2}\.\d{4}\. \d{2}\:\d{2}\:\d{2}) (\d{2}\.\d{2}\.\d{4}\. \d{2}\:\d{2}\:\d{2})\s?)?()|(?<datoteka_zahtjeva>UR [0-9a-zA-Z_-]{1,}\.csv\s?)?()|(?<kreiranje_rezerviranog_zahtjev>ZD [0-9]{1,}\s?)?()|(?<kreiranje_zahtjeva>ZP [0-9]{1,} [0-9]{1,}\s?)?)*$)";
                naredba = Console.ReadLine();
                regex = new Regex(regexIzraz);
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