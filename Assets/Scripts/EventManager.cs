using UnityEngine.Events;

public static class EventManager
{
    public static UnityEvent Tick = new();
    public static UnityEvent CantRotate = new();
    public static UnityEvent<Direction, PovState> MovementInput = new();
    public static UnityEvent<Direction, PovState> RotateClicked = new();
    public static UnityEvent PovChanged = new();
    public static UnityEvent ThreeDimensionsViewActivated = new();
    public static UnityEvent NextBlobRequested = new();
    public static UnityEvent<Blob, int> BlobCreated = new();
    public static UnityEvent ReachEscapeButton = new();
    public static UnityEvent ConnectWaterPool = new();
    public static UnityEvent GameLost = new();
    public static UnityEvent RestartGame = new();
    public static UnityEvent<float> UpdateIsoTimer = new();
    public static UnityEvent BlobDestroyed = new();
    public static UnityEvent FailAction = new();
    public static UnityEvent PartAddedToGrid = new();
}
