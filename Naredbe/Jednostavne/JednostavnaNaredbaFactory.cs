using msakac_zadaca_1.CsvCitac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using msakac_zadaca_1.Naredbe.Slozene;

namespace msakac_zadaca_1.Naredbe.Jednostavne
{
    public class JednostavnaNaredbaFactory : NaredbaFactory
    {
        public override AbstractJednostavnaNaredba KreirajJednostavnuNaredbu(string tip)
        {
            switch (tip)
            {
                case "status_vezova":
                    return new StatusVezova();
                case "prekid_rada":
                    return new PrekidRada();
                default:
                    throw new Exception($"Jednostavna naredba {tip} nije moguca!");
            }
        }
        public override AbstractSlozenaNaredba KreirajSlozenuNaredbu(string tip)
        {
            throw new NotImplementedException();
        }
    }
}
