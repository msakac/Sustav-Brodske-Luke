using msakac_zadaca_2.Modeli;

namespace msakac_zadaca_2.Visitor
{
    public interface IVisitor
    {
        Vez? Visit(Element element);
    }
}
