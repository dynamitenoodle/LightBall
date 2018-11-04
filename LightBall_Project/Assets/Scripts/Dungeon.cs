using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dungeon : MonoBehaviour
{
    public static RoomScript startRoom;
    public static List<List<RoomScript>> Rooms;
    public static List<List<int>> Map;
    public static int sizeX;
    public static int sizeY;
    public static int maxSize;
    public GameObject bd;
    static GameObject Floortile;
    public GameObject Wall;
	public int zPosition = 1;
    public static List<int> worldToMapPos(Vector3 m_worlPos)
    {
        int xPos = Mathf.FloorToInt(m_worlPos.x / Floortile.transform.localScale.x);
        int yPos = Mathf.FloorToInt(m_worlPos.y / Floortile.transform.localScale.y);
        List<int> mapPos = new List<int>();
        mapPos.Add(xPos);
        mapPos.Add(yPos);
        return mapPos;
    }
    public int Holes;
    static Dungeon()
    {
        System.Random r = new System.Random();
        if (maxSize < 8)
        {
            maxSize = 64;
        }
        if (sizeX == 0)
        {
            sizeX = 8;
        }
        if (sizeY == 0)
        {
            sizeY = 8;
        }
        Rooms = new List<List<RoomScript>>();
        Map = new List<List<int>>();
        for (int x = 0; x < sizeX; x++)
        {
            Rooms.Add(new List<RoomScript>());
            Map.Add(new List<int>());
            for (int y = 0; y < sizeY; y++)
            {
                Rooms[x].Add(null);
                Map[x].Add(0);
            }
        }
    }
    // Use this for initialization
    void Start()
    {
        Floortile = bd;
        int rooms = 22;
        List<Vector2> Dungeon = new List<Vector2>();
        while (Dungeon.Count < rooms)
        {
            Vector2 start = new Vector2(Random.Range(1, sizeX - 1), Random.Range(1, sizeY - 1));
            Dungeon = Path(rooms,new Vector2(4,4));
        }

        for(int i = 0;i<Dungeon.Count;i++)
        {
            if (i == 0)
            {
                Rooms[(int)Dungeon[i].x][(int)Dungeon[i].y] = new RoomScript();
                startRoom = Rooms[(int)Dungeon[i].x][(int)Dungeon[i].y];
                startRoom.type = RoomScript.RoomType.StartRoom;
                Map[(int)Dungeon[i].x][(int)Dungeon[i].y] = 2;
            }
            else
            {
                Rooms[(int)Dungeon[i].x][(int)Dungeon[i].y] = new RoomScript();
                Map[(int)Dungeon[i].x][(int)Dungeon[i].y] = 1;
            }
        }
        for (int x = 0; x < Rooms.Count; x++)
        {
            for (int y = 0; y < Rooms[x].Count; y++)
            {
                if (Rooms[x][y] != null)
                {
                    if (x + 1 < Rooms.Count)
                    {
                        if (Rooms[x + 1][y] != null)
                        {
                            Rooms[x][y].right = Rooms[x + 1][y];
                        }
                    }
                    if (x - 1 >= 0)
                    {
                        if (Rooms[x - 1][y] != null)
                        {
                            Rooms[x][y].left = Rooms[x - 1][y];
                        }
                    }
                    if (y + 1 < Rooms[x].Count)
                    {
                        if (Rooms[x][y + 1] != null)
                        {
                            Rooms[x][y].up = Rooms[x][y + 1];
                        }
                    }
                    if (y - 1 >= 0)
                    {
                        if (Rooms[x][y - 1] != null)
                        {
                            Rooms[x][y].down = Rooms[x][y - 1];
                        }
                    }
                }
            }
        }
        startRoom.decideTypes();
        for (int i = 0; i < Rooms.Count; i++)
        {
            for (int t = 0; t < Rooms[i].Count; t++)
            {
                GameObject temp;
                if(Rooms[i][t]!=null)
                switch (Rooms[i][t].type)
                {
                    case RoomScript.RoomType.deadEndUp:
                        temp = Instantiate<GameObject>(Resources.Load<GameObject>("Rooms/End/End1"), new Vector3(i * bd.transform.localScale.x, t * bd.transform.localScale.y, 0f), Quaternion.Euler(0, 0, 180));
                        break;
                    case RoomScript.RoomType.deadEndDown:
                        temp = Instantiate<GameObject>(Resources.Load<GameObject>("Rooms/End/End1"), new Vector3(i * bd.transform.localScale.x, t * bd.transform.localScale.y, 0f), Quaternion.Euler(0, 0, 0));    
                        break;
                    case RoomScript.RoomType.deadEndRight:
                        temp = Instantiate<GameObject>(Resources.Load<GameObject>("Rooms/End/End1"), new Vector3(i * bd.transform.localScale.x, t * bd.transform.localScale.y, 0f), Quaternion.Euler(0, 0, 270));
                        break;
                    case RoomScript.RoomType.deadEndLeft:
                        temp = Instantiate<GameObject>(Resources.Load<GameObject>("Rooms/End/End1"), new Vector3(i * bd.transform.localScale.x, t * bd.transform.localScale.y, 0f), Quaternion.Euler(0, 0, 90));
                        break;
                    case RoomScript.RoomType.hallwayUpDown:
                            if (Random.value > .5f)
                            {
                                temp = Instantiate<GameObject>(Resources.Load<GameObject>("Rooms/Hallway/Hallway1"), new Vector3(i * bd.transform.localScale.x, t * bd.transform.localScale.y, 0f), Quaternion.Euler(0, 0, 90));
                            }
                            else
                            {
                                temp = Instantiate<GameObject>(Resources.Load<GameObject>("Rooms/Hallway/Hallway1"), new Vector3(i * bd.transform.localScale.x, t * bd.transform.localScale.y, 0f), Quaternion.Euler(0, 0, 270));
                            }
                        break;
                    case RoomScript.RoomType.hallwayLeftRight:
                            if (Random.value > .5f)
                            {
                                temp = Instantiate<GameObject>(Resources.Load<GameObject>("Rooms/Hallway/Hallway1"), new Vector3(i * bd.transform.localScale.x, t * bd.transform.localScale.y, 0f), Quaternion.Euler(0, 0, 0));
                            }
                            else
                            {
                                temp = Instantiate<GameObject>(Resources.Load<GameObject>("Rooms/Hallway/Hallway1"), new Vector3(i * bd.transform.localScale.x, t * bd.transform.localScale.y, 0f), Quaternion.Euler(0, 0, 180));
                            }
                            break;
                    case RoomScript.RoomType.cornerUpLeft:
                            temp = Instantiate<GameObject>(Resources.Load<GameObject>("Rooms/Corner/Corner1"), new Vector3(i * bd.transform.localScale.x, t * bd.transform.localScale.y, 0f), Quaternion.Euler(0, 0, 270));
                            break;
                    case RoomScript.RoomType.cornerUpRight:
                            temp = Instantiate<GameObject>(Resources.Load<GameObject>("Rooms/Corner/Corner1"), new Vector3(i * bd.transform.localScale.x, t * bd.transform.localScale.y, 0f), Quaternion.Euler(0, 0, 180));
                            break;
                    case RoomScript.RoomType.cornerDownRight:
                            temp = Instantiate<GameObject>(Resources.Load<GameObject>("Rooms/Corner/Corner1"), new Vector3(i * bd.transform.localScale.x, t * bd.transform.localScale.y, 0f), Quaternion.Euler(0, 0, 90));
                            break;
                    case RoomScript.RoomType.cornerDownLeft:
                            temp = Instantiate<GameObject>(Resources.Load<GameObject>("Rooms/Corner/Corner1"), new Vector3(i * bd.transform.localScale.x, t * bd.transform.localScale.y, 0f), Quaternion.Euler(0, 0, 0));
                            break;
                    case RoomScript.RoomType.triUp:
                            temp = Instantiate<GameObject>(Resources.Load<GameObject>("Rooms/Tri/Tri1"), new Vector3(i * bd.transform.localScale.x, t * bd.transform.localScale.y, 0f), Quaternion.Euler(0, 0, 0));
                            break;
                    case RoomScript.RoomType.triDown:
                            temp = Instantiate<GameObject>(Resources.Load<GameObject>("Rooms/Tri/Tri1"), new Vector3(i * bd.transform.localScale.x, t * bd.transform.localScale.y, 0f), Quaternion.Euler(0, 0, 180));
                            break;
                    case RoomScript.RoomType.triLeft:
                            temp = Instantiate<GameObject>(Resources.Load<GameObject>("Rooms/Tri/Tri1"), new Vector3(i * bd.transform.localScale.x, t * bd.transform.localScale.y, 0f), Quaternion.Euler(0, 0, 90));
                            break;
                    case RoomScript.RoomType.triRight:
                            temp = Instantiate<GameObject>(Resources.Load<GameObject>("Rooms/Tri/Tri1"), new Vector3(i * bd.transform.localScale.x, t * bd.transform.localScale.y, 0f), Quaternion.Euler(0, 0, 270));
                            break;
                    case RoomScript.RoomType.Quad:
                            float y = Random.value;
                            if (y > .75f)
                            {
                                temp = Instantiate<GameObject>(Resources.Load<GameObject>("Rooms/Quad/Quad1"), new Vector3(i * bd.transform.localScale.x, t * bd.transform.localScale.y, 0f), Quaternion.Euler(0, 0, 270));
                            }
                            else if (y > .5f)
                            {
                                temp = Instantiate<GameObject>(Resources.Load<GameObject>("Rooms/Quad/Quad1"), new Vector3(i * bd.transform.localScale.x, t * bd.transform.localScale.y, 0f), Quaternion.Euler(0, 0, 180));
                            }
                            else if (y > .25f)
                            {
                                temp = Instantiate<GameObject>(Resources.Load<GameObject>("Rooms/Quad/Quad1"), new Vector3(i * bd.transform.localScale.x, t * bd.transform.localScale.y, 0f), Quaternion.Euler(0, 0, 90));
                            }
                            else
                            {
                                temp = Instantiate<GameObject>(Resources.Load<GameObject>("Rooms/Quad/Quad1"), new Vector3(i * bd.transform.localScale.x, t * bd.transform.localScale.y, 0f), Quaternion.Euler(0, 0, 0));
                            }
                            break;
                    case RoomScript.RoomType.StartRoom:
                            temp = Instantiate<GameObject>(Resources.Load<GameObject>("Rooms/Spawn/Spawn1"), new Vector3(i * bd.transform.localScale.x, t * bd.transform.localScale.y, 0f), Quaternion.Euler(0, 0, 0));
                            GameObject Op = GameObject.FindGameObjectWithTag("Player");
                            Op.transform.position = temp.transform.position; //- new Vector3(temp.transform.localScale.x/2, temp.transform.localScale.y / 2, 0);

                            break;
                    default:
                        break;
                }


            }
        }

        PlaceWalls();
    }
    List<Vector2> Path(int rooms, Vector2 startPoint)
    {
        int forkRoom = 0;
            List<Vector2> finalPath = new List<Vector2>();
            System.Random r = new System.Random();
            
            Vector2 current = startPoint;
            List<int> directions = new List<int>();
            finalPath.Add(current);

            int b = 0;
            int storedDir = 1; 
            int dir = 1;
            directions.Add(dir);
            for (int j = 0; j < 4; j++)
            {
            int length = r.Next(1,3);
            for (int i = 0; i < length; i++)
            {
                if (dir == 1)
                {//left
                    if (current.x - 1 >= 0)//can we
                    {
                        current = new Vector2(current.x - 1, current.y);
                        if (!finalPath.Contains(current))
                        {
                            finalPath.Add(current);
                            directions.Add(dir);
                        }
                        else
                        {
                            length++;
                        }
                    }
                    else
                    {
                        while (dir == 1)
                        {
                            if (i == 0)
                            {
                                dir = 2;
                                break;
                            }
                            dir = r.Next(3, 4);

                        }
                    }
                }
                else if (dir == 2)
                {//right
                    if (current.x + 1 < sizeX)//can we
                    {
                        current = new Vector2(current.x + 1, current.y);
                        if (!finalPath.Contains(current))
                        {
                            finalPath.Add(current);
                            directions.Add(dir);
                        }
                        else
                        {
                            length++;
                        }
                    }
                    else
                    {
                        while (dir == 2)
                        {
                            if (i == 0)
                            {
                                dir = 1;
                                break;
                            }
                            dir = r.Next(3, 4);
                        }
                    }
                }
                else if (dir == 3)
                {//down
                    if (current.y - 1 > 0)//can we
                    {
                        current = new Vector2(current.x, current.y - 1);
                        if (!finalPath.Contains(current))
                        {
                            finalPath.Add(current);
                            directions.Add(dir);
                        }
                        else
                        {
                            length++;
                        }
                    }
                    else
                    {
                        while (dir == 3)
                        {
                            if (i == 0)
                            {
                                dir = 4;
                                break;
                            }
                            dir = r.Next(1, 2);
                        }
                    }
                }
                else if (dir == 4)
                {//up
                    if (current.y + 1 < sizeY)//can we
                    {
                        current = new Vector2(current.x, current.y + 1);
                        if (!finalPath.Contains(current))
                        {
                            finalPath.Add(current);
                            directions.Add(dir);
                        }
                        else
                        {
                            length++;
                        }
                    }
                    else
                    {
                        while (dir == 4)
                        {
                            if (i == 0)
                            {
                                dir = 3;
                                break;
                            }
                            dir = r.Next(1, 2);
                        }
                    }
                }
            }
            forkRoom = 0;
            current = finalPath[forkRoom];
            if(storedDir<4)
            storedDir++;
            dir = storedDir;
        }
       /* forkRoom = r.Next(0, 2);
        if (forkRoom == 0)
        {
            forkRoom = r.Next(1, finalPath.Count - 1);
        }
        else if (forkRoom == 1)
        {
            forkRoom = finalPath.Count / 2;
        }
        else if (forkRoom == 2)
        {
            forkRoom = finalPath.Count - 1;
        }
        current = finalPath[forkRoom];
        if (directions[forkRoom] < 3)
        {
            dir = Random.Range(3, 4);
        }
        else
        {
            dir = Random.Range(1, 2);
        }*/
        while (finalPath.Count < rooms)
            {
                if (b > 1000000)
                {
                    print("end early");
                    break;
                }


                int length = r.Next(2,8);
                for (int i = 0; i <= length; i++)
                {
                    /* if (r.Next(1, 8) == 1)
                     {
                         if (dir < 3)
                         {
                             dir = r.Next(3, 4);
                         }
                         else
                         {
                             dir = r.Next(3, 4);
                         }
                     }*/
                    if (finalPath.Count > rooms)
                    {
                        break;
                    }
                    if (dir == 1)
                    {//left
                        if (current.x - 1 >= 0)//can we
                        {
                            current = new Vector2(current.x - 1, current.y);
                            if (!finalPath.Contains(current))
                            {
                                finalPath.Add(current);
                                directions.Add(dir);
                            }
                            else
                            {
                                length++;
                            }
                        }
                        else
                        {
                            while (dir == 1)
                            {
                                if (i == 0)
                                {
                                    dir = 2;
                                    break;
                                }
                                dir = r.Next(3, 4);

                            }
                        }
                    }
                    if (dir == 2)
                    {//right
                        if (current.x + 1 < sizeX)//can we
                        {
                            current = new Vector2(current.x + 1, current.y);
                            if (!finalPath.Contains(current))
                            {
                                finalPath.Add(current);
                                directions.Add(dir);
                            }
                            else
                            {
                                length++;
                            }
                        }
                        else
                        {
                            while (dir == 2)
                            {
                                if (i == 0)
                                {
                                    dir = 1;
                                    break;
                                }
                                dir = r.Next(3, 4);
                            }
                        }
                    }
                    if (dir == 3)
                    {//down
                        if (current.y - 1 > 0)//can we
                        {
                            current = new Vector2(current.x, current.y - 1);
                            if (!finalPath.Contains(current))
                            {
                                finalPath.Add(current);
                                directions.Add(dir);
                            }
                            else
                            {
                                length++;
                            }
                        }
                        else
                        {
                            while (dir == 3)
                            {
                                if (i == 0)
                                {
                                    dir = 4;
                                    break;
                                }
                                dir = r.Next(1, 2);
                            }
                        }
                    }
                    if (dir == 4)
                    {//up
                        if (current.y + 1 < sizeY)//can we
                        {
                            current = new Vector2(current.x, current.y + 1);
                            if (!finalPath.Contains(current))
                            {
                                finalPath.Add(current);
                                directions.Add(dir);
                            }
                            else
                            {
                                length++;
                            }
                        }
                        else
                        {
                            while (dir == 4)
                            {
                                if (i == 0)
                                {
                                    dir = 3;
                                    break;
                                }
                                dir = r.Next(1, 2);
                            }
                        }
                    }
                }
                forkRoom = r.Next(0, 2);
                if (forkRoom == 0)
                {
                forkRoom = r.Next(1, finalPath.Count - 1);
                }
                else if (forkRoom == 1)
                {
                    forkRoom = finalPath.Count / 2;
                }
                else if (forkRoom == 2)
                {
                    forkRoom = finalPath.Count - 1;
                }
                current = finalPath[forkRoom];
                if (directions[forkRoom] < 3)
                {
                    dir = Random.Range(3, 4);
                }
                else
                {
                    dir = Random.Range(1, 2);
                }
                


                b++;
            }
            
        
        return finalPath;
    }
    //Builds walls after the maze is built
    void PlaceWalls()
    {
        /*
        for (int i = 0; i < Map.Count - 1; i++)
        {
            for (int j = 0; j < Map[i].Count; j++)
            {
                if (Map[i][j] == 1 || Map[i][j] == 2)
                {
                    //Corners
                    //Top Left
                    if (i == 0 && j == 0)
                    {
                        Instantiate<GameObject>(Wall, new Vector3(i * bd.transform.localScale.x - 11, j * bd.transform.localScale.y, 3.1f), Quaternion.Euler(Vector3.zero));
                        Instantiate<GameObject>(Wall, new Vector3(i * bd.transform.localScale.x, j * bd.transform.localScale.y + 11, 3f), Quaternion.Euler(new Vector3(0,0,90)));
                        
                    }
                    //Top Right
                    else if (i == sizeY-1 && j == 0)
                    {
                        Instantiate<GameObject>(Wall, new Vector3(i * bd.transform.localScale.x + 11, j * bd.transform.localScale.y, 3f), Quaternion.Euler(Vector3.zero));
                        Instantiate<GameObject>(Wall, new Vector3(i * bd.transform.localScale.x, j * bd.transform.localScale.y + 11, 3.01f), Quaternion.Euler(new Vector3(0, 0, 90)));
                        
                    }

                    //Lower Left
                    else if (i == 0 && j == sizeX-1)
                    {
                        Instantiate<GameObject>(Wall, new Vector3(i * bd.transform.localScale.x - 11, j * bd.transform.localScale.y, zPosition), Quaternion.Euler(Vector3.zero));
                        Instantiate<GameObject>(Wall, new Vector3(i * bd.transform.localScale.x, j * bd.transform.localScale.y + 11, 3.01f), Quaternion.Euler(new Vector3(0, 0, 90)));
                        //Check Down
                        if (Map[i][j - 1] == 0)
                        {
                            Instantiate<GameObject>(Wall, new Vector3(i * bd.transform.localScale.x, j * bd.transform.localScale.y - 11, zPosition), Quaternion.Euler(new Vector3(0, 0, 90)));
                        }

                    }

                    //Lower Right
                    else if (i == sizeY-1 && j == sizeX-1)
                    {
                        Instantiate<GameObject>(Wall, new Vector3(i * bd.transform.localScale.x + 11, j * bd.transform.localScale.y, zPosition), Quaternion.Euler(Vector3.zero));
                        Instantiate<GameObject>(Wall, new Vector3(i * bd.transform.localScale.x, j * bd.transform.localScale.y - 11, zPosition), Quaternion.Euler(new Vector3(0, 0, 90)));
                        
                    }
                    //Edges
                    //Left
                    else if (i == 0)
                    {
                        Instantiate<GameObject>(Wall, new Vector3(i * bd.transform.localScale.x - 11, j * bd.transform.localScale.y, zPosition), Quaternion.Euler(Vector3.zero));
                        //Check right
                        if (Map[i+1][j] == 0)
                        {
                            Instantiate<GameObject>(Wall, new Vector3(i * bd.transform.localScale.x + 11, j * bd.transform.localScale.y, zPosition), Quaternion.Euler(Vector3.zero));
                        }
                        //Check up
                        if (Map[i][j+1] == 0)
                        {
                            Instantiate<GameObject>(Wall, new Vector3(i * bd.transform.localScale.x, j * bd.transform.localScale.y + 11, zPosition), Quaternion.Euler(new Vector3(0, 0, 90)));
                        }
                        //Check Down
                        if (Map[i][j-1] == 0)
                        {
                            Instantiate<GameObject>(Wall, new Vector3(i * bd.transform.localScale.x, j * bd.transform.localScale.y - 11, zPosition), Quaternion.Euler(new Vector3(0, 0, 90)));
                        }
                    }
                    //Right
                    else if (i == sizeY-1)
                    {
                        Instantiate<GameObject>(Wall, new Vector3(i * bd.transform.localScale.x + 11, j * bd.transform.localScale.y, zPosition), Quaternion.Euler(Vector3.zero));
                        //Check left
                        if (Map[i - 1][j] == 0)
                        {
                            Instantiate<GameObject>(Wall, new Vector3(i * bd.transform.localScale.x - 11, j * bd.transform.localScale.y, zPosition), Quaternion.Euler(Vector3.zero));
                        }
                        //Check up
                        if (Map[i][j + 1] == 0)
                        {
                            Instantiate<GameObject>(Wall, new Vector3(i * bd.transform.localScale.x, j * bd.transform.localScale.y + 11, zPosition), Quaternion.Euler(new Vector3(0, 0, 90)));
                        }
                        //Check Down
                        if (Map[i][j - 1] == 0)
                        {
                            Instantiate<GameObject>(Wall, new Vector3(i * bd.transform.localScale.x, j * bd.transform.localScale.y - 11, zPosition), Quaternion.Euler(new Vector3(0, 0, 90)));
                        }
                    }
                    //Up
                    else if (j == sizeX-1)
                    {
                        Instantiate<GameObject>(Wall, new Vector3(i * bd.transform.localScale.x, j * bd.transform.localScale.y + 11, zPosition), Quaternion.Euler(new Vector3(0, 0, 90)));
                        //Check left
                        if (Map[i - 1][j] == 0)
                        {
                            Instantiate<GameObject>(Wall, new Vector3(i * bd.transform.localScale.x - 11, j * bd.transform.localScale.y, zPosition), Quaternion.Euler(Vector3.zero));
                        }
                        //Check right
                        if (Map[i + 1][j] == 0)
                        {
                            Instantiate<GameObject>(Wall, new Vector3(i * bd.transform.localScale.x + 11, j * bd.transform.localScale.y, zPosition), Quaternion.Euler(Vector3.zero));
                        }
                        //Check Down
                        if (Map[i][j - 1] == 0)
                        {
                            Instantiate<GameObject>(Wall, new Vector3(i * bd.transform.localScale.x, j * bd.transform.localScale.y - 11, zPosition), Quaternion.Euler(new Vector3(0, 0, 90)));
                        }
                    }
                    //Down
                    else if (j == 0)
                    {
                        Instantiate<GameObject>(Wall, new Vector3(i * bd.transform.localScale.x, j * bd.transform.localScale.y - 11, zPosition), Quaternion.Euler(new Vector3(0, 0, 90)));
                        //Check left
                        if (Map[i - 1][j] == 0)
                        {
                            Instantiate<GameObject>(Wall, new Vector3(i * bd.transform.localScale.x - 11, j * bd.transform.localScale.y, zPosition), Quaternion.Euler(Vector3.zero));
                        }
                        //Check right
                        if (Map[i + 1][j] == 0)
                        {
                            Instantiate<GameObject>(Wall, new Vector3(i * bd.transform.localScale.x + 11, j * bd.transform.localScale.y, zPosition), Quaternion.Euler(Vector3.zero));
                        }
                        //Check up
                        if (Map[i][j + 1] == 0)
                        {
                            Instantiate<GameObject>(Wall, new Vector3(i * bd.transform.localScale.x, j * bd.transform.localScale.y + 11, zPosition), Quaternion.Euler(new Vector3(0, 0, 90)));
                        }
                    }
                    //other
                    else
                    {
                        //Check left
                        if (Map[i - 1][j] == 0)
                        {
                            Instantiate<GameObject>(Wall, new Vector3(i * bd.transform.localScale.x - 11, j * bd.transform.localScale.y, zPosition), Quaternion.Euler(Vector3.zero));
                        }
                        //Check right
                        if (Map[i + 1][j] == 0)
                        {
                            Instantiate<GameObject>(Wall, new Vector3(i * bd.transform.localScale.x + 11, j * bd.transform.localScale.y, zPosition), Quaternion.Euler(Vector3.zero));
                        }
                        //Check up
                        if (Map[i][j + 1] == 0)
                        {
                            Instantiate<GameObject>(Wall, new Vector3(i * bd.transform.localScale.x, j * bd.transform.localScale.y + 11, zPosition), Quaternion.Euler(new Vector3(0, 0, 90)));
                        }
                        //Check Down
                        if (Map[i][j - 1] == 0)
                        {
                            Instantiate<GameObject>(Wall, new Vector3(i * bd.transform.localScale.x, j * bd.transform.localScale.y - 11, zPosition), Quaternion.Euler(new Vector3(0, 0, 90)));
                        }
                    }
                }
            }
        }*/
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
