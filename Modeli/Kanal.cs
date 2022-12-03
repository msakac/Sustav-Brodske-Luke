using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using msakac_zadaca_1.Aplikacija;

namespace msakac_zadaca_1.Modeli
{
    public class Kanal
    {
        public int Id { get; set; }
        public int Frekvecija { get; set; }
        public int MaksimalanBroj { get; set; }

        public Kanal(int id, int frekvecija, int maksimalanBroj)
        {
            Id = id;
            Frekvecija = frekvecija;
            MaksimalanBroj = maksimalanBroj;
        }

        public void DodajUListuKanala()
        {
            BrodskaLuka brodskaLuka = BrodskaLuka.Instanca();
            int index = brodskaLuka.listaKanala.FindIndex(kanal => kanal.Id == this.Id);
            if (index >= 0)
            {
                throw new Exception($"Kanal sa ID-om {this.Id} vec postoji u listi");
            }
            index = brodskaLuka.listaKanala.FindIndex(kanal => kanal.Frekvecija == this.Frekvecija);
            if (index >= 0)
            {
                throw new Exception($"Kanal sa frekvencijom {this.Frekvecija} vec postoji u listi");
            }
            brodskaLuka.listaKanala.Add(this);
        }
    }
}
