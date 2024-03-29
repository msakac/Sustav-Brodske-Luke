using msakac_zadaca_3.Modeli;
using msakac_zadaca_3.Aplikacija;

namespace msakac_zadaca_3.Visitor
{
    public class PutnickiVisitor : IVisitor
    {
        private DateTime datumVrijemeProvjere;

        public PutnickiVisitor(DateTime datumVrijemeProvjere)
        {
            this.datumVrijemeProvjere = datumVrijemeProvjere;
        }
        public Vez? Visit(Element element)
        {
            Rezervacija rezervacija = (Rezervacija) element;
            BrodskaLuka brodskaLuka = BrodskaLuka.Instanca();
            Vez vez = brodskaLuka.listaVezova.Find(v => v.Id == rezervacija.IdVez)!;
            if(vez.Vrsta.oznakaVeza == OznakaVrsteVeza.PU && datumVrijemeProvjere >= rezervacija.DatumVrijemeOd && datumVrijemeProvjere <= rezervacija.DatumVrijemeDo){
                return vez;
            }
            return null;
        }
    }
}
