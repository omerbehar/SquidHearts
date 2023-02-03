using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField] private KeyCode moveLeftKey, moveRightKey, rotateLeftKey, rotateRightKey;

    [SerializeField] private KeyCode changePovKey = KeyCode.LeftShift, goto3DKey = KeyCode.Space;

    [SerializeField] private float holdTimeToSecondMovement, holdTimeToThirdMovement;

    private float timeFromKeyDown;

    private int moveCount;
    private static InputManager Instance { get; set; }
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        MoveOrRotateInput(MovementType.Move, Direction.Right);
        MoveOrRotateInput(MovementType.Move, Direction.Left);
        MoveOrRotateInput(MovementType.Rotate, Direction.Right);
        MoveOrRotateInput(MovementType.Rotate, Direction.Left);
        ChangePovInput();
        Goto3DInput();
    }

    private void Goto3DInput()
    {
        if (Input.GetKeyDown(goto3DKey))
        {
            EventManager.threeDimensionsViewActivated.Invoke();
        }
    }

    private void ChangePovInput()
    {
        if (Input.GetKeyDown(changePovKey))
        {
            EventManager.povChanged.Invoke();
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
                EventManager.movementInput.Invoke(direction);
                break;
            case MovementType.Rotate:
                EventManager.rotateClicked.Invoke(direction);
                break;
        }
        timeFromKeyDown = 0;
    }
}
