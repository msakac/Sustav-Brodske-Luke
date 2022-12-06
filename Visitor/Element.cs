using msakac_zadaca_1.Modeli;

namespace msakac_zadaca_1.Visitor
{
    public abstract class Element
    {
        public abstract Vez? Accept(IVisitor visitor);
    }
}
