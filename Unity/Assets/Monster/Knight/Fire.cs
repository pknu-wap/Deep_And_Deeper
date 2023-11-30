using UnityEngine;

namespace Monster.Knight
{
    public class Fire : MonoBehaviour
    {
        [SerializeField] private float removeTime;

        private float _timer;

        private void Start()
        {
            _timer = removeTime;
        }

        private void Update()
        {
            _timer -= Time.deltaTime;
            if (_timer > 0) return;

            Destroy(gameObject);
        }
    }
}