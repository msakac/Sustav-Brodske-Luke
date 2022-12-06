using msakac_zadaca_1.Visitor;

namespace msakac_zadaca_1.Modeli
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

        public override void Accept(IVisitor visitor){

        }

    }
}
