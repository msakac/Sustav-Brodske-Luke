using msakac_zadaca_2.Aplikacija;

namespace msakac_zadaca_2.Naredbe
{
    public class FormatIspisaTablica : AbstractNaredba
    {
        public override void IzvrsiNaredbu(string naredba)
        {
            string[]? argumenti = naredba.Split(' ');
            argumenti = argumenti.Skip(1).ToArray();
            if(argumenti.Count() == 0){
                throw new Exception($"Naredba '{naredba}' nije ispravnog formata");
            }
            Tablica tablica = Tablica.Instanca;
            tablica.UrediIspisPodataka(argumenti);
        }
    }
}
