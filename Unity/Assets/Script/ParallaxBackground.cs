using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    [SerializeField]private float parallaxScale = 1f;  // 배경 스프라이트의 움직임 비율
    private Transform _transformOfCamera;
    private Vector3 previousCamPos;
    
    private SpriteRenderer _spriteRenderer;
    private float _spriteX;
    private float _screenX;
    
    private void Start()
    {
        _transformOfCamera = Camera.main.transform;
        previousCamPos = _transformOfCamera.position;
        
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteX = _spriteRenderer.bounds.size.x; //카메라 크기에 맞춰진 후 스프라이트의 가로 크기(스케일 반영됨)
        
        var screenY = Camera.main.orthographicSize * 2; //카메라 세로 크기
        _screenX = screenY / Screen.height * Screen.width; //카메라 가로 크기
    }

    private void Update()
    {
        float parallax = (previousCamPos.x - _transformOfCamera.position.x) * parallaxScale;

        // 현재 배경 스프라이트의 위치를 업데이트
        Vector3 backgroundTargetPos = new Vector3(transform.position.x + parallax, transform.position.y, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, backgroundTargetPos, 1f);
        
        // 카메라의 이전 위치 업데이트
        previousCamPos = _transformOfCamera.position;

        if (transform.position.x + _spriteX / 2f <= _transformOfCamera.position.x - _screenX / 2f) //현재 스프라이트의 오른쪽 가장자리 x값이 카메라의 왼쪽 가장자리 x값보다 작거나 같아진다면
        {
            //스프라이트를 카메라의 오른쪽 가장자리와 맞닿도록 만들기
            transform.position = _transformOfCamera.position + new Vector3(_screenX, 0f, 10f);
        }
        else if(transform.position.x - _spriteX/2f > _transformOfCamera.position.x + _screenX / 2f)
        {
            transform.position = _transformOfCamera.position - new Vector3(_screenX, 0f, -10f);
        }
    }
}