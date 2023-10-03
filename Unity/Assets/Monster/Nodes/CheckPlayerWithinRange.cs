using System;
using MBT;
using UnityEngine;

namespace Monster.Nodes
{
    [AddComponentMenu("")]
    [MBTNode(name = "Monster/CheckPlayerWithinRange")]
    public class CheckPlayerWithinRange : Leaf
    {
        [SerializeField] private Transform player;
        [SerializeField] private RangeOption rangeOption = RangeOption.X;
        [SerializeField] private float range;

        private bool IsPlayerWithinRange()
        {
            var playerPosition = player.position;
            var thisPosition = transform.position;

            return rangeOption switch
            {
                RangeOption.X => Math.Abs(playerPosition.x - thisPosition.x) <= range,
                RangeOption.Y => Math.Abs(playerPosition.y - thisPosition.y) <= range,
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        public override NodeResult Execute()
        {
            return IsPlayerWithinRange() ? NodeResult.success : NodeResult.failure;
        }

        private enum RangeOption
        {
            X,
            Y
        }
    }
}