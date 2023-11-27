using MBT;
using UnityEngine;

namespace Monster.Nodes
{
    [AddComponentMenu("")]
    [MBTNode(name = "Monster/MonsterSetState")]
    public class MonsterSetState : Leaf
    {
        [SerializeField] private string stateName;

        private Monster _monster;

        private void Start()
        {
            _monster = transform.parent == null ? GetComponent<Monster>() : transform.parent.GetComponent<Monster>();
        }

        public override NodeResult Execute()
        {
            _monster.SetState(stateName);
            return NodeResult.success;
        }
    }
}