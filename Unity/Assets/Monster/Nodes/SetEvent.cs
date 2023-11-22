using MBT;
using UnityEngine;

namespace Monster.Nodes
{
    [AddComponentMenu("")]
    [MBTNode(name = "Monster/SetEvent")]
    public class SetEvent : Leaf
    {
        [SerializeField] private string eventName;
        private Monster _monster;

        private void Start()
        {
            _monster = transform.parent.GetComponent<Monster>();
        }

        public override NodeResult Execute()
        {
            _monster.SetEvent(eventName);
            return NodeResult.success;
        }
    }
}