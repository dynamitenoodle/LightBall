using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomScript : MonoBehaviour {
    public enum RoomType
    {
        deadEndUp,
        deadEndDown,
        deadEndRight,
        deadEndLeft,
        hallwayUpDown,
        hallwayLeftRight,
        TriUp,
        TriDown,
        TriLeft,
        TriRight,
        Quad,
    }
    public RoomScript up;
    public RoomScript down;
    public RoomScript left;
    public RoomScript right;
    
}
