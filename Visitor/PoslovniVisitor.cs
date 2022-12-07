using msakac_zadaca_2.Modeli;
using msakac_zadaca_2.Aplikacija;

namespace msakac_zadaca_2.Visitor
{
    public class PoslovniVisitor : IVisitor
    {
        private DateTime datumVrijemeProvjere;

        public PoslovniVisitor(DateTime datumVrijemeProvjere)
        {
            this.datumVrijemeProvjere = datumVrijemeProvjere;
        }
        public Vez? Visit(Element element)
        {
            Rezervacija rezervacija = (Rezervacija) element;
            BrodskaLuka brodskaLuka = BrodskaLuka.Instanca();
            Vez vez = brodskaLuka.listaVezova.Find(v => v.Id == rezervacija.IdVez)!;
            if(vez.Vrsta.oznakaVeza == OznakaVrsteVeza.PO && datumVrijemeProvjere >= rezervacija.DatumVrijemeOd && datumVrijemeProvjere <= rezervacija.DatumVrijemeDo){
                return vez;
            }
            return null;
        }
    }
}
