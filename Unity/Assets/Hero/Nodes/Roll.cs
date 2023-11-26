using MBT;
using UnityEngine;

namespace Hero.Nodes
{
    [AddComponentMenu("")]
    [MBTNode(name = "Hero/Roll")]
    public class Roll : Leaf
    {
        [SerializeField] private float moveSpeed = 10f;
        [SerializeField] private float cost = 20f;

        public override NodeResult Execute()
        {
            if (HeroManager.Instance.Stamina < cost) return NodeResult.failure;

            var dir = HeroManager.Instance.GetFlipX() ? -1 : 1;
            HeroManager.Instance.SetVelocityX(moveSpeed * dir);
            HeroManager.Instance.ConsumeStamina(cost);

            return NodeResult.success;
        }
    }
}