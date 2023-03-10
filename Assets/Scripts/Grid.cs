using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class Grid
{
    public const float GridUnit = 0.5f;
    private static IGridElement[,,] grid = new IGridElement[14, 19, 14];
    private static Vector3Int rootIndex = new Vector3Int(7, 17, 7);
    public static Node calculatedTrail;


    public static IGridElement CanMoveTo(Vector3Int position)
    {
        if (position.x > grid.GetLength(0) - 1 || position.x < 0) return new OutOfBoundElement(GridElementType.Wall);
        if (position.y > grid.GetLength(1) - 1) return new OutOfBoundElement(GridElementType.Ceiling);
        if (position.z > grid.GetLength(2) - 1 || position.z < 0) return new OutOfBoundElement(GridElementType.Wall);
        if (position.y < 0) return new OutOfBoundElement(GridElementType.Floor);

        return grid[position.x, position.y, position.z];
    }
    public static void AddPartToGrid(Vector3Int partPosition, IGridElement part)
    {
        if (IsOutOfBound(partPosition))
            return;
        grid[partPosition.x, partPosition.y, partPosition.z] ??= part;
        EventManager.PartAddedToGrid.Invoke();
        //AStar(new Vector3Int(7, 17, 7), partPosition);
    }
    private static bool IsOutOfBound(Vector3Int position)
    {
        if (position.x <= grid.GetLength(0) && position.y <= grid.GetLength(1)
                                            && position.z <= grid.GetLength(2)) return false;
        Debug.LogError($"position out of bounds {position}");
        return true;
    }
    public static Node AStar(Vector3Int origin, Vector3Int target)
    {
        Debug.Log("A*");
        Node[,,] allNodes = new Node[grid.GetLength(0), grid.GetLength(1), grid.GetLength(2)];
        for (int i = 0; i < grid.GetLength(0); i++)
        {
            for (int j = 0; j < grid.GetLength(1); j++)
            {
                for (int k = 0; k < grid.GetLength(2); k++)
                {
                    allNodes[i, j, k] = (new Node(new Vector3Int(i, j, k), target));
                }
            }
        }
        Node root = allNodes[origin.x, origin.y, origin.z];
        List<Node> open = new List<Node>();
        List<Node> closed = new List<Node>();
        open.Add(root);
        bool findPath = false;
        while (!findPath)
        {
            Node current = open.OrderBy(n => n.Cost).ToList()[0];
            open.Remove(current);
            closed.Add(current);
            if (current.position == target)
            {
                findPath = true;
                break;
            }
            List<Node> neighbors = new List<Node>();
            Vector3Int pos = current.position;
            if (pos.x + 1 < allNodes.GetLength(0)) neighbors.Add(allNodes[pos.x + 1, pos.y, pos.z]);
            if (pos.x - 1 >= 0) neighbors.Add(allNodes[pos.x - 1, pos.y, pos.z]);
            if (pos.y + 1 < allNodes.GetLength(1)) neighbors.Add(allNodes[pos.x, pos.y + 1, pos.z]);
            if (pos.y - 1 >= 0) neighbors.Add(allNodes[pos.x, pos.y - 1, pos.z]);
            if (pos.z + 1 < allNodes.GetLength(2)) neighbors.Add(allNodes[pos.x, pos.y, pos.z + 1]);
            if (pos.z - 1 >= 0) neighbors.Add(allNodes[pos.x, pos.y, pos.z - 1]);

            foreach (Node n in neighbors)
            {
                if (closed.Contains(n) || CanMoveTo(n.position) == null || CanMoveTo(n.position).ElementType != GridElementType.Blob)
                { continue; }
                //Debug.Log($"Visit Neighbor {n.position}");
                if (!open.Contains(n) || current.distanceFromOrigin + 1 < n.distanceFromOrigin)
                {
                    n.distanceFromOrigin = current.distanceFromOrigin + 1;
                    if (n.parent != null) n.parent.children.Remove(n);
                    n.parent = current;
                    current.children.Add(n);
                    if (!open.Contains(n))
                    {
                        open.Add(n);
                    }
                }
            }
        }

        calculatedTrail = root;
        return root;
    }

    public static void PrintGrid()
    {
        for (int i = 0; i < grid.GetLength(0); i++)
        {
            for (int j = 0; j < grid.GetLength(1); j++)
            {
                for (int k = 0; k < grid.GetLength(2); k++)
                {
                    if (grid[i, j, k] != null)
                        Debug.Log($"({i},{j},{k})");
                }
            }

        }
    }

    public static void ClearGrid()
    {
        grid = new IGridElement[14, 19, 14];
    }
}
    
