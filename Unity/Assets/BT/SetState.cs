using MBT;
using UnityEngine;

namespace BT
{
    [AddComponentMenu("")]
    [MBTNode(name = "Common/SetState")]
    public class SetState : Leaf
    {
        [SerializeField] private string stateName;
        private Animator _animator;

        private void Start()
        {
            _animator = GetComponent<Animator>();
        }

        public override NodeResult Execute()
        {
            _animator.Play(stateName);
            return NodeResult.success;
        }
    }
}