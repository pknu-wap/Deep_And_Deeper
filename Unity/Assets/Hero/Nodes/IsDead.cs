using System;
using MBT;
using UnityEngine;

namespace Hero.Nodes
{
    [AddComponentMenu("")]
    [MBTNode(name = "Hero/IsDead")]
    public class IsDead : Leaf
    {
        public override NodeResult Execute()
        {
            if (HeroManager.Instance.CheckDead())
            {
                HeroManager.Instance.SetState("Die");
                HeroManager.Instance.GameOver();
                return NodeResult.success;
            }
            else
            {
                return NodeResult.failure;
            }
        }
    }
}