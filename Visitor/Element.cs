using msakac_zadaca_2.Modeli;

namespace msakac_zadaca_2.Visitor
{
    public abstract class Element
    {
        public abstract Vez? Accept(IVisitor visitor);
    }
}
