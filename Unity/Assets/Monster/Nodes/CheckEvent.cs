using System;
using MBT;
using UnityEngine;

namespace Monster.Nodes
{
    [AddComponentMenu("")]
    [MBTNode(name = "Monster/CheckEvent")]
    public class CheckEvent : Leaf
    {
        [SerializeField] private string eventName;
        private Monster _monster;

        private void Start()
        {
            _monster = transform.parent.GetComponent<Monster>();
        }

        public override NodeResult Execute()
        {
            return string.Equals(_monster.GetEvent(), eventName, StringComparison.Ordinal)
                ? NodeResult.success
                : NodeResult.failure;
        }
    }
}