using MBT;
using UnityEngine;

namespace Hero.Nodes
{
    [AddComponentMenu("")]
    [MBTNode(name = "Hero/Run")]
    public class Run : Leaf
    {
        public override NodeResult Execute()
        {
            return HeroManager.Instance.Run();
        }
    }
}