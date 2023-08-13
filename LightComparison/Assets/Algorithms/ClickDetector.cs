using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickDetector : MonoBehaviour
{
    private Camera _camera;

    [SerializeField] private Sprite _sprite;
    private Texture2D _texture;

    private MapScriptableObject _map;

    [SerializeField] private PixelChanger[] _pixelChangers;

    private bool _wasRightDown = false;
    private bool _wasPlacingWall = false;

    void Start()
    {
        _camera = Camera.main;

        if (!_sprite) _sprite = GetComponent<Sprite>();
        _texture = _sprite.texture;

        _map = _pixelChangers[0].GetMap();
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            var hit = Physics2D.Raycast(_camera.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit.collider == null || hit.collider.gameObject != gameObject) return;

            Vector2 offsetFromCenter;
            offsetFromCenter.x = hit.point.x - transform.position.x;
            offsetFromCenter.y = hit.point.y - transform.position.y;

            offsetFromCenter *= _sprite.pixelsPerUnit / transform.localScale.x;

            var posX = _texture.width / 2;
            var posY = _texture.height / 2;
            posX += Mathf.RoundToInt(offsetFromCenter.x - .5f);
            posY += Mathf.RoundToInt(offsetFromCenter.y - .5f);

            foreach (var pixelChanger in _pixelChangers)
            {
                pixelChanger.SetLightPos(posX, posY);
            }
        }

        if(Input.GetMouseButton(1))
        {
            var hit = Physics2D.Raycast(_camera.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit.collider == null || hit.collider.gameObject != gameObject) return;

            Vector2 offsetFromCenter;
            offsetFromCenter.x = hit.point.x - transform.position.x;
            offsetFromCenter.y = hit.point.y - transform.position.y;

            offsetFromCenter *= _sprite.pixelsPerUnit / transform.localScale.x;

            var posX = _texture.width / 2;
            var posY = _texture.height / 2;
            posX += Mathf.RoundToInt(offsetFromCenter.x - .5f);
            posY += Mathf.RoundToInt(offsetFromCenter.y - .5f);


            if (!_wasRightDown)
            {
                _wasRightDown = true;
                _wasPlacingWall = !_map.map[posX][posY];
            }

            _map.map[posX][posY] = _wasPlacingWall;
            foreach (var pixelChanger in _pixelChangers)
            {
                pixelChanger.ResetFPS();
            }
        }
        else
        {
            _wasRightDown = false;
        }
    }
}
