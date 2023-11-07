using MBT;
using UnityEngine;

namespace Hero.Nodes
{
    [AddComponentMenu("")]
    [MBTNode(name = "Hero/SetState")]
    public class SetState : Leaf
    {
        [SerializeField] private string stateName;

        public override NodeResult Execute()
        {
            HeroManager.Instance.SetState(stateName);
            return NodeResult.success;
        }
    }
}