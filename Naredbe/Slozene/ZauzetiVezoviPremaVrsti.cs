using msakac_zadaca_1.Aplikacija;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace msakac_zadaca_1.Naredbe.Slozene
{
    public class ZauzetiVezoviPremaVrsti : AbstractSlozenaNaredba
    {
        public override void IzvrsiNaredbu(string naredba)
        {
            Console.WriteLine("Ispis ukupnog broja zauzetih vezova prema vrstama u odredeno vrijeme");
        }
    }
}
