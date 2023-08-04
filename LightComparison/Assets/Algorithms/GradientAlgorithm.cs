using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GradientAlgorithm : LightAlgorithm
{
    private MapScriptableObject _map;
    private List<List<Color>> _colorMap;

    public override void SetMaps(MapScriptableObject map, ref List<List<Color>> colorMap)
    {
        _map = map;
        _colorMap = colorMap;
    }


    public override void CalculateLight(int x, int y, int rangeLimit)
    {
        var color = Color.white;
        float mapSize = _map.mapSize;
        for (int i = 0; i < _map.mapSize; ++i)
        {
            color.r = i / mapSize;
            for (int j = 0; j < _map.mapSize; ++j)
            {
                if (rangeLimit != -1 && (i < x - rangeLimit || i > x + rangeLimit || j < y - rangeLimit || j > y + rangeLimit))
                {
                    _colorMap[i][j] = Color.black;
                    continue;
                }

                if(i == x && j == y)
                {
                    _colorMap[i][j] = Color.yellow;
                    continue;
                }
                
                color.g = j / mapSize;
                _colorMap[i][j] = color;
            }
        }
    }
}
