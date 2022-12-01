using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using msakac_zadaca_1.Modeli;
using System.IO;
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
        private bool PodaciInicijalizirani = false;
        public List<Brod> listaBrodova = new List<Brod>();
        public List<Vez> listaVezova = new List<Vez>();
        public List<VrstaVeza> listaVrsteVezove = new List<VrstaVeza>();
        public List<StavkaRasporeda> listaStavkiRasporeda = new List<StavkaRasporeda>();
        public List<Rezervacija> listaRezervacija = new List<Rezervacija>();
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
                if (group.Value != "")
                {
                    if (group.Key == "status_vezova" || group.Key == "prekid_rada")
                    {
                        if (group.Key == "prekid_rada")
                        {
                            prekiniRad = true;
                        }
                        else
                        {
                            proxy.IspisiVirtualnoVrijeme();
                            factory = new JednostavnaNaredbaFactory();
                            AbstractJednostavnaNaredba naredba = factory.KreirajJednostavnuNaredbu(group.Key);
                            naredba.IzvrsiNaredbu();
                        }

                    }
                    else
                    {
                        proxy.IspisiVirtualnoVrijeme();
                        factory = new SlozenaNaredbaFactory();
                        AbstractSlozenaNaredba naredba = factory.KreirajSlozenuNaredbu(group.Key);
                        naredba.IzvrsiNaredbu(group.Value);
                    }
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
        public void DodajUListuVezova(Vez vez)
        {
            listaVezova.Add(vez);
        }
        public List<Vez> DohvatiSveVezove()
        {
            return listaVezova;
        }
    }
}
