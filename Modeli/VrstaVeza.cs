namespace msakac_zadaca_1.Modeli
{
    public class VrstaVeza
    {
        public string? nazivVeza { get; set; }
        public OznakaVrsteVeza oznakaVeza { get; set; }
        public List<OznakaVrsteBroda>? oznakaVrsteBroda { get; set; }
        public VrstaVeza(string? nazivVeza, OznakaVrsteVeza oznakaVeza, List<OznakaVrsteBroda>? oznakaVrsteBroda)
        {
            this.nazivVeza = nazivVeza;
            this.oznakaVeza = oznakaVeza;
            this.oznakaVrsteBroda = oznakaVrsteBroda;
        }
    }
}
