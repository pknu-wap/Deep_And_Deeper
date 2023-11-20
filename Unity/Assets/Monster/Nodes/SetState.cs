using MBT;
using UnityEngine;

namespace Monster.Nodes
{
    [AddComponentMenu("")]
    [MBTNode(name = "Monster/SetState")]
    public class SetState : Leaf
    {
        [SerializeField] private string stateName;
        private Animator _animator;

        private void Start()
        {
            _animator = transform.parent == null ? GetComponent<Animator>() : transform.parent.GetComponent<Animator>();
        }

        public override NodeResult Execute()
        {
            _animator.Play(stateName);
            return NodeResult.success;
        }
    }
}