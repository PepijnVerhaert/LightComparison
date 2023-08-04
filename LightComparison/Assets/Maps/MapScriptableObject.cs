using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

[CreateAssetMenu(fileName = "Map", menuName = "ScriptableObjects/MapScriptableObject", order = 1)]
public class MapScriptableObject : ScriptableObject
{
    public List<List<bool>> map = new List<List<bool>>();

    public int mapSize = 64;

    void Reset()
    {
        Debug.Log("Reset map");

        map.Clear();

        var row = new List<bool>();
        for (int i = 0; i < mapSize; i++) 
        {
            row.Add(false);
        }
        for (int i = 0; i < mapSize; i++)
        {
            map.Add(row);
        }
    }
}
