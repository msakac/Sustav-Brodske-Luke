using msakac_zadaca_1.Aplikacija;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace msakac_zadaca_1.Naredbe.Jednostavne
{
    public class PrekidRada : AbstractJednostavnaNaredba
    {
        public override void IzvrsiNaredbu()
        {
            IspisPoruke.Uspjeh("\nPrekidam rad aplikacije!");
            Environment.Exit(0);
        }
    }
}
