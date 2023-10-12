using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BackgroundController : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        var spriteX = _spriteRenderer.bounds.size.x;
        var spriteY = _spriteRenderer.bounds.size.y;

        var screenY = Camera.main.orthographicSize * 2;
        var screenX = screenY / Screen.height * Screen.width;

        transform.localScale = new Vector2(Mathf.Ceil(screenX / spriteX), Mathf.Ceil(screenY / spriteY));
    }
}
