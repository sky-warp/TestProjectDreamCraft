public interface IProvider<ObjectType, ElementType>
{
    public ObjectType GetElement(ElementType elementType);
}