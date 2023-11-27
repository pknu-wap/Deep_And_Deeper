using MBT;
using UnityEngine;

namespace Monster.Nodes
{
    [AddComponentMenu("")]
    [MBTNode(name = "Monster/CheckMonsterDead")]
    public class CheckMonsterDead : Leaf
    {
        private Monster _monster;

        private void Start()
        {
            _monster = transform.parent == null ? GetComponent<Monster>() : transform.parent.GetComponent<Monster>();
        }

        public override NodeResult Execute()
        {
            return _monster.isDead ? NodeResult.success : NodeResult.failure;
        }
    }
}