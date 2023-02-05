using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField] private KeyCode moveLeftKey, moveRightKey, rotateLeftKey, rotateRightKey;
    [SerializeField] private KeyCode moveUpKey = KeyCode.W;
    [SerializeField] private KeyCode changePovKey = KeyCode.LeftShift, goto3DKey = KeyCode.Space;

    [SerializeField] private float holdTimeToSecondMovement, holdTimeToThirdMovement;

    private bool isPovHeld;
    private bool isIsoHeld;
    private float timeFromKeyDown;

    private int moveCount;

    [SerializeField] private GameManager gameManager;
    //private static InputManager Instance { get; set; }
    private void Awake()
    {
        // if (Instance != null)
        // {
        //     Destroy(gameObject);
        //     return;
        // }
        // //Instance = this;
        // DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        MoveOrRotateInput(MovementType.Move, Direction.Right);
        MoveOrRotateInput(MovementType.Move, Direction.Left);
        MoveOrRotateInput(MovementType.Move, Direction.Up);
        MoveOrRotateInput(MovementType.Rotate, Direction.Right);
        MoveOrRotateInput(MovementType.Rotate, Direction.Left);
        ChangePovInput();
        Goto3DInput();
    }

    private void Goto3DInput()
    {
        if (isPovHeld) return;
        if (Input.GetKeyDown(goto3DKey))
        {
            EventManager.ThreeDimensionsViewActivated.Invoke();
            isIsoHeld = true;
        }
        if (Input.GetKeyUp(goto3DKey))
        {
            EventManager.ThreeDimensionsViewActivated.Invoke();
            isIsoHeld = false;
        }
    }

    private void ChangePovInput()
    {
        if (isIsoHeld) return;
        if (Input.GetKeyDown(changePovKey))
        {
            EventManager.PovChanged.Invoke();
            isPovHeld = true;
        }
        if (Input.GetKeyUp(changePovKey))
        {
            EventManager.PovChanged.Invoke();
            isPovHeld = false;
        }
    }

    private void MoveOrRotateInput(MovementType movementType, Direction direction)
    {   
        KeyCode keyCode = movementType switch
        {
            MovementType.Move => direction switch
            {
                Direction.Left => moveLeftKey,
                Direction.Right => moveRightKey,
                Direction.Up => moveUpKey,
                _ => default
            },
            MovementType.Rotate => direction switch
            {
                Direction.Left => rotateLeftKey,
                Direction.Right => rotateRightKey,
                _ => default
            },
        };
        if (Input.GetKeyDown(keyCode))
        {
            InvokeMoveOrRotateEvent(movementType, direction);
        }
        if (Input.GetKey(keyCode))
        {
            timeFromKeyDown += Time.deltaTime;
            if ((moveCount == 1 && timeFromKeyDown > holdTimeToSecondMovement) ||
                moveCount >= 2 && timeFromKeyDown > holdTimeToThirdMovement)
                InvokeMoveOrRotateEvent(movementType, direction);
        }
        if (!Input.GetKeyUp(keyCode)) return;
        moveCount = 0;
        timeFromKeyDown = 0;
    }

    private void InvokeMoveOrRotateEvent(MovementType movementType, Direction direction)
    {
        moveCount++;
        switch (movementType)
        {
            case MovementType.Move:
                EventManager.MovementInput.Invoke(direction, gameManager.povState);
                break;
            case MovementType.Rotate:
                EventManager.RotateClicked.Invoke(direction, gameManager.povState);
                break;
        }
        timeFromKeyDown = 0;
    }
}
