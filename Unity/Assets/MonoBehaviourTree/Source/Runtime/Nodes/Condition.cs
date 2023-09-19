namespace MBT
{
    public abstract class Condition : Decorator
    {
        private bool lastConditionCheckResult;

        public override NodeResult Execute()
        {
            var node = GetChild();
            if (node == null) {
                return NodeResult.failure;
            }
            if (node.status is Status.Success or Status.Failure) {
                return NodeResult.From(node.status);
            }
            lastConditionCheckResult = Check();
            return lastConditionCheckResult == false ? NodeResult.failure : node.runningNodeResult;
        }

        /// <summary>
        /// Reevaluate condition and try to abort the tree if required
        /// </summary>
        /// <param name="abortType">Abort type</param>
        protected void EvaluateConditionAndTryAbort(Abort abortType)
        {
            var c = Check();
            if (c == lastConditionCheckResult) return;
            lastConditionCheckResult = c;
            TryAbort(abortType);
        }

        /// <summary>
        /// Method called to check condition
        /// </summary>
        /// <returns>Condition result</returns>
        protected abstract bool Check();
    }
}
