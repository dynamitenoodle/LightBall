using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dungeon : MonoBehaviour
{
    public static List<List<int>> Map;
    public static int sizeX;
    public static int sizeY;
    public static int maxSize;
    static Dungeon()
    {
        System.Random r = new System.Random();
        if (maxSize < 8)
        {
            maxSize = 16;
        }
        if (sizeX == 0)
        {
            sizeX = r.Next(1, maxSize / 2);
        }
        if (sizeY == 0)
        {
            sizeY = maxSize / sizeX;
        }
        Map = new List<List<int>>();
        for (int x = 0; x < sizeX; x++)
        {
            Map.Add(new List<int>());
            for (int y = 0; y < sizeY; y++)
            {
                Map[x].Add(1);
            }
        }
    }
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
