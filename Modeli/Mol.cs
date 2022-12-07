using msakac_zadaca_2.Aplikacija;

namespace msakac_zadaca_2.Modeli
{
    public class Mol
    {
        public int Id { get; set; }
        public string Naziv { get; set; }

        public Mol(int id, string naziv)
        {
            Id = id;
            Naziv = naziv;
        }
        public void DodajUListuMolova()
        {
            BrodskaLuka brodskaLuka = BrodskaLuka.Instanca();
            int index = brodskaLuka.listaMolova.FindIndex(mol => mol.Id == this.Id);
            if (index >= 0)
            {
                throw new Exception($"Mol sa ID-om {this.Id} vec postoji u listi");
            }
            brodskaLuka.listaMolova.Add(this);
        }
    }

}
