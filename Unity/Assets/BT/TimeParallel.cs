using MBT;
using UnityEngine;

namespace BT
{
    [AddComponentMenu("")]
    [MBTNode(name = "TimeParallel", order = 170)]
    public class TimeParallel : Composite
    {
        [SerializeField] private float duration;
        private int _index;
        private float _timer;

        public override void OnAllowInterrupt()
        {
            if (random) ShuffleList(children);
        }

        public override void OnEnter()
        {
            _index = 0;
            _timer = 0;
        }

        public override void OnBehaviourTreeAbort()
        {
            // Do not continue from last index
            _index = 0;
            _timer = 0;
        }

        public override NodeResult Execute()
        {
            while (_index < children.Count)
            {
                var child = children[_index];

                if (child.status is Status.Success or Status.Failure)
                {
                    _index++;
                }

                return child.runningNodeResult;
            }

            _timer += DeltaTime;
            if (_timer > duration) return NodeResult.success;

            _index = 0;
            return NodeResult.running;
        }
    }
}