
public class OutOfBoundElement : IGridElement
{
    public GridElementType ElementType { get; }

    public OutOfBoundElement(GridElementType elementType)
    {
        ElementType = elementType;
    }
}