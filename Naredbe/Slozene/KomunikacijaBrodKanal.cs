using msakac_zadaca_1.Aplikacija;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using msakac_zadaca_1.Modeli;

namespace msakac_zadaca_1.Naredbe.Slozene
{
    public class KomunikacijaBrodKanal : AbstractSlozenaNaredba
    {
        public override void IzvrsiNaredbu(string naredba)
        {
            string[]? argumenti = naredba.Split(' ');
            int idBrod = int.Parse(argumenti[1]);
            int idKanal = int.Parse(argumenti[2]);
            bool odjaviBrodSaKanal = false;
            if(argumenti.Length==4 && argumenti[3] == "Q") odjaviBrodSaKanal = true;

            BrodskaLuka brodskaLuka = BrodskaLuka.Instanca();
            Brod? brod = brodskaLuka.listaBrodova.Find(b => b.Id == idBrod);
            Kanal? kanal = brodskaLuka.listaKanala.Find(k => k.Id == idKanal);
            // provjeri da li postoji brod i kanal
            if(brod == null){
                throw new Exception($"Brod sa ID-om {idBrod} ne postoji u listi");
            }
            if(kanal == null){
                throw new Exception($"Kanal sa ID-om {idKanal} ne postoji u listi");
            }
            // ako je sve ok, spoji ili odjavi brod na kanal
            if(odjaviBrodSaKanal){
                kanal.OdjaviBrodSaKanala(brod);
                return;
            }
            kanal.SpojiBrodNaKanal(brod);
        }
    }
}
