using msakac_zadaca_1.Modeli;
using msakac_zadaca_1.Aplikacija;

namespace msakac_zadaca_1.Visitor
{
    public class OstaliVisitor : IVisitor
    {
        private DateTime datumVrijemeProvjere;

        public OstaliVisitor(DateTime datumVrijemeProvjere)
        {
            this.datumVrijemeProvjere = datumVrijemeProvjere;
        }
        public Vez? Visit(Element element)
        {
            Rezervacija rezervacija = (Rezervacija)element;
            BrodskaLuka brodskaLuka = BrodskaLuka.Instanca();
            Vez vez = brodskaLuka.listaVezova.Find(v => v.Id == rezervacija.IdVez)!;
            if (vez.Vrsta.oznakaVeza == OznakaVrsteVeza.OS && datumVrijemeProvjere >= rezervacija.DatumVrijemeOd && datumVrijemeProvjere <= rezervacija.DatumVrijemeDo)
            {
                return vez;
            }
            return null;
        }
    }
}
