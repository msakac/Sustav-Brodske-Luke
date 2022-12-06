using msakac_zadaca_1.Modeli;
using msakac_zadaca_1.CsvCitac;
using msakac_zadaca_1.Naredbe.Jednostavne;
using msakac_zadaca_1.Naredbe.Slozene;
using msakac_zadaca_1.Naredbe;

namespace msakac_zadaca_1.Aplikacija
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
        public List<Vez> listaVezova = new List<Vez>();
        public List<VrstaVeza> listaVrsteVezove = new List<VrstaVeza>();
        public List<Mol> listaMolova = new List<Mol>();
        public List<Kanal> listaKanala = new List<Kanal>();
        public List<StavkaRasporeda> listaStavkiRasporeda = new List<StavkaRasporeda>();
        public List<Rezervacija> listaRezervacija = new List<Rezervacija>();
        public List<StavkaDnevnika> listaStavkiDnevnika = new List<StavkaDnevnika>();
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

        public void ObradiNaredbe(List<KeyValuePair<string, string>> listaRegexGrupaIVrijednosti)
        {
            NaredbaFactory? factory = null;
            bool prekiniRad = false;
            foreach (KeyValuePair<string, string> group in listaRegexGrupaIVrijednosti)
            {
                // Ako je neka naredba prazna, preskoci
                if (group.Value == "") continue;

                // Ako je naredba za prekid rada, postavi prekiniRad na true
                if (group.Key == "prekid_rada" && group.Value != "")
                {
                    prekiniRad = true;
                    continue;
                }

                proxy.IspisiVirtualnoVrijeme();

                // Ako je jednostavna naredba, stvori jednostavnu naredbu
                if (group.Key == "status_vezova")
                {
                    factory = new JednostavnaNaredbaFactory();
                    AbstractJednostavnaNaredba naredba = factory.KreirajJednostavnuNaredbu(group.Key);
                    naredba.IzvrsiNaredbu();

                }
                
                // Ako je slozena naredba, stvori slozenu naredbu
                else
                {
                    
                    factory = new SlozenaNaredbaFactory();
                    AbstractSlozenaNaredba naredba = factory.KreirajSlozenuNaredbu(group.Key);
                    naredba.IzvrsiNaredbu(group.Value);
                }

            }

            if (prekiniRad)
            {
                factory = new JednostavnaNaredbaFactory();
                AbstractJednostavnaNaredba naredba = factory.KreirajJednostavnuNaredbu("prekid_rada");
                naredba.IzvrsiNaredbu();
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
