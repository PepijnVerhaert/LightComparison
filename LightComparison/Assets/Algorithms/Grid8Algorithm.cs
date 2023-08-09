using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;
using static UnityEngine.UI.Image;
using UnityEngine.UIElements;
using System.Numerics;
using System;
using Unity.VisualScripting;
using UnityEditor;

public class Grid8Algorithm : LightAlgorithm
{
    private MapScriptableObject _map;
    private List<List<Color>> _colorMap;

    private List<List<float>> _grid;

    public override void SetMaps(MapScriptableObject map, ref List<List<Color>> colorMap)
    {
        _map = map;
        _colorMap = colorMap;
        _grid = new List<List<float>>();
        for (int i = 0; i < _map.mapSize; i++)
        {
            var row = new List<float>();
            for (int j = 0; j < _map.mapSize; j++)
            {
                row.Add(1f);
            }
            _grid.Add(row);
        }
    }

    public override void CalculateLight(int x, int y, int rangeLimit)
    {
        for (int i = 0; i < _grid.Count; i++)
        {
            for (int j = 0; j < _grid[i].Count; j++)
            {
                _grid[i][j] = _map.map[i][j] == true ? 0f : 1f;
            }
        }

        VisibilityWithinCone(x, y, 1, 1, 0, 1);
        VisibilityWithinCone(x, y, 1, 1, 1, 0);

        VisibilityWithinCone(x, y, 1, -1, 1, 0);
        VisibilityWithinCone(x, y, 1, -1, 0, -1);

        VisibilityWithinCone(x, y, -1, -1, 0, -1);
        VisibilityWithinCone(x, y, -1, -1, -1, 0);

        VisibilityWithinCone(x, y, -1, 1, -1, 0);
        VisibilityWithinCone(x, y, -1, 1, 0, 1);

        Color color = Color.black;

        for (int i = 0; i < _colorMap.Count; i++)
        {
            for (int j = 0; j < _colorMap[i].Count; j++)
            {
                if (i == x && j == y)
                {
                    _colorMap[i][j] = Color.white;
                    continue;
                }

                if (_map.map[i][j] == true)
                {
                    _colorMap[i][j] = Color.green * .8f;
                    continue;
                }

                color = Color.white * .7f * _grid[i][j];

                //color = _grid[i][j] >= .15f ? Color.white * .7f : Color.black;

                if (rangeLimit != -1)
                {
                    float distance = Mathf.Abs(x - i) + Mathf.Abs(y - j);
                    color *= 1f - distance / rangeLimit;
                }

                _colorMap[i][j] = color;
            }
        }
    }
    public void VisibilityWithinCone(int oX, int oY, int uX, int uY, int vX, int vY)
    {
        float originX = oX;
        float originY = oY;
        float dimsX = _map.mapSize;
        float dimsY = _map.mapSize;
        int m = 0;
        int k = 0;
        float posX = oX;
        float posY = oY;
        while (posX < dimsX && posX >= 0 && posY < dimsY && posY >= 0)
        {
            while (posX < dimsX && posX >= 0 && posY < dimsY && posY >= 0)
            {
                if (posX != dimsX && posY != dimsY && m + k != 0)
                {
                    int posMinusUX = uX + vX >= 0 ? (int)Math.Max(originX, posX - uX) : (int)Math.Min(originX, posX - uX);
                    int posMinusUY = uY + vY >= 0 ? (int)Math.Max(originY, posY - uY) : (int)Math.Min(originY, posY - uY);

                    int posMinusVX = uX + vX >= 0 ? (int)Math.Max(originX, posX - vX) : (int)Math.Min(originX, posX - vX);
                    int posMinusVY = uY + vY >= 0 ? (int)Math.Max(originY, posY - vY) : (int)Math.Min(originY, posY - vY);

                    _grid[(int)posX][(int)posY] *= (m * _grid[posMinusUX][posMinusUY] + k * _grid[posMinusVX][posMinusVY]) / (m + k);
                }
                k += 1;
                posX += vX;
                posY += vY;
            }
            m += 1;
            k = 0;
            posX = oX + m * uX;
            posY = oY + m * uY;
        }
    }
}
