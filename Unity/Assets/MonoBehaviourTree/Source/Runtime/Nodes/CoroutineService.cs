using System.Collections;
using UnityEngine;

namespace MBT
{
    public abstract class CoroutineService : Decorator
    {
        public float interval = 1f;
        public bool callOnEnter = true;
        private Coroutine coroutine;
        private WaitForSeconds waitForSeconds;

        public override void OnEnter()
        {
            // IMPROVEMENT: WaitForSeconds could be initialized in some special node init callback
            if (waitForSeconds == null)
            {
                // Create new WaitForSeconds
                OnValidate();
            }
            coroutine = StartCoroutine(ScheduleTask());
            if (callOnEnter)
            {
                Task();
            }
        }

        public override NodeResult Execute()
        {
            var node = GetChild();
            if (node == null) {
                return NodeResult.failure;
            }
            return node.status is Status.Success or Status.Failure ? NodeResult.From(node.status) : node.runningNodeResult;
        }

        protected abstract void Task();

        public override void OnExit()
        {
            if (coroutine == null)
            {
                return;
            }
            StopCoroutine(coroutine);
            coroutine = null;
        }

        private IEnumerator ScheduleTask()
        {
            while(true)
            {
                yield return waitForSeconds;
                Task();
            }
        }

        protected virtual void OnValidate()
        {
            interval = Mathf.Max(0f, interval);
            waitForSeconds = new WaitForSeconds(interval);
        }
    }
}
