using MBT;
using UnityEngine;

namespace Monster.Nodes
{
    [AddComponentMenu("")]
    [MBTNode(name = "Monster/Chase")]
    public class Chase : Leaf
    {
        private Monster _monster;

        private void Start()
        {
            _monster = transform.parent == null ? GetComponent<Monster>() : transform.parent.GetComponent<Monster>();
        }

        public override NodeResult Execute()
        {
            _monster.Chase();
            return NodeResult.success;
        }
    }
}