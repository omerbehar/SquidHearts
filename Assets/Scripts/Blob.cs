
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Blob : Connectable
{
    public Vector3Int GridPosition { get; private set; } = new Vector3Int(50, 50, 50);

    [SerializeField] private List<Vector3Int> blobRelativeParts = new();
    private bool isMovable = true;
    private Vector3Int rotationCount = Vector3Int.zero;

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

        //Vector3Int rotationAxis = Vector3Int.zero;

        //if (directionVector.x != 0)
        //    rotationAxis = Vector3Int.right * directionVector.x;
        //else if (directionVector.z != 0)
        //{
        //    switch (rotationCount.x)
        //    {
        //        case 0:
        //            rotationAxis = Vector3Int.forward;
        //            break;
        //        case 1:
        //            rotationAxis = Vector3Int.up;
        //            break;
        //        case 2:
        //            rotationAxis = Vector3Int.back;
        //            break;
        //        case 3:
        //            rotationAxis = Vector3Int.down;
        //            break;
        //    }
        //    rotationAxis *= directionVector.z;
        //}
        ////print("RotationAxis: " + rotationAxis);
        //rotationCount += rotationAxis;
        //rotationCount = new Vector3Int(rotationCount.x % 4, rotationCount.y % 4, rotationCount.z % 4);
        //print("euler: " + transform.localEulerAngles + " Add: " + rotationAxis * 90);
        //transform.Rotate(rotationAxis * 90);
        //print("Result: " + transform.localEulerAngles);
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
        if (CanMove(new Vector3Int(directionVector.x, 0, directionVector.z)))
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
                switch (collidedElement.ElementType)
                {
                    case GridElement.Blob:
                    case GridElement.Cage:
                        willCollide = true;
                        ConnectToElement(collidedElement);
                        break;
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
