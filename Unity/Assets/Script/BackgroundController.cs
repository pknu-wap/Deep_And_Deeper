using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundController : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    private Transform _transform;
    private void Awake()
    {
        //background sprites 카메라에 맞춘 크기 조절
        _spriteRenderer = GetComponent<SpriteRenderer>();
        var spriteX = _spriteRenderer.bounds.size.x;
        var spriteY = _spriteRenderer.bounds.size.y;

        var screenY = Camera.main.orthographicSize * 2; //카메라 가로
        var screenX = screenY / Screen.height * Screen.width; //카메라 세로

        transform.localScale = new Vector2(Mathf.Ceil(screenX / spriteX), Mathf.Ceil(screenY / spriteY));
    }
}
