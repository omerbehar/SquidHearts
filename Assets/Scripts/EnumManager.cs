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
    Side,   //Z
    Iso     //45
}

public enum LinkState
{
    RootPort,
    DesignatedPort,
    Blocking
}



public enum GridElement
{
    Empty, 
    Blob, 
    Cage,
    Wall,
    Ceiling,
    Floor
}

