
using System;
using UnityEngine;

public class MainClass: MonoBehaviour
{
    private void Start()
    {
        //Populate();
        Grid.AddPartToGrid(new Vector3Int(0, 0, 0), new OutOfBoundElement(GridElementType.Blob));
        Grid.AddPartToGrid(new Vector3Int(1, 0, 0), new OutOfBoundElement(GridElementType.Blob));
        Grid.AddPartToGrid(new Vector3Int(2, 0, 0), new OutOfBoundElement(GridElementType.Blob));
        Grid.AddPartToGrid(new Vector3Int(0, 1, 0), new OutOfBoundElement(GridElementType.Blob));
        Grid.AddPartToGrid(new Vector3Int(0, 2, 0), new OutOfBoundElement(GridElementType.Blob));
        Grid.AddPartToGrid(new Vector3Int(1, 2, 0), new OutOfBoundElement(GridElementType.Blob));
        Grid.AddPartToGrid(new Vector3Int(2, 2, 0), new OutOfBoundElement(GridElementType.Blob));
        Grid.AddPartToGrid(new Vector3Int(2, 1, 0), new OutOfBoundElement(GridElementType.Blob));
        
        //Grid.AddBlobPart(new Vector3Int(2, 1, 0), new OutOfBoundElement(GridElement.Blob));
        //Grid.AddBlobPart(new Vector3Int(0, 1, 0), new OutOfBoundElement(GridElement.Blob));
        //Grid.AddBlobPart(new Vector3Int(0, 2, 0), new OutOfBoundElement(GridElement.Blob));
        //Grid.AddBlobPart(new Vector3Int(1, 2, 0), new OutOfBoundElement(GridElement.Blob));

        //Grid.PrintGrid();
        Node root = Grid.AStar(new Vector3Int(0, 0, 0), new Vector3Int(2, 0, 0));
        PrintTree(root, "0");
    }

