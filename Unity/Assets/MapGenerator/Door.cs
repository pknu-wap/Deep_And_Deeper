using Hero;
using UnityEngine;

namespace MapGenerator
{
    public class Door : MonoBehaviour
    {
        [SerializeField] private int dir = -1;
        [SerializeField] private Vector2 cameraSize = new Vector2(19.2f, 10.8f);

        private GameObject _mainCamera;

        private readonly float[] _dy = { -1, 0, 1, 0 };
        private readonly float[] _dx = { 0, -1, 0, 1 };

        private const float TransformMultiplier = 6;

        private void Start()
        {
            _mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            var newPosition = _mainCamera.transform.position;
            newPosition.x += _dx[dir] * cameraSize.x;
            newPosition.y += _dy[dir] * cameraSize.y;
            _mainCamera.transform.position = newPosition;
            HeroManager.Instance.topViewTransform.transform.Translate(
                _dx[dir] * TransformMultiplier,
                _dy[dir] * TransformMultiplier,
                0
            );
            MapGenerator.Instance.savedCameraPosition = newPosition;
        }
    }
}