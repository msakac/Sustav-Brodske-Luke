using msakac_zadaca_3.Modeli;
using msakac_zadaca_3.CsvCitac;
using msakac_zadaca_3.Naredbe;
using msakac_zadaca_3.Memento;
using msakac_zadaca_3.Pogledi;

namespace msakac_zadaca_3.Aplikacija
{
    public enum NaziviGrupa
    {
        brodovi
    }
    public class BrodskaLuka
    {
        //Singleton property
        private static readonly BrodskaLuka _instance = new BrodskaLuka();
        public readonly string trenutniDirektorij = Directory.GetCurrentDirectory() + "\\";
        private bool PodaciInicijalizirani = false;
        public List<Brod> listaBrodova = new List<Brod>();
        public Ispis? ispis;
        public List<Vez> listaVezova = new List<Vez>();
        public List<VrstaVeza> listaVrsteVezove = new List<VrstaVeza>();
        public List<Mol> listaMolova = new List<Mol>();
        public List<Kanal> listaKanala = new List<Kanal>();
        public List<StavkaRasporeda> listaStavkiRasporeda = new List<StavkaRasporeda>();
        public List<Rezervacija> listaRezervacija = new List<Rezervacija>();
        public List<StavkaDnevnika> listaStavkiDnevnika = new List<StavkaDnevnika>();
        public Caretaker caretaker = new Caretaker();
        private VirtualniSatProxy proxy = new VirtualniSatProxy();

        private Luka? _luka;
        public Luka? luka
        {
            get { return _luka; }
            set
            {
                if (_luka == null)
                {
                    _luka = value;
                }
                else
                {
                    throw new Exception("Jedna luka vec postoji u sustavu!");
                }
            }
        }

        private BrodskaLuka()
        {

        }
        public static BrodskaLuka Instanca()
        {
            return _instance;
        }
        public void InicijalizirajPodatke(List<KeyValuePair<string, string>> listaRegexGrupaIDatoteka)
        {
            if (!PodaciInicijalizirani)
            {
                kreirajVrsteVezova();
                foreach (KeyValuePair<string, string> group in listaRegexGrupaIDatoteka)
                {
                    if (group.Value != "")
                    {
                        CsvCitacCreator objekt = new CsvCitacConcreteCreator();
                        AbstractCsvCitac csvCitac = objekt.KreirajCitac(group.Key);
                        csvCitac.citajPodatke(group.Value);
                    }

                }
                proxy.Postavi(luka!.VirtualnoVrijeme);
                IspisPoruke.Uspjeh($"\nPodaci inicijalizirani! Virtualni sat postavljen na: {proxy.Dohvati().ToString()}");
                PodaciInicijalizirani = true;
                return;
            }
            Console.WriteLine("ERROR: Podaci su vec ucitani");
        }

        public void ObradiNaredbe(List<(string grupa, string komanda, int index)> listaRegexGrupaIVrijednosti)
        {
            bool prekiniRad = false;
            NaredbaCreator objekt = new NaredbeConcreteCreator();
            foreach ((string grupa, string komanda, int index) in listaRegexGrupaIVrijednosti)
            {
                //Ispisi virtualno vrijeme prije izvrsavanja naredbe
                proxy.IspisiVirtualnoVrijeme();
                // Ako je naredba za prekid rada, postavi prekiniRad na true
                if (grupa == "prekid_rada" && komanda != "")
                {
                    prekiniRad = true;
                    continue;
                }
                //Obrada naredbe
                AbstractNaredba naredba = objekt.KreirajNaredbu(grupa);
                naredba.IzvrsiNaredbu(komanda);
            }
            //ako je u naredbama bila naredba za prekid rada, prekini rad programa na kraju obrade naredbi
            if (prekiniRad)
            {
                AbstractNaredba naredba = objekt.KreirajNaredbu("prekid_rada");
                naredba.IzvrsiNaredbu("");
            }
        }
        private void kreirajVrsteVezova()
        {
            List<OznakaVrsteBroda> vrsteBrodova = new List<OznakaVrsteBroda>() {
                OznakaVrsteBroda.TR,
                OznakaVrsteBroda.KA,
                OznakaVrsteBroda.KL,
                OznakaVrsteBroda.KR
            };
            listaVrsteVezove.Add(new VrstaVeza("Putnicki", OznakaVrsteVeza.PU, vrsteBrodova));

            vrsteBrodova = new List<OznakaVrsteBroda>() {
                OznakaVrsteBroda.RI,
                OznakaVrsteBroda.TE
            };
            listaVrsteVezove.Add(new VrstaVeza("Poslovni", OznakaVrsteVeza.PO, vrsteBrodova));

            vrsteBrodova = new List<OznakaVrsteBroda>() {
                OznakaVrsteBroda.JA,
                OznakaVrsteBroda.BR,
                OznakaVrsteBroda.RO
            };
            listaVrsteVezove.Add(new VrstaVeza("Ostali", OznakaVrsteVeza.OS, vrsteBrodova));
        }
        public VrstaVeza DohvatiVrstuVeza(string vrsta)
        {
            VrstaVeza veza = listaVrsteVezove.Find(vrstaVeza => Enum.GetName(vrstaVeza.oznakaVeza) == vrsta)!;
            if (veza == null)
            {
                throw new Exception(message: $"Vrsta veza '{vrsta}' ne postoji u listi!");
            }
            return veza;
        }
    }
}
