using UnityEngine;

namespace Monster.Giant
{
    public class Clock : MonoBehaviour
    {
        [SerializeField] private float rotateRate = 2f;

        private RectTransform _rect;

        private void Start()
        {
            _rect = GetComponent<RectTransform>();
        }

        private void Update()
        {
            _rect.Rotate(new Vector3(0, 0, Time.deltaTime * rotateRate));
        }
    }
}