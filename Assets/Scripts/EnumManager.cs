using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction
{
    Right,
    Left,
    Up
}

public enum MovementType
{
    Move,
    Rotate
}

public enum PovState
{
    Front,  //X
    Side    //Z
}

public enum GridElement
{
    Empty, 
    Blob, 
    Cage
};
