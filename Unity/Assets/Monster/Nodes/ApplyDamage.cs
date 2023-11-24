using Hero;
using MBT;
using UnityEngine;

namespace Monster.Nodes
{
    [AddComponentMenu("")]
    [MBTNode(name = "Monster/ApplyDamage")]
    public class ApplyDamage : Leaf
    {
        [SerializeField] private int target; //0: player 1: monster

        public override NodeResult Execute()
        {
            HeroManager.Instance.ApplyDamage(target);
            return NodeResult.success;
        }
    }
}