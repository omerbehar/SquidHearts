
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
                newPart = (new Vector3Int(-part.y * directionVector.z, part.x * directionVector.z, part.z));
            }
            else if (directionVector.x != 0)
            {
                newPart = (new Vector3Int(part.x, -part.z * directionVector.x, part.y * directionVector.x));
            }
            else { return; }
            if (CanRotatePart(newPart))
                newParts.Add(newPart);
        }
        print(directionVector * 90);
        transform.Rotate(directionVector * 90,Space.World);
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
        // else
        // {
        //     EventManager.Tick.RemoveListener(MoveBlobOnTick);
        // }
    }

    private bool CanRotatePart(Vector3Int newPart)
    {
        IGridElement collidedElement = Grid.CanMoveTo(newPart + GridPosition);
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
            IGridElement collidedElement = Grid.CanMoveTo(part + direction + GridPosition);
            if (collidedElement != null)
            {
                Debug.Log(collidedElement.ElementType);
                switch (collidedElement.ElementType)
                {
                    case GridElement.Wall:
                        return false;
                    case GridElement.Ceiling:
                        EventManager.NextBlobRequested.Invoke();
                        Destroy(gameObject);
                        return false;
                    case GridElement.Floor:
                        break;
                    case GridElement.Blob:
                    case GridElement.Cage:
                        willCollide = true;
                        OnConnected(collidedElement);
                        break;
                }
            }
        }
        SetNewConnectionsStatus();
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

 //public void RotateBlobOnInput(Vector3Int directionVector)
    //{
    //    List<Vector3Int> newParts = new();
    //    foreach (Vector3Int part in blobRelativeParts)
    //    {
    //        Vector3Int newPart;
    //        if (directionVector.x != 0)
    //        {
    //            newPart = (new Vector3Int(part.x, part.z * directionVector.x, -part.y * directionVector.x));
    //        }
    //        else if (directionVector.z != 0)
    //        {
    //            newPart = (new Vector3Int(part.y * directionVector.z, -part.x * directionVector.z, part.z));
    //        }
    //    }
    //    transform.rotation *= Quaternion.AngleAxis(90f, directionVector);
    //}
}
