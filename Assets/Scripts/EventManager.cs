using UnityEngine.Events;

public static class EventManager
{
    public static UnityEvent Tick = new();
    public static UnityEvent CantRotate = new();
    public static UnityEvent<Direction> MovementInput = new();
    public static UnityEvent<Direction> RotateClicked = new();
    public static UnityEvent PovChanged = new();
    public static UnityEvent ThreeDimensionsViewActivated = new();
    public static UnityEvent NextBlobRequested = new();
    public static UnityEvent<Blob> BlobCreated = new();
    public static UnityEvent GameLost = new();
    public static UnityEvent RestartGame = new();
    public static UnityEvent UpdateIsoTimer = new();
}
