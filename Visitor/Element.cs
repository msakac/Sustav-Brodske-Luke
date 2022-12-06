namespace msakac_zadaca_1.Visitor
{
    public abstract class Element
    {
        public abstract void Accept(IVisitor visitor);
    }
}
