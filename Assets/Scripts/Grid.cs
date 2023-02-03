using UnityEngine;

public static class Grid
{
    public const float GridUnit = 1;

    private static IGridElement[,,] grid = new IGridElement[100, 100, 100];
    public static IGridElement IsCollide(Vector3Int position)
    {
        return grid[position.x, position.y, position.z];
    }
    public static void AddBlobPart(Vector3Int partPositio, IGridElement blob)
    {
        if (grid[partPositio.x, partPositio.y, partPositio.z] == null)
            grid[partPositio.x, partPositio.y, partPositio.z] = blob;
        Debug.Log(partPositio);
    }
}
public enum GridElement { Empty, Blob, Cage};
