using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private void Awake()
    {
        InitEventListeners();
    }

    private void InitEventListeners()
    {
        RemoveEventListeners();
        EventManager.movementClicked.AddListener(OnMovementClicked);
        EventManager.rotateClicked.AddListener(OnRotateClicked);
        EventManager.povChanged.AddListener(OnPovChanged);
    }

    private void OnPovChanged()
    {
        throw new NotImplementedException();
    }

    private void OnRotateClicked(Direction direction)
    {
        switch (direction)
        {
            case Direction.Left:
                Debug.Log("rotated left");
                break;
            case Direction.Right:
                Debug.Log("rotated right");
                break;
        }
    }

    private void OnMovementClicked(Direction direction)
    {
        switch (direction)
        {
            case Direction.Left:
                Debug.Log("moved left");
                break;
            case Direction.Right:
                Debug.Log("moved right");
                break;
        }
    }

    private void RemoveEventListeners()
    {
        //EventManager.movementClicked.RemoveAllListeners();
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
