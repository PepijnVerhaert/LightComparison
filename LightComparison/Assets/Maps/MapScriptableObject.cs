using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

[CreateAssetMenu(fileName = "Map", menuName = "ScriptableObjects/MapScriptableObject", order = 1)]
public class MapScriptableObject : ScriptableObject
{
    public List<List<bool>> map = new List<List<bool>>();

    public List<bool> map1d = new List<bool>();

    public int mapSize = 64;

    public void Init()
    {
        Debug.Log("Init map");

        map.Clear();

        for (int i = 0; i < mapSize; i++)
        {
            var row = new List<bool>();
            for (int j = 0; j < mapSize; j++)
            {
                if (Random.Range(0, 4) == 0)
                {
                    row.Add(true);
                }
                else
                {
                    row.Add(false);
                }
            }
            map.Add(row);
        }

        To1D();
    }

    public void To1D()
    {
        map1d.Clear();

        for (int i = 0; i < mapSize; i++)
        {
            for (int j = 0; j < mapSize; j++)
            {
                map1d.Add(map[i][j]);
            }
        }
    }

    public void To2D()
    {
        map.Clear();

        for (int i = 0; i < mapSize; i++)
        {
            var row = new List<bool>();
            for (int j = 0; j < mapSize; j++)
            {
                row.Add(map1d[i*64 + j]);
            }
            map.Add(row);
        }
    }
}