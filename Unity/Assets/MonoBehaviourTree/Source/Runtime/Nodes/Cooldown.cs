using UnityEngine;

namespace MBT
{
    [AddComponentMenu("")]
    [MBTNode(name = "Decorators/Cooldown")]
    public class Cooldown : Decorator
    {
        public AbortTypes abort = AbortTypes.None;
        [Space]
        public FloatReference time = new FloatReference(1f);
        [Tooltip("When set to true, there will be no cooldown when child node returns failure")]
        public bool resetOnChildFailure;
        private float cooldownTime;
        private bool entered;
        private bool childFailed;
        
        public enum AbortTypes
        {
            None, LowerPriority
        }

        public override void OnAllowInterrupt()
        {
            if (abort == AbortTypes.LowerPriority)
            {
                ObtainTreeSnapshot();
            }
        }

        public override NodeResult Execute()
        {
            var node = GetChild();
            if (node == null) {
                return NodeResult.failure;
            }
            switch (node.status)
            {
                case Status.Success:
                    return NodeResult.success;
                case Status.Failure:
                    // If reset option is enabled flag will be raised and set true
                    childFailed = resetOnChildFailure;
                    return NodeResult.failure;
            }

            if (!(cooldownTime <= Time.time)) return NodeResult.failure;
            entered = true;
            return node.runningNodeResult;

        }

        public override void OnExit()
        {
            // Setup cooldown and event when child was entered
            // Check reset option too
            if (entered && !childFailed)
            {
                cooldownTime = Time.time + time.Value;
                // For LowerPriority try to abort after given time
                if (abort == AbortTypes.LowerPriority)
                {
                    behaviourTree.onTick += OnBehaviourTreeTick;
                }
            }
            // Reset flags
            entered = false;
            childFailed = false;
        }

        public override void OnDisallowInterrupt()
        {
            behaviourTree.onTick -= OnBehaviourTreeTick;
        }

        private void OnBehaviourTreeTick()
        {
            if (!(cooldownTime <= Time.time)) return;
            // Task should be aborted, so there is no need to listen anymore
            behaviourTree.onTick -= OnBehaviourTreeTick;
            TryAbort(Abort.LowerPriority);
        }
    }
}
