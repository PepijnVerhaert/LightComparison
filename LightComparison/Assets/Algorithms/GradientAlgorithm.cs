using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
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
            for (int j = 0; j < _map.mapSize; ++j)
            {
                if (_map.map[i][j])
                {
                    _colorMap[i][j] = Color.green * .8f;
                    continue;
                }

                if (rangeLimit != -1 && Mathf.Abs(x - i) + Mathf.Abs(y - j) > rangeLimit)
                {
                    _colorMap[i][j] = Color.black;
                    continue;
                }

                if(i == x && j == y)
                {
                    _colorMap[i][j] = Color.white;
                    continue;
                }

                color.r = i / mapSize;
                color.g = j / mapSize;
                color.b = 1f;

                if(rangeLimit != -1)
                {
                    float distance = Mathf.Abs(x - i) + Mathf.Abs(y - j);
                    color *= 1f - distance / rangeLimit;
                }

                _colorMap[i][j] = color;
            }
        }
    }
}
