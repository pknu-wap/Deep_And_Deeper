using UnityEngine;

namespace MBT
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(MonoBehaviourTree))]
    public class MBTExecutor : MonoBehaviour
    {
        public MonoBehaviourTree monoBehaviourTree;

        private void Reset()
        {
            monoBehaviourTree = GetComponent<MonoBehaviourTree>();
            OnValidate();
        }

        private void Update()
        {
            monoBehaviourTree.Tick();
        }

        private void OnValidate()
        {
            if (monoBehaviourTree == null || monoBehaviourTree.parent == null) return;
            monoBehaviourTree = null;
            Debug.LogWarning("Subtree should not be target of update. Select parent tree instead.", this.gameObject);
        }
    }
}
