using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cellroom
{

    private Vector3 position;
    public bool door;
    public bool side;
    

    public Vector3 Position { get => position; set => position = value; }

    
    
    public Cellroom(Vector3 pos, bool side)
    {
        this.Position = pos;
        this.side = side;
    }
}
