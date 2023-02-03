using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Grid
{
    public const float GridUnit=1;

    private static GridElemnt[,,] grid = new GridElemnt[100,100,100];
    public static GridElemnt IsCollide(Vector3Int position)
    {
        return grid[position.x, position.y, position.z];
    }
    public static void AddBlobPart(Vector3Int part)
    {
        if (grid[part.x, part.y, part.z] == GridElemnt.Empty)
            grid[part.x, part.y, part.z] = GridElemnt.Blob;
        Debug.Log(part);
    }
}
public enum GridElemnt { Empty, Blob, Cage};
