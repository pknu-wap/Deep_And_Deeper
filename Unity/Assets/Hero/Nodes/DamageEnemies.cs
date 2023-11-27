using System.Collections.Generic;
using MBT;
using UnityEngine;

namespace Hero.Nodes
{
    [AddComponentMenu("")]
    [MBTNode(name = "Hero/DamageEnemies")]
    public class DamageEnemies : Leaf
    {
        [SerializeField] private BoxCollider2D boxCollider2D;
        [SerializeField] private float damage = 50;

        private Dictionary<BoxCollider2D, bool> _triggerDictionary;

        public override NodeResult Execute()
        {
            return NodeResult.success;
        }

        private void OnTriggerEnter2D(BoxCollider2D other)
        {
            _triggerDictionary[other] = true;
        }

        private void OnTriggerExit2D(BoxCollider2D other)
        {
            _triggerDictionary[other] = false;
        }
    }
}