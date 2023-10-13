using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    [SerializeField]private float parallaxScale = 1f;  // 배경 스프라이트의 움직임 비율
    private Transform _transformOfCamera;
    private Vector3 previousCamPos;

    private void Start()
    {
        _transformOfCamera = Camera.main.transform;
        previousCamPos = _transformOfCamera.position;
    }

    private void Update()
    {
        // 카메라의 이동량 계산
        float parallax = (previousCamPos.x - _transformOfCamera.position.x) * parallaxScale;

        // 현재 배경 스프라이트의 위치를 업데이트
        Vector3 backgroundTargetPos = new Vector3(transform.position.x + parallax, transform.position.y, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, backgroundTargetPos, 1f);

        // 카메라의 이전 위치 업데이트
        previousCamPos = _transformOfCamera.position;
    }
}