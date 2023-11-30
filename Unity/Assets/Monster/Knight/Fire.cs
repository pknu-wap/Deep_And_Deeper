using UnityEngine;

namespace Monster.Knight
{
    public class Fire : MonoBehaviour
    {
        private float _timer;

        private const float RemoveTime = 0.5f;

        private void Start()
        {
            _timer = RemoveTime;
        }

        private void Update()
        {
            _timer -= Time.deltaTime;
            if (_timer > 0) return;

            Destroy(gameObject);
        }
    }
}