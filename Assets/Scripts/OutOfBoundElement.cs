
public class OutOfBoundElement : IGridElement
{
    public GridElement ElementType { get; }

    public OutOfBoundElement(GridElement elementType)
    {
        ElementType = elementType;
    }
}