using Hero;
using MBT;
using UnityEngine;

namespace Monster.Nodes
{
    [AddComponentMenu("")]
    [MBTNode(name = "Monster/ApplyDamage")]
    public class ApplyDamage : Leaf
    {
        [SerializeField] private float damage;

        public override NodeResult Execute()
        {
            HeroManager.Instance.ApplyDamage(damage);
            return NodeResult.success;
        }
    }
}