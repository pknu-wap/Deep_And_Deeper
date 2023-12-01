using UnityEngine;

namespace Hero
{
    public class TopViewHero : MonoBehaviour
    {
        [SerializeField] private float moveSpeed = 5f;

        private Animator _animator;
        private SpriteRenderer _spriteRenderer;
        private Rigidbody2D _rigidbody2D;
        private Transform _cameraTransform;

        private void Start()
        {
            _cameraTransform = GameObject.FindGameObjectWithTag("MainCamera").transform;
            _animator = GetComponent<Animator>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _rigidbody2D = GetComponent<Rigidbody2D>();

            if (!MapGenerator.MapGenerator.Instance.saved) return;
            MapGenerator.MapGenerator.Instance.saved = false;
            transform.position = MapGenerator.MapGenerator.Instance.savedPosition;
            _cameraTransform.position = MapGenerator.MapGenerator.Instance.savedCameraPosition;
        }

        public void SavePosition()
        {
            MapGenerator.MapGenerator.Instance.savedPosition = transform.position;
            MapGenerator.MapGenerator.Instance.saved = true;
            MapGenerator.MapGenerator.Instance.savedCameraPosition = _cameraTransform.position;
        }

        private void Update()
        {
            var hInput = Input.GetAxis("Horizontal");
            var vInput = Input.GetAxis("Vertical");

            if (hInput == 0 && vInput == 0)
            {
                _animator.Play("Idle");
                _rigidbody2D.velocity = Vector2.zero;

                if (Input.GetKeyDown(KeyCode.F5))
                    _cameraTransform.position = transform.position + new Vector3(0, 0, -10);
                return;
            }

            if (hInput != 0) _spriteRenderer.flipX = (hInput < 0);

            _animator.Play("Run");
            var hDirection = Vector2.right * hInput;
            var vDirection = Vector2.up * vInput;
            var direction = hDirection + vDirection;
            direction.Normalize();
            _rigidbody2D.velocity = direction * moveSpeed;
        }
    }
}