using MBT;
using UnityEngine;

namespace BT
{
    [AddComponentMenu("")]
    [MBTNode(name = "TimeSequence", order = 160)]
    public class TimeSequence : Composite
    {
        [SerializeField] private float duration;
        private bool _continuouslySucceeding;
        private int _index;
        private NodeResult _nodeResult;
        private float _timer;

        public override void OnAllowInterrupt()
        {
            if (random) ShuffleList(children);
        }

        public override void OnEnter()
        {
            _index = 0;
            _timer = 0;
            _nodeResult = NodeResult.failure;
            _continuouslySucceeding = true;
        }

        public override void OnBehaviourTreeAbort()
        {
            // Do not continue from last index
            _index = 0;
            _timer = 0;
            _nodeResult = NodeResult.failure;
            _continuouslySucceeding = true;
        }

        public override NodeResult Execute()
        {
            while (_index < children.Count)
            {
                var child = children[_index];

                if (child.status == Status.Success)
                {
                    _index++;
                }
                else if (child.status == Status.Failure)
                {
                    _index++;
                    _continuouslySucceeding = false;
                }

                return child.runningNodeResult;
            }

            if (_continuouslySucceeding) _nodeResult = NodeResult.success;

            _timer += DeltaTime;
            if (_timer > duration) return _nodeResult;

            _index = 0;
            _continuouslySucceeding = true;
            return NodeResult.running;
        }
    }
}