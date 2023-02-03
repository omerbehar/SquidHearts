using UnityEngine;

public static class Grid
{
    public const float GridUnit = 0.5f;

    private static IGridElement[,,] grid = new IGridElement[100, 100, 100];
    public static IGridElement IsCollide(Vector3Int position)
    {
        return grid[position.x, position.y, position.z];
    }
    public static void AddBlobPart(Vector3Int partPosition, IGridElement blob)
    {
        if (grid[partPosition.x, partPosition.y, partPosition.z] == null)
            grid[partPosition.x, partPosition.y, partPosition.z] = blob;
    }
}
