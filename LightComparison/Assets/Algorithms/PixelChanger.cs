using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PixelChanger : MonoBehaviour
{
    [SerializeField] private Sprite _sprite;
    private Texture2D _texture;

    [SerializeField] private LightAlgorithm _lightAlgorithm;

    int _lightX = -1;
    int _lightY = -1;

    public MapScriptableObject _map;
    public List<List<Color>> _colorMap = new List<List<Color>>();

    private int _lightRange = 20;

    public void SetLightPos(int lightX, int lightY)
    {
        _lightX = lightX;
        _lightY = lightY;
    }

    void Start()
    {
        //texture
        if(!_sprite) _sprite = GetComponent<Sprite>();
        _texture = _sprite.texture;

        //maps
        for (int i = 0; i < _map.mapSize; i++)
        {
            var row = new List<Color>();
            for (int j = 0; j < _map.mapSize; j++)
            {
                row.Add(Color.white);
            }
            _colorMap.Add(row);
        }

        _lightAlgorithm.SetMaps(_map, ref _colorMap);
        
        //pos
        _lightX = _texture.width / 2;
        _lightY = _texture.height / 2;
    }

    void Update()
    {
        _lightAlgorithm.CalculateLight(_lightX, _lightY, _lightRange);

        //change texture color
        for (int i = 0; i < _map.mapSize; i++)
        {
            for (int j = 0; j < _map.mapSize; j++)
            {
                _texture.SetPixel(i, j, _colorMap[i][j]);
            }
        }

        _texture.Apply();
    }
}
