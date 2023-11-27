using MBT;
using UnityEngine;

namespace Monster.Nodes
{
    [AddComponentMenu("")]
    [MBTNode(name = "Monster/CheckPlayerTrigger")]
    public class CheckPlayerTrigger : Leaf
    {
        private bool _playerTrigger;

        private void OnTriggerEnter2D(Collider2D other)
        {
            _playerTrigger = true;
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            _playerTrigger = false;
        }

        public override NodeResult Execute()
        {
            return _playerTrigger ? NodeResult.success : NodeResult.failure;
        }
    }
}