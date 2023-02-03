
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
[Serializable]
public class Blob : Connectable
{
    [field: SerializeField] public Vector3Int GridPosition { get; set; } = new Vector3Int(50, 50, 50);

    [SerializeField] private List<Vector3Int> blobRelativeParts = new();
    public bool isMovable = true;
    private List<Link> links;

    public void MoveBlobOnTick()
    {
        if (!isMovable)
        {
            EventManager.Tick.RemoveListener(MoveBlobOnTick);
            return;
        }
        MoveBlobOnInput(new Vector3Int(0, 1, 0));
    }
    public void RotateBlobOnInput(Vector3Int directionVector)
    {
        List<Vector3Int> newParts = new();
        foreach (Vector3Int part in blobRelativeParts)
        {
            Vector3Int newPart;
            if (directionVector.z != 0)
            {
                newPart = (new Vector3Int(part.y * directionVector.x, -part.z * directionVector.x, part.z));
            }
            else if (directionVector.x != 0)
            {
                newPart = (new Vector3Int(part.x, -part.z * directionVector.x, part.y * directionVector.x));
            }
            else { return; }
            if (CanRotatePart(newPart))
                newParts.Add(newPart);
        }
        transform.eulerAngles += directionVector * 90;
        blobRelativeParts = newParts;
    }
    public void MoveBlobOnInput(Vector3Int directionVector)
    {
        if (!isMovable)
        {
            EventManager.Tick.RemoveListener(MoveBlobOnTick);
            return;
        }
        if (CanMove(directionVector))
        {
            transform.position += (Vector3)directionVector * Grid.GridUnit;
            GridPosition += directionVector;
        }
        else
        {
            EventManager.Tick.RemoveListener(MoveBlobOnTick);
        }
    }

   
    private bool CanRotatePart(Vector3Int newPart)
    {
        IGridElement collidedElement = Grid.IsCollide(newPart + GridPosition);
        if (collidedElement != null)
            switch (collidedElement.ElementType)
            {
                case GridElement.Blob:
                case GridElement.Cage:
                    EventManager.CantRotate.Invoke();
                    print("CantRotate");
                    return false; ;
            }
        return true;
    }
    private bool CanMove(Vector3Int direction)
    {
        bool willCollide = false;
        foreach (Vector3Int part in blobRelativeParts)
        {
            IGridElement collidedElement = Grid.IsCollide(part + direction + GridPosition);
            if (collidedElement != null)
            {
                switch (collidedElement.ElementType)
                {
                    case GridElement.Blob:
                    case GridElement.Cage:
                        willCollide = true;
                        ConnectToElement(collidedElement);
                        break;
                }
            }
        }
        UpdateNewConnections();
        if (willCollide)
        {
            isMovable = false;
            foreach (Vector3Int part in blobRelativeParts)
            {
                Grid.AddBlobPart(part + GridPosition, this);
            }
            EventManager.NextBlobRequested.Invoke();
        }
        return isMovable;
    }


    private void Start()
    {
        EventManager.Tick.AddListener(MoveBlobOnTick);
    }

    public void AddLink(IGridElement element, LinkState state)
    {
        throw new NotImplementedException();
    }
}
