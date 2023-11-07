using MBT;
using UnityEngine;

namespace Hero.Nodes
{
    [AddComponentMenu("")]
    [MBTNode(name = "Hero/Roll")]
    public class Roll : Leaf
    {
        [SerializeField] private float moveSpeed = 10f;

        public override NodeResult Execute()
        {
            var dir = HeroManager.Instance.GetFlipX() ? -1 : 1;
            HeroManager.Instance.SetVelocityX(moveSpeed * dir);

            return NodeResult.success;
        }
    }
}