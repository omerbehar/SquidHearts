
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
[Serializable]
public class Blob : Connectable
{
    [field: SerializeField] public Vector3Int GridPosition { get; set; } = new Vector3Int(50, 50, 50);

    [SerializeField] protected List<Vector3Int> blobRelativeParts = new();
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
                case GridElementType.Blob:
                    EventManager.CantRotate.Invoke();
                    print("CantRotate");
                    return false;
                case GridElementType.BlobDestroyer:
                    DestroyBlob();
                    break;
                case GridElementType.Wall:
                    return false;
                case GridElementType.Ceiling:
                    EventManager.NextBlobRequested.Invoke();
                    DestroyBlob();
                    return false;
                case GridElementType.Floor:
                    return false;
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
                    case GridElementType.BlobDestroyer:
                        DestroyBlob();
                        break;
                    case GridElementType.Wall:
                        return false;
                    case GridElementType.Ceiling:
                        EventManager.NextBlobRequested.Invoke();
                        DestroyBlob();
                        return false;
                    case GridElementType.Floor:
                        return false;
                    case GridElementType.Blob:
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
                Grid.AddPartToGrid(part + GridPosition, this);
            }
            EventManager.NextBlobRequested.Invoke();
        }
        else
        {
            CheckOverlapOnConnect();
        }
        return isMovable;
    }
    private void CheckOverlapOnConnect()
    {
        foreach (Vector3Int part in blobRelativeParts)
        {
            switch (Grid.CanMoveTo(part).ElementType)
            {
                case GridElementType.EscapeButton:
                    EventManager.ReachEscapeButton.Invoke();
                    break;
                case GridElementType.WaterPool:
                    EventManager.ConnectWaterPool.Invoke();
                    break;
            }
        }
    }
    private void DestroyBlob() {
        Destroy(gameObject);
    }

    private void Start()
    {
        
    }

    public void AddLink(IGridElement element, LinkState state)
    {
        throw new NotImplementedException();
    }


}
