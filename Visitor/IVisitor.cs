using msakac_zadaca_1.Modeli;

namespace msakac_zadaca_1.Visitor
{
    public interface IVisitor
    {
        Vez? Visit(Element element);
    }
}
