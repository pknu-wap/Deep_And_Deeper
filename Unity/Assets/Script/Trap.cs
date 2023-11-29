using Hero;
using UnityEngine;

namespace Script
{
    public class Trap : MonoBehaviour
    {
        [SerializeField] private float damage = 50;
        [SerializeField] private float damageCooldown = 1f;

        private bool _heroTrigger;
        private bool _isReady = true;
        private float _damageTimer;

        private void Damage()
        {
            HeroManager.Instance.OnDamaged(damage);
            _isReady = false;
            _damageTimer = damageCooldown;
        }

        private void Update()
        {
            if (_isReady) return;

            _damageTimer -= Time.deltaTime;

            if (_damageTimer > 0) return;

            _damageTimer = 0;
            _isReady = true;

            if (!_heroTrigger) return;

            Damage();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            _heroTrigger = true;

            if (!_isReady) return;

            Damage();
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            _heroTrigger = false;
        }
    }
}