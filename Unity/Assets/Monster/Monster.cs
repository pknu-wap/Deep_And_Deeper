using Hero;
using UnityEngine;

namespace Monster
{
    public class Monster : MonoBehaviour
    {
#pragma warning disable 0414
        [SerializeField] private float hp = 1;
#pragma warning restore 0414
        [SerializeField] private float speed = 1;
        private Rigidbody2D _rigidbody2D;
        private Vector3 _originScale;
        private Vector3 _flippedScale;

        private void Start()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _originScale = transform.localScale;
            _flippedScale = _originScale;
            _flippedScale.x *= -1;
        }

        private void Attack()
        {
            Debug.Log("attack!");
        }

        public void Chase()
        {
            var flipped = transform.position.x < HeroManager.Instance.GetPosition().x;
            transform.localScale = flipped ? _flippedScale : _originScale;

            var x = flipped ? 1 : -1;
            _rigidbody2D.velocity = new Vector2(x * speed, _rigidbody2D.velocity.y);
        }
    }
}