using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//카메라가 플레이어를 따라다니도록
public class CameraController : MonoBehaviour
{
    private Transform _transformOfTarget;
        
    [SerializeField] private float speed; //카메라 움직이는 속도
    [SerializeField] private Vector2 offset;
    [SerializeField] private float limitMinX, limitMaxX, limitMinY, limitMaxY;
    private float _cameraHalfWidth, _cameraHalfHeight;
    private void Start()
    {
        _transformOfTarget = GameObject.FindGameObjectWithTag ("Player").transform;
        _cameraHalfWidth = Camera.main.aspect * Camera.main.orthographicSize; //카메라의 가로 반 값
        _cameraHalfHeight = Camera.main.orthographicSize; //카메라의 세로 반 값
    }
    private void LateUpdate()
    {
        Vector3 playerPosition = new Vector3(_transformOfTarget.position.x + 9f, _transformOfTarget.position.y,
            _transformOfTarget.position.z);
        
        transform.position = Vector3.Lerp(transform.position, playerPosition, Time.deltaTime * speed);
       
        var clampX = Mathf.Clamp(_transformOfTarget.position.x + offset.x, limitMinX + _cameraHalfWidth,
            limitMaxX - _cameraHalfWidth);
        var clampY = Mathf.Clamp(_transformOfTarget.position.y + offset.y, limitMinY + _cameraHalfHeight,
            limitMaxY - _cameraHalfHeight);
        
       transform.position = new Vector3(clampX,clampY,-10f);
       
    }
}
