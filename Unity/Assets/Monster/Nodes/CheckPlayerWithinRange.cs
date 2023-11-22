using System;
using Hero;
using MBT;
using UnityEngine;

namespace Monster.Nodes
{
    [AddComponentMenu("")]
    [MBTNode(name = "Monster/CheckPlayerWithinRange")]
    public class CheckPlayerWithinRange : Leaf
    {
        [SerializeField] private float xRange = 8;
        [SerializeField] private float yRange = 2;

        private bool IsPlayerWithinRange()
        {
            var playerPosition = HeroManager.Instance.GetPosition();
            var thisPosition = transform.position;

            return Math.Abs(playerPosition.x - thisPosition.x) <= xRange
                   && Math.Abs(playerPosition.y - thisPosition.y) <= yRange;
        }

        public override NodeResult Execute()
        {
            return IsPlayerWithinRange() ? NodeResult.success : NodeResult.failure;
        }
    }
}