    private static void Populate()//8, 6, 4
    {
        Grid.AddPartToGrid(new Vector3Int(0, 0, 0), new OutOfBoundElement(GridElementType.Blob));
        Grid.AddPartToGrid(new Vector3Int(1, 0, 0), new OutOfBoundElement(GridElementType.Blob));
        Grid.AddPartToGrid(new Vector3Int(2, 0, 0), new OutOfBoundElement(GridElementType.Blob));
        Grid.AddPartToGrid(new Vector3Int(2, 1, 0), new OutOfBoundElement(GridElementType.Blob));
        Grid.AddPartToGrid(new Vector3Int(2, 1, 1), new OutOfBoundElement(GridElementType.Blob));
        Grid.AddPartToGrid(new Vector3Int(2, 1, 2), new OutOfBoundElement(GridElementType.Blob));
        Grid.AddPartToGrid(new Vector3Int(2, 1, 3), new OutOfBoundElement(GridElementType.Blob));
        Grid.AddPartToGrid(new Vector3Int(3, 1, 3), new OutOfBoundElement(GridElementType.Blob));
        Grid.AddPartToGrid(new Vector3Int(4, 1, 3), new OutOfBoundElement(GridElementType.Blob));
        Grid.AddPartToGrid(new Vector3Int(4, 2, 3), new OutOfBoundElement(GridElementType.Blob));
        Grid.AddPartToGrid(new Vector3Int(4, 3, 3), new OutOfBoundElement(GridElementType.Blob));
        Grid.AddPartToGrid(new Vector3Int(4, 4, 3), new OutOfBoundElement(GridElementType.Blob));
        Grid.AddPartToGrid(new Vector3Int(5, 4, 3), new OutOfBoundElement(GridElementType.Blob));
        Grid.AddPartToGrid(new Vector3Int(6, 4, 3), new OutOfBoundElement(GridElementType.Blob));
        Grid.AddPartToGrid(new Vector3Int(6, 5, 3), new OutOfBoundElement(GridElementType.Blob));
        Grid.AddPartToGrid(new Vector3Int(6, 6, 3), new OutOfBoundElement(GridElementType.Blob));
        Grid.AddPartToGrid(new Vector3Int(6, 6, 4), new OutOfBoundElement(GridElementType.Blob));
        Grid.AddPartToGrid(new Vector3Int(7, 6, 4), new OutOfBoundElement(GridElementType.Blob));
        Grid.AddPartToGrid(new Vector3Int(8, 6, 4), new OutOfBoundElement(GridElementType.Blob));
        Grid.AddPartToGrid(new Vector3Int(0, 1, 0), new OutOfBoundElement(GridElementType.Blob));
        Grid.AddPartToGrid(new Vector3Int(0, 2, 0), new OutOfBoundElement(GridElementType.Blob));
        Grid.AddPartToGrid(new Vector3Int(1, 2, 0), new OutOfBoundElement(GridElementType.Blob));
        Grid.AddPartToGrid(new Vector3Int(2, 2, 0), new OutOfBoundElement(GridElementType.Blob));
        Grid.AddPartToGrid(new Vector3Int(3, 2, 0), new OutOfBoundElement(GridElementType.Blob));
        Grid.AddPartToGrid(new Vector3Int(3, 3, 0), new OutOfBoundElement(GridElementType.Blob));
        Grid.AddPartToGrid(new Vector3Int(3, 4, 0), new OutOfBoundElement(GridElementType.Blob));
        Grid.AddPartToGrid(new Vector3Int(3, 5, 0), new OutOfBoundElement(GridElementType.Blob));
        Grid.AddPartToGrid(new Vector3Int(2, 5, 0), new OutOfBoundElement(GridElementType.Blob));
        Grid.AddPartToGrid(new Vector3Int(2, 5, 1), new OutOfBoundElement(GridElementType.Blob));
        Grid.AddPartToGrid(new Vector3Int(2, 5, 2), new OutOfBoundElement(GridElementType.Blob));
        Grid.AddPartToGrid(new Vector3Int(3, 5, 2), new OutOfBoundElement(GridElementType.Blob));
        Grid.AddPartToGrid(new Vector3Int(4, 5, 2), new OutOfBoundElement(GridElementType.Blob));
        Grid.AddPartToGrid(new Vector3Int(5, 5, 2), new OutOfBoundElement(GridElementType.Blob));
        Grid.AddPartToGrid(new Vector3Int(5, 4, 2), new OutOfBoundElement(GridElementType.Blob));
        Grid.AddPartToGrid(new Vector3Int(5, 3, 2), new OutOfBoundElement(GridElementType.Blob));
        Grid.AddPartToGrid(new Vector3Int(5, 3, 3), new OutOfBoundElement(GridElementType.Blob));
        Grid.AddPartToGrid(new Vector3Int(5, 3, 4), new OutOfBoundElement(GridElementType.Blob));
        Grid.AddPartToGrid(new Vector3Int(5, 3, 5), new OutOfBoundElement(GridElementType.Blob));
        Grid.AddPartToGrid(new Vector3Int(5, 3, 6), new OutOfBoundElement(GridElementType.Blob));
        Grid.AddPartToGrid(new Vector3Int(6, 3, 6), new OutOfBoundElement(GridElementType.Blob));
        Grid.AddPartToGrid(new Vector3Int(7, 3, 6), new OutOfBoundElement(GridElementType.Blob));
        Grid.AddPartToGrid(new Vector3Int(7, 4, 6), new OutOfBoundElement(GridElementType.Blob));
        Grid.AddPartToGrid(new Vector3Int(7, 5, 6), new OutOfBoundElement(GridElementType.Blob));
        Grid.AddPartToGrid(new Vector3Int(8, 5, 6), new OutOfBoundElement(GridElementType.Blob));
        Grid.AddPartToGrid(new Vector3Int(8, 6, 6), new OutOfBoundElement(GridElementType.Blob));
        Grid.AddPartToGrid(new Vector3Int(8, 6, 5), new OutOfBoundElement(GridElementType.Blob));
        Grid.AddPartToGrid(new Vector3Int(8, 6, 4), new OutOfBoundElement(GridElementType.Blob));
    }

    private void PrintTree(Node root, string line)
    {
        print("Path" + line + " Position: " + root.position);
        for (int i = 0; i < root.children.Count; i++)
        {
            PrintTree(root.children[i], line + i);
        }
    }
}
