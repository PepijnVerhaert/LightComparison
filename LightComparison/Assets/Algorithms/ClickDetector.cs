using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickDetector : MonoBehaviour
{
    private Camera _camera;

    [SerializeField] private Sprite _sprite;
    private Texture2D _texture;

    [SerializeField] private PixelChanger[] _pixelChangers;

    void Start()
    {
        _camera = Camera.main;

        if (!_sprite) _sprite = GetComponent<Sprite>();
        _texture = _sprite.texture;
    }

    void Update()
    {
        if (!Input.GetMouseButton(0)) return;

        var hit = Physics2D.Raycast(_camera.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        if(hit.collider == null || hit.collider.gameObject != gameObject) return;

        Vector2 offsetFromCenter;
        offsetFromCenter.x = hit.point.x - transform.position.x;
        offsetFromCenter.y = hit.point.y - transform.position.y;

        offsetFromCenter *= _sprite.pixelsPerUnit / transform.localScale.x;

        var posX = _texture.width / 2;
        var posY = _texture.height / 2;
        posX += Mathf.RoundToInt(offsetFromCenter.x);
        posY += Mathf.RoundToInt(offsetFromCenter.y);

        foreach (var pixelChanger in _pixelChangers)
        {
            pixelChanger.SetLightPos(posX, posY);
        }

    }
}
