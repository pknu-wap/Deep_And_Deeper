using MBT;
using UnityEngine;

namespace BT
{
    [AddComponentMenu("")]
    [MBTNode(name = "Common/Wait")]
    public class Wait : Leaf
    {
        [SerializeField] private float time;

        private float _timer;

        public override void OnEnter()
        {
            _timer = 0;
        }

        public override NodeResult Execute()
        {
            _timer += DeltaTime;
            return _timer > time ? NodeResult.success : NodeResult.running;
        }
    }
}