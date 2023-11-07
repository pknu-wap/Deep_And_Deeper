using Hero;
using MBT;
using UnityEngine;

namespace BT
{
    [AddComponentMenu("")]
    [MBTNode(name = "Common/SetState")]
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