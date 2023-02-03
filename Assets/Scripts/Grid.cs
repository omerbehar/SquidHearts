using UnityEngine;

public static class Grid
{
    public const float GridUnit = 0.5f;
    private static IGridElement[,,] grid = new IGridElement[14, 19, 14];
    public static IGridElement CanMoveTo(Vector3Int position)
    {
        if (position.x > grid.GetLength(0) - 1 || position.x < 0) return new OutOfBoundElement(GridElement.Wall);
        if (position.y > grid.GetLength(1) - 1) return new OutOfBoundElement(GridElement.Ceiling);
        if (position.z > grid.GetLength(2) - 1 || position.z < 0) return new OutOfBoundElement(GridElement.Wall);
        if (position.y < 0) return new OutOfBoundElement(GridElement.Floor);
        
        return grid[position.x, position.y, position.z];
    }
    public static void AddBlobPart(Vector3Int partPosition, IGridElement blob)
    {
        if (IsOutOfBound(partPosition))
            return;
        grid[partPosition.x, partPosition.y, partPosition.z] ??= blob;
    }
    private static bool IsOutOfBound(Vector3Int position)
    {
        if (position.x <= grid.GetLength(0) && position.y <= grid.GetLength(1)
                                            && position.z <= grid.GetLength(2)) return false;
        Debug.LogError($"position out of bounds {position}");
        return true;
    }
}
