using MBT;
using UnityEngine;

namespace Hero.Nodes
{
    [AddComponentMenu("")]
    [MBTNode(name = "Hero/Run")]
    public class Run : Leaf
    {
        [SerializeField] private float moveSpeed = 5;

        public override NodeResult Execute()
        {
            var inputX = Input.GetAxis("Horizontal");
            if (inputX == 0) return NodeResult.failure;

            HeroManager.Instance.SetFlipX(inputX < 0);
            HeroManager.Instance.SetVelocityX(inputX * moveSpeed);

            return NodeResult.success;
        }
    }
}