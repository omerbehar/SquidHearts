using System;
using System.Collections;
using System.Collections.Generic;
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
        EventManager.movementInput.AddListener(OnMovementClicked);
        EventManager.rotateClicked.AddListener(OnRotateClicked);
    }


    private void OnRotateClicked(Direction direction)
    {
        switch (GameManager.Instance.povState)
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
                        currentBlob.RotateBlobOnInput(Vector3Int.back);
                        break;
                    case Direction.Right:
                        currentBlob.RotateBlobOnInput(Vector3Int.forward);
                        break;
                }
                break;
        }
    }

    private void OnMovementClicked(Direction direction)
    {
        switch (GameManager.Instance.povState)
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
                }

                break;
        }
    }

    private void RemoveEventListeners()
    {
        EventManager.movementInput.RemoveListener(OnMovementClicked);
        EventManager.rotateClicked.RemoveListener(OnRotateClicked);
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }
}