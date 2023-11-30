using UnityEngine;

namespace Monster.Knight
{
    public class FireCreator : MonoBehaviour
    {
        public int dir;

        [SerializeField] private GameObject fire;
        [SerializeField] private float dist;
        [SerializeField] private float waitTime;
        [SerializeField] private float destroyTime;

        private float _destroyTimer;
        private float _timer;
        private Vector3 _pos;
        private float _yScale = 1;
        private float _xScale = 1;

        private void Start()
        {
            _destroyTimer = destroyTime;
            _timer = waitTime;
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

            waitTime = Mathf.Max(0.03f, waitTime * 0.65f);
            _timer = waitTime;

            var fireObject = Instantiate(fire, transform.position + _pos, Quaternion.identity);
            var newScale = fireObject.transform.localScale;
            newScale.x *= _xScale;
            newScale.y *= _yScale;
            fireObject.transform.localScale = newScale;
            _yScale *= 1.12f;
            _xScale *= 1.15f;
            _pos.x += dist * dir;
            _pos.y += 293 * 0.0008f;
            dist *= 1.1f;
        }
    }
}