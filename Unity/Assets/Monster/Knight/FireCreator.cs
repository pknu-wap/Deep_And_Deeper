using UnityEngine;

namespace Monster.Knight
{
    public class FireCreator : MonoBehaviour
    {
        public int dir;

        [SerializeField] private GameObject fire;

        private float _destroyTimer;
        private float _timer;
        private Vector3 _pos;

        private float _dist = 1;
        private float _waitTime = 0.2f;
        private const float DestroyTime = 1.5f;

        private void Start()
        {
            _destroyTimer = DestroyTime;
            _timer = _waitTime;
        }

        private void Update()
        {
            _destroyTimer -= Time.deltaTime;
            if (_destroyTimer <= 0)
            {
                Destroy(gameObject);
                return;
            }

            _timer -= Time.deltaTime;
            if (_timer > 0) return;

            _waitTime = Mathf.Max(0.03f, _waitTime * 0.65f);
            _timer = _waitTime;

            Instantiate(fire, transform.position + _pos, Quaternion.identity);
            _pos.x += _dist * dir;

            _dist *= 1.1f;
        }
    }
}