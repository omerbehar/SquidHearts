
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    public Node parent;
    public List<Node> children;
    public Vector3Int position;
    public int distanceFromOrigin;
    public int distanceFromTarget;
    public int Cost { get { return distanceFromOrigin + distanceFromTarget; } }
    
    public Node(Vector3Int position, Vector3Int target)
    {
        this.position = position;
        Vector3Int distance = target - position;
        distanceFromTarget = Mathf.Abs(distance.x) + Mathf.Abs(distance.y) + Mathf.Abs(distance.z);
    }
}