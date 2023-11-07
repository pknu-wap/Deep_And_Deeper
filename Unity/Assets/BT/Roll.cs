using Hero;
using MBT;
using UnityEngine;

namespace BT
{
    [AddComponentMenu("")]
    [MBTNode(name = "Hero/Roll")]
    public class Roll : Leaf
    {
        [SerializeField] private float moveSpeed = 7f;

        public override NodeResult Execute()
        {
            var dir = HeroManager.Instance.GetFlipX() ? -1 : 1;
            HeroManager.Instance.SetVelocity(moveSpeed * dir, null);

            return NodeResult.success;
        }
    }
}