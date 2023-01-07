using msakac_zadaca_3.Modeli;

namespace msakac_zadaca_3.Visitor
{
    public abstract class Element
    {
        public abstract Vez? Accept(IVisitor visitor);
    }
}
