using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//배경을 카메라 크기에 맞추기
public class BackgroundController : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    private Transform _transform;
    private GameObject _otherObject;

    [SerializeField] private Transform _otherObjTransform;
    
    private void Awake()
    {
        //background sprites 카메라에 맞춘 크기 조절
        _spriteRenderer = GetComponent<SpriteRenderer>();
        var spriteX = _spriteRenderer.bounds.size.x; //스프라이트의 가로 크기
        var spriteY = _spriteRenderer.bounds.size.y; //스프라이트의 세로 크기

        var screenY = Camera.main.orthographicSize * 2; //카메라 세로 크기
        var screenX = screenY / Screen.height * Screen.width; //카메라 가로 크기

        transform.localScale = new Vector3(screenX / spriteX, screenY / spriteY);
    }

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        var spriteX = _spriteRenderer.bounds.size.x; //카메라 크기에 맞춰진 후 스프라이트의 가로 크기(스케일 반영됨)
        
        if (_otherObjTransform != null)
        {
            transform.position = _otherObjTransform.position +
                                 new Vector3(spriteX, 0f,10f);
        }
        else
            transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y);
    }
}