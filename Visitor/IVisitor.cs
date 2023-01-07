using msakac_zadaca_3.Modeli;

namespace msakac_zadaca_3.Visitor
{
    public interface IVisitor
    {
        Vez? Visit(Element element);
    }
}
