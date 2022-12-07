using msakac_zadaca_2.Visitor;

namespace msakac_zadaca_2.Modeli
{
    public class Rezervacija : Element
    {
        public int IdVez { get; set; }
        public int IdBrod { get; set; }
        public DateTime DatumVrijemeOd { get; set; }
        public DateTime DatumVrijemeDo { get; set; }

        public Rezervacija(int idVez, int idBrod, DateTime datumVrijemeOd, DateTime datumVrijemeDo)
        {
            IdVez = idVez;
            IdBrod = idBrod;
            DatumVrijemeOd = datumVrijemeOd;
            DatumVrijemeDo = datumVrijemeDo;
        }

        public override Vez? Accept(IVisitor visitor)
        {
            Vez? v = visitor.Visit(this);
            if (v != null)
            {
                return v;
            }
            return null;
        }

    }
}
