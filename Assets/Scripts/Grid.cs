using UnityEngine;

public static class Grid
{
    public const float GridUnit=1;

    private static GridElement[,,] grid = new GridElement[100,100,100];
    public static GridElement IsCollide(Vector3Int position)
    {
        return grid[position.x, position.y, position.z];
    }
    public static void AddBlobPart(Vector3Int part)
    {
        if (grid[part.x, part.y, part.z] == GridElement.Empty)
            grid[part.x, part.y, part.z] = GridElement.Blob;
        Debug.Log(part);
    }
}
