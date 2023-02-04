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

public enum LinkState
{
    RootPort,
    DesignatedPort,
    Blocking
}



public enum GridElementType
{
    Empty, 
    Blob, 
    EscapeButton,
    BlobDestroyer,
    WaterPool
    Wall,
    Ceiling,
    Floor
}

