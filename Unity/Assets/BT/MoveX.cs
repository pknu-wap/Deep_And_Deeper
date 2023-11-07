using Hero;
using MBT;
using UnityEngine;

namespace BT
{
    [AddComponentMenu("")]
    [MBTNode(name = "Hero/MoveX")]
    public class MoveX : Leaf
    {
        [SerializeField] private float moveSpeed;

        public override NodeResult Execute()
        {
            var inputX = Input.GetAxis("Horizontal");
            if (inputX == 0) return NodeResult.failure;
            
            HeroManager.Instance.SetVelocity(inputX * moveSpeed, null);
            return NodeResult.success;
        }
    }
}