using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GenericGridElement :MonoBehaviour, IGridElement
{
    [field:SerializeField]public GridElementType ElementType { get; set; }
    [SerializeField] private List<Vector3Int> RelativeParts = new();

    private void Start()
    {
        foreach(Vector3Int part in RelativeParts)
        {
            Grid.AddPartToGrid(part, this);
        }
    }
}