using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PixelChanger : MonoBehaviour
{
    [SerializeField] private Sprite _sprite;
    private Texture2D _texture;

    [SerializeField] private LightAlgorithm _lightAlgorithm;

    int _lightX = -1;
    int _lightY = -1;

    static MapScriptableObject _map;
    static bool _isMapOpened = false;

    public List<List<Color>> _colorMap = new List<List<Color>>();

    private int _lightRange = -1;

    //maps
    /*
     * Map
     * Empty64
     */

    [SerializeField] public string _mapKey = "";

    public void SetRange(float range)
    {
        int rangeI = (int)range;
        _lightRange = rangeI;

        if(_lightRange >= _map.mapSize)
        {
            _lightRange = _map.mapSize-1;
        }
    }

    public void SetLightPos(int lightX, int lightY)
    {
        _lightX = lightX;
        _lightY = lightY;
    }

    public ref MapScriptableObject GetMap()
    {
        OpenMap();
        return ref _map;
    }

    void Start()
    {
        //texture
        if(!_sprite) _sprite = GetComponent<Sprite>();
        _texture = _sprite.texture;

        //maps
        OpenMap();

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

    private void OnDisable()
    {
        SaveMap();
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

    private void LoadMap()
    {
        var jsonMap = PlayerPrefs.GetString(_mapKey);
        //_map = JsonUtility.FromJson<MapScriptableObject>(jsonMap);
        MapScriptableObject map = ScriptableObject.CreateInstance<MapScriptableObject>();
        map.Init();
        JsonUtility.FromJsonOverwrite(jsonMap, map);
        _map = map;
        _map.To2D();
    }

    private void OpenMap()
    {
        if (_isMapOpened) return;
        if (PlayerPrefs.HasKey(_mapKey))
        {
            LoadMap();
        }
        else
        {
            CreateMap();
            SaveMap();
        }
        _isMapOpened = true;
    }


    private void CreateMap()
    {
        _map = ScriptableObject.CreateInstance<MapScriptableObject>();
        _map.Init();
    }

    private void SaveMap()
    {
        _map.To1D();

        var jsonMap = JsonUtility.ToJson(_map, true);

        PlayerPrefs.SetString(_mapKey, jsonMap);
        PlayerPrefs.Save();
    }
}
