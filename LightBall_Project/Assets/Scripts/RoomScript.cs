using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomScript {
    public enum RoomType
    {
        deadEndUp,
        deadEndDown,
        deadEndRight,
        deadEndLeft,
        hallwayUpDown,
        hallwayLeftRight,
        cornerUpLeft,
        cornerUpRight,
        cornerDownRight,
        cornerDownLeft,
        triUp,
        triDown,
        triLeft,
        triRight,
        Quad,
        StartRoom
        
    }
    public static object lockObject;
    public static List<RoomScript> lastList;
    public RoomType type;
    public RoomScript up;
    public RoomScript down;
    public RoomScript left;
    public RoomScript right;
    static RoomScript()
    {
        lockObject = new object();
        lastList = new List<RoomScript>();
    }
    public RoomScript()
    {

    }
    public void decideTypes()
    {
        int neighbors = 4;
        if (up == null)//find neighbors
        {
            neighbors--;
        }
        if (down == null)
        {
            neighbors--;
        }
        if (right == null)
        {
            neighbors--;
        }
        if (left == null)
        {
            neighbors--;
        }
        if (type != RoomType.StartRoom)
        {
            if (neighbors == 4)
            {
                type = RoomType.Quad;
            }
            else if (neighbors == 3)
            {
                if (up == null)
                {
                    type = RoomType.triDown;
                }
                else if (down == null)
                {
                    type = RoomType.triUp;
                }
                else if (left == null)
                {
                    type = RoomType.triRight;
                }
                else
                {
                    type = RoomType.triLeft;
                }
            }
            else if (neighbors == 2)
            {
                if (up == null)
                {
                    if (left == null)
                    {
                        type = RoomType.cornerUpLeft;
                    }
                    else if (right == null)
                    {
                        type = RoomType.cornerUpRight;
                    }
                    else if (down == null)
                    {
                        type = RoomType.hallwayUpDown;
                    }
                }
                else if (down == null)
                {
                    if (left == null)
                    {
                        type = RoomType.cornerDownLeft;
                    }
                    else if (right == null)
                    {
                        type = RoomType.cornerDownRight;
                    }
                }
                else if (left == null)
                {
                    if (right == null)
                    {
                        type = RoomType.hallwayLeftRight;
                    }
                }
            }
            else
            {
                if (up != null)
                {
                    type = RoomType.deadEndDown;
                }
                else if (down != null)
                {
                    type = RoomType.deadEndUp;
                }
                else if (left != null)
                {
                    type = RoomType.deadEndLeft;
                }
                else if (right != null)
                {
                    type = RoomType.deadEndRight;
                }
            }
        }
        lock (lockObject)
        {
            lastList.Add(this);
        }
        if (up != null && !lastList.Contains(up))
        {
            up.decideTypes();
        }
        if (down != null && !lastList.Contains(down))
        {
            down.decideTypes();
        }
        if (right != null && !lastList.Contains(right))
        {
            right.decideTypes();
        }
        if (left != null && !lastList.Contains(left))
        {
            left.decideTypes();
        }
    }
    
}
