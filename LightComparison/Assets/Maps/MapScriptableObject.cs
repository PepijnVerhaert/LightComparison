using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

[CreateAssetMenu(fileName = "Map", menuName = "ScriptableObjects/MapScriptableObject", order = 1)]
public class MapScriptableObject : ScriptableObject
{
    public List<List<bool>> map = new List<List<bool>>();

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
                if (Random.Range(0, 5) == 0)
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
    }
}
