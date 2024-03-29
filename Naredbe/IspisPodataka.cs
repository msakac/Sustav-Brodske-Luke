using msakac_zadaca_3.Aplikacija;
using msakac_zadaca_3.VlastitaFunkcionalnost;

namespace msakac_zadaca_3.Naredbe
{
    public class IspisPodataka : AbstractNaredba
    {
        public override void IzvrsiNaredbu(string naredba)
        {
            string[]? argumenti = naredba.Split(' ');
            argumenti = argumenti.Skip(1).ToArray();
            if(argumenti.Count() == 0){
                throw new Exception($"Naredba '{naredba}' nije ispravnog formata");
            }

            //Postavljanje Chain of responsibility
            Ispis brodovi = new Brodovi();
            Ispis dnevnik = new DnevnikRada();
            Ispis kanali = new Kanali();
            Ispis molovi = new Molovi();
            Ispis raspored = new Raspored();

            brodovi.SetNasljednik(dnevnik);
            dnevnik.SetNasljednik(kanali);
            kanali.SetNasljednik(molovi);
            molovi.SetNasljednik(raspored);

            foreach (string argument in argumenti)
            {
                brodovi.ObradiZahtjev(argument);
            }
        }
    }
}
