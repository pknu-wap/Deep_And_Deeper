using System.Collections.Generic;
using Hero;
using Monster.Nodes;
using UnityEngine;

namespace Monster
{
    public class Monster : MonoBehaviour
    {
        // [SerializeField] private float hp = 1;
        [SerializeField] private float speed = 1;
        private Rigidbody2D _rigidbody2D;
        private Vector3 _originScale;
        private Vector3 _flippedScale;
        private string _currentEvent;

        private void Start()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _originScale = transform.localScale;
            _flippedScale = _originScale;
            _flippedScale.x *= -1;
        }

        public void SetEvent(string eventName)
        {
            _currentEvent = eventName;
        }

        public string GetEvent()
        {
            return _currentEvent;
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