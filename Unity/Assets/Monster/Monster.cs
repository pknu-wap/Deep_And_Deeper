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
        private Dictionary<string, CheckEvent> _eventCheckers;

        private void Start()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _originScale = transform.localScale;
            _flippedScale = _originScale;
            _flippedScale.x *= -1;
        }

        public void ConnectEventChecker(string eventName, CheckEvent eventChecker)
        {
            _eventCheckers[eventName] = eventChecker;
        }

        private void OnEvent(string eventName)
        {
            _eventCheckers[eventName].Check();
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