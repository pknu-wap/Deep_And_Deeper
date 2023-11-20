using MBT;
using UnityEngine;

namespace Monster.Nodes
{
    [AddComponentMenu("")]
    [MBTNode(name = "Monster/CheckEvent")]
    public class CheckEvent : Leaf
    {
        [SerializeField] private string eventName;
        private bool _eventInvoked;

        private void Start()
        {
            var a = transform.parent.GetComponent<Monster>();
            a.ConnectEventChecker(eventName, this);
        }

        public void Check()
        {
            _eventInvoked = true;
        }

        public override NodeResult Execute()
        {
            if (!_eventInvoked) return NodeResult.failure;

            _eventInvoked = false;
            return NodeResult.success;
        }
    }
}