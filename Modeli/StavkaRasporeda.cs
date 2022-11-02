using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using msakac_zadaca_1.Aplikacija;

namespace msakac_zadaca_1.Modeli
{
    public class StavkaRasporeda
    {
        public int IdVez { get; set; }
        public int IdBrod { get; set; }
        public List<DayOfWeek> DaniUTjednu { get; set; }
        public TimeOnly VrijemeOd { get; set; }
        public TimeOnly VrijemeDo { get; set; }

        public StavkaRasporeda(int idVez, int idBrod, List<DayOfWeek> daniUTjednu, TimeOnly vrijemeOd, TimeOnly vrijemeDo)
        {
            IdVez = idVez;
            IdBrod = idBrod;
            DaniUTjednu = daniUTjednu;
            VrijemeOd = vrijemeOd;
            VrijemeDo = vrijemeDo;
        }

        public static DayOfWeek dohvatiDanTjedna(string dan)
        {
            try
            {
                DayOfWeek danTjedna = (DayOfWeek)Enum.Parse(typeof(DayOfWeek), dan);
                if (!Enum.IsDefined(typeof(DayOfWeek), danTjedna))
                {
                    throw new Exception();
                }
                return danTjedna;
            }
            catch
            {
                throw new Exception(message: $"Neispravna vrijednost dana '{dan}'");
            }
        }

        public void DodajURaspored()
        {
            BrodskaLuka brodskaLuka = BrodskaLuka.Instanca();
            Vez? vez = brodskaLuka.listaVezova.Find(vez => vez.Id == this.IdVez);
            Brod? brod = brodskaLuka.listaBrodova.Find(brod => brod.Id == this.IdBrod);
            if (vez == null)
            {
                throw new Exception($"Vez sa ID-om {this.IdVez} ne postoji u listi vezova!");
            }
            if (brod == null)
            {
                throw new Exception($"Brod sa ID-om {this.IdBrod} ne postoji u listi brodova!");
            }

            bool vrstaBrodaOdgovaraVezu = false;
            foreach (OznakaVrsteBroda oznaka in vez.Vrsta.oznakaVrsteBroda!)
            {
                if (oznaka == brod.Vrsta)
                {
                    vrstaBrodaOdgovaraVezu = true;
                }
            }
            if (!vrstaBrodaOdgovaraVezu)
            {
                throw new Exception($"Brod sa vrstom '{brod.Vrsta}' ne može biti vezan na '{vez.Vrsta.nazivVeza}' vez");
            }

            int postojiURasporedu = brodskaLuka.listaStavkiRasporeda.FindIndex(stavka =>
            stavka.IdBrod == this.IdBrod &&
            stavka.IdVez == this.IdVez &&
            stavka.VrijemeOd == this.VrijemeOd &&
            stavka.VrijemeDo == this.VrijemeDo);
            if (postojiURasporedu >= 0)
            {
                throw new Exception($"Brod '{this.IdBrod}' u terminu {this.VrijemeOd}-{this.VrijemeDo} vec ima rezerviran vez '{this.IdVez}'");
            }

            brodskaLuka.listaStavkiRasporeda.Add(this);
        }

    }
}
