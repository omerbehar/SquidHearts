using UnityEngine.Events;

public static class EventManager
{
    public static UnityEvent Tick = new();
    public static UnityEvent CantRotate = new();
    public static UnityEvent<Direction> movementInput = new UnityEvent<Direction>();
    public static UnityEvent<Direction> rotateClicked = new UnityEvent<Direction>();
    public static UnityEvent povChanged = new UnityEvent();
    public static UnityEvent threeDimensionsViewActivated = new UnityEvent();
}
