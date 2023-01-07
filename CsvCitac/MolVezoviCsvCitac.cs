using msakac_zadaca_3.Aplikacija;
using msakac_zadaca_3.Modeli;

namespace msakac_zadaca_3.CsvCitac
{
    public class MolVezoviCsvCitac : AbstractCsvCitac
    {
        public override void citajPodatke(string datoteka)
        {
            Console.WriteLine($"\nMol-Vezovi | Učitavam datoteku: {datoteka}...");
            BrodskaLuka brodskaLuka = BrodskaLuka.Instanca();
            try
            {
                using StreamReader citac = new StreamReader(brodskaLuka.trenutniDirektorij + datoteka);
                string prviRedak = citac.ReadLine()!;
                int brojAtributa = prviRedak.Split(';').Count();
                int brojPropertija = typeof(Mol).GetProperties().Length;
                if (brojAtributa != brojPropertija)
                {
                    IspisPoruke.FatalnaGreska("Broj redaka u datoteci i atributa u klasi su razliciti!");
                }
                string redak;
                int ucitaniPodaci = 0;
                int dodanoMolovaVezovima = 0;
                while ((redak = citac.ReadLine()!) != null)
                {
                    try
                    {
                        string[]? podaci = redak?.Split(';');
                        int idMol = int.Parse(podaci![0]);
                        Mol? mol = brodskaLuka.listaMolova.Find(m => m.Id == idMol);
                        if (mol == null)
                        {
                            throw new Exception($"Mol id {idMol} ne postoji u listi molova!");
                        }
                        int[] vezovi = podaci[1].Split(',').Select(int.Parse).ToArray();
                        dodanoMolovaVezovima += dodajMolVezovima(vezovi, mol);
                        ucitaniPodaci++;
                    }
                    catch (Exception e)
                    {
                        Greska.Instanca.IspisiGresku(e, redak);
                    }
                }
                int brojVezova = brodskaLuka.listaVezova.Count();
                obrisiVezoveBezMola();
                IspisPoruke.Uspjeh($"|===== Od {brojVezova} vezova, njih {dodanoMolovaVezovima} sada ima mol, a {brojVezova - dodanoMolovaVezovima} je obrisano");
                IspisPoruke.Uspjeh($"|===== Učitano {ucitaniPodaci.ToString()} ispravnih redaka iz datoteke {datoteka} ");
            }
            catch
            {
                IspisPoruke.FatalnaGreska($"Datoteku {datoteka} nije moguće pročitati ili ne postoji u direktoriju!");
            }
        }

        private int dodajMolVezovima(int[] vezovi, Mol mol)
        {
            int brojDodanihMolovaVezovima = 0;
            BrodskaLuka brodskaLuka = BrodskaLuka.Instanca();
            foreach (int vez in vezovi)
            {
                try
                {
                    Vez? v = brodskaLuka.listaVezova.Find(v => v.Id == vez);
                    if (v == null)
                    {
                        throw new Exception($"Vez id {vez} ne postoji u listi vezova!");
                    }
                    if (v.Mol != null)
                    {
                        throw new Exception($"Vez id {vez} već je dodijeljen molu id {v.Mol.Id}!");
                    }
                    brodskaLuka.listaVezova.Find(v => v.Id == vez)!.Mol = mol;
                    brojDodanihMolovaVezovima++;
                }
                catch (Exception e)
                {
                    Greska.Instanca.IspisiGresku(e, null);
                }

            }
            return brojDodanihMolovaVezovima;
        }

        private void obrisiVezoveBezMola()
        {
            BrodskaLuka brodskaLuka = BrodskaLuka.Instanca();
            List<Vez> listaVezovaZaBrisanje = brodskaLuka.listaVezova.FindAll(v => v.Mol == null);
            foreach (Vez v in listaVezovaZaBrisanje)
            {
                Exception e = new Exception($"Vez id {v.Id} nema mol te je obrisan iz liste molova!");
                Greska.Instanca.IspisiGresku(e, null);
                brodskaLuka.listaVezova.Remove(v);
            }
        }
    }
}
