using System.Collections;
using System.Collections.Generic;
using MBT;
using UnityEngine;

namespace Hero.Nodes
{
    [AddComponentMenu("")]
    [MBTNode(name = "Hero/DamageEnemies")]
    public class DamageEnemies : Leaf
    {
        [SerializeField] private float damage = 50;

        private readonly Dictionary<Collider2D, bool> _triggerDictionary = new();

        public override NodeResult Execute()
        {
            var removeList = new ArrayList();

            foreach (var (key, value) in _triggerDictionary)
            {
                if (!value)
                {
                    removeList.Add(key);
                    continue;
                }

                // ReSharper disable once Unity.PerformanceCriticalCodeInvocation
                var monster = key.transform.parent.GetComponent<Monster.Monster>();

                // ReSharper disable once Unity.PerformanceCriticalCodeNullComparison
                if (monster == null)
                {
                    removeList.Add(key);
                    continue;
                }

                monster.OnDamaged(damage);
            }

            foreach (Collider2D key in removeList)
            {
                _triggerDictionary.Remove(key);
            }

            return NodeResult.success;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            _triggerDictionary[other] = true;
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            _triggerDictionary[other] = false;
        }
    }
}