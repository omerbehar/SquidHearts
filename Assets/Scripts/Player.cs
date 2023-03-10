using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Blob currentBlob;

    private void Awake()
    {
        InitEventListeners();
    }
    private void InitEventListeners()
    {
        RemoveEventListeners();
        EventManager.MovementInput.AddListener(OnMovementClicked);
        EventManager.RotateClicked.AddListener(OnRotateClicked);
        EventManager.BlobCreated.AddListener(OnBlobCreated);
    }

    private void OnBlobCreated(Blob newBlob, int blobAmount)
    {
        currentBlob = newBlob;
    }

    private void OnRotateClicked(Direction direction, PovState povState)
    {
        switch (povState)
        {
            case PovState.Front:
                switch (direction)
                {
                    case Direction.Left:
                        currentBlob.RotateBlobOnInput(Vector3Int.left);
                        break;
                    case Direction.Right:
                        currentBlob.RotateBlobOnInput(Vector3Int.right);
                        break;
                }
                break;
            case PovState.Side:
                switch (direction)
                {
                    case Direction.Left:
                        currentBlob.RotateBlobOnInput(Vector3Int.forward);
                        break;
                    case Direction.Right:
                        currentBlob.RotateBlobOnInput(Vector3Int.back);
                        break;
                }
                break;
        }
    }

    private void OnMovementClicked(Direction direction, PovState povState)
    {
        switch (povState)
        {
            case PovState.Front:
                switch (direction)
                {
                    case Direction.Left:
                        currentBlob.MoveBlobOnInput(Vector3Int.back);
                        break;
                    case Direction.Right:
                        currentBlob.MoveBlobOnInput(Vector3Int.forward);
                        break;
                    case Direction.Up:
                        currentBlob.MoveBlobOnInput(Vector3Int.up);
                        break;
                }

                break;
            case PovState.Side:
                switch (direction)
                {
                    case Direction.Left:
                        currentBlob.MoveBlobOnInput(Vector3Int.left);
                        break;
                    case Direction.Right:
                        currentBlob.MoveBlobOnInput(Vector3Int.right);
                        break;
                    case Direction.Up:
                        currentBlob.MoveBlobOnInput(Vector3Int.up);
                        break;
                }
                break;
        }
    }
    private void RemoveEventListeners()
    {
        EventManager.MovementInput.RemoveListener(OnMovementClicked);
        EventManager.RotateClicked.RemoveListener(OnRotateClicked);
        EventManager.BlobCreated.RemoveListener(OnBlobCreated);
    }
}