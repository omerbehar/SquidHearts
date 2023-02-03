using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blob : MonoBehaviour
{
    public Vector3Int blobGridPosition = new Vector3Int(50, 50, 50);
    [SerializeField] private List<Vector3Int> blobRelativeParts = new();
    private bool isMoving = true;

    public void MoveBlobOnTick()
    {
        if (!isMoving)
        {
            EventManager.Tick.RemoveListener(MoveBlobOnTick);
            return;
        }
        if (OnMove(Vector3Int.up))
        {
            transform.position += Vector3.up * Grid.GridUnit;
            blobGridPosition += Vector3Int.up;
        }
    }
    public void RotateBlobOnInput(Vector3Int axis)
    {
        List<Vector3Int> newParts = new();
        foreach (Vector3Int part in blobRelativeParts)
        {
            Vector3Int newPart;
            if (axis.z != 0)
            {
                newPart = (new Vector3Int(part.y * axis.x, -part.z * axis.x, part.z));
            }
            else if (axis.x != 0)
            {
                newPart = (new Vector3Int(part.x, -part.z * axis.x, part.y * axis.x));
            }
            else { return; }
            switch (Grid.IsCollide(newPart+ blobGridPosition))
            {
                case GridElement.Blob:
                case GridElement.Cage:
                    EventManager.CantRotate.Invoke();
                    print("CantRotate");
                    return;
            }
            newParts.Add(newPart);
        }
        transform.eulerAngles += axis * 90;
        blobRelativeParts = newParts;
    }
    public void MoveBlobOnInput(Vector3Int axis)
    {
        if (OnMove(new Vector3Int(axis.x, 0, axis.z)))
        {
            transform.position += (Vector3)axis * Grid.GridUnit;
            blobGridPosition += axis;
        }
        else
        {
            EventManager.Tick.RemoveListener(MoveBlobOnTick);
        }
    }
    private bool OnMove(Vector3Int direction)
    {
        bool isMove = true;
        foreach (Vector3Int part in blobRelativeParts)
        {
            switch (Grid.IsCollide(part + direction+ blobGridPosition))
            {
                case GridElement.Blob:
                    isMove = false;
                    break;
                case GridElement.Cage:
                    isMove = false;
                    break;
            }
        }
        if(!isMove)
        {
            isMoving = false;
            foreach (Vector3Int part in blobRelativeParts)
            {
                Grid.AddBlobPart(part + blobGridPosition);
            }
        }
        return isMove;
    }

    private void Start()
    {
        EventManager.Tick.AddListener(MoveBlobOnTick);
    }
}
