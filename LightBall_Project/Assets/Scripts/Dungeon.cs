using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dungeon : MonoBehaviour
{
    public static List<List<int>> Map;
    public static int sizeX;
    public static int sizeY;
    public static int maxSize;
    public GameObject bd;
    static GameObject Floortile;
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
        Map = new List<List<int>>();
        for (int x = 0; x < sizeX; x++)
        {
            Map.Add(new List<int>());
            for (int y = 0; y < sizeY; y++)
            {
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
                Map[(int)Dungeon[i].x][(int)Dungeon[i].y] = 2;
            }
            else
            {
                Map[(int)Dungeon[i].x][(int)Dungeon[i].y] = 1;
            }
        }
        foreach (List<int> item in Map)
        {
            string line = "";
            for (int i = 0; i < item.Count; i++)
            {
                line += item[i];
            }
            print(line);
        }
        for (int i = 0; i < Map.Count; i++)
        {
            for (int t = 0; t < Map[i].Count; t++)
            {
                if (Map[i][t] == 1)
                {
                    Instantiate<GameObject>(bd, new Vector3(i * bd.transform.localScale.x, t * bd.transform.localScale.y, 3), Quaternion.Euler(Vector3.zero));
                }
                else if (Map[i][t] == 2)
                {
                    GameObject temp = Instantiate<GameObject>(bd, new Vector3(i * 20, t * 20, 3), Quaternion.Euler(Vector3.zero));
                    temp.GetComponent<MeshRenderer>().material.color = new Color(255, 0, 0);
                }
                else
                {
                    GameObject temp = Instantiate<GameObject>(bd, new Vector3(i * 20, t * 20, 3), Quaternion.Euler(Vector3.zero));
                    temp.GetComponent<MeshRenderer>().material.color = new Color(255, 255, 255);
                }
            }
        }
    }
    List<Vector2> Path(int rooms, Vector2 startPoint)
    {
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
            int forkRoom = 0;
            current = finalPath[forkRoom];
            if(storedDir<4)
            storedDir++;
            dir = storedDir;
        }
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
                int forkRoom = r.Next(0, 2);
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
    // Update is called once per frame
    void Update()
    {
        
    }
}
