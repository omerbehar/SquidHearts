using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public static class EventManager
{
    public static UnityEvent<Direction> movementClicked = new UnityEvent<Direction>();
    public static UnityEvent<Direction> rotateClicked = new UnityEvent<Direction>();
    public static UnityEvent povChanged = new UnityEvent();
    public static UnityEvent threeDimensionsViewActivated = new UnityEvent();
}
