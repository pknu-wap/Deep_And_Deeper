using System;
using MBT;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Monster.Nodes
{
    [AddComponentMenu("")]
    [MBTNode(name = "Monster/CheckRandom")]
    public class CheckRandom : Leaf
    {
        [SerializeField] private float potential;

        private void Start()
        {
            potential *= 100;
        }

        public override NodeResult Execute()
        {
            var rand = Random.Range(1, 10001);

            return rand <= potential ? NodeResult.success : NodeResult.failure;
        }
    }
}