using System.Collections.Generic;
using UnityEngine;

namespace MBT
{
    [DisallowMultipleComponent]
    [DefaultExecutionOrder(-1000)]
    public class Blackboard : MonoBehaviour
    {
        public List<BlackboardVariable> variables = new List<BlackboardVariable>();
        private readonly Dictionary<string, BlackboardVariable> dictionary = new Dictionary<string, BlackboardVariable>();
        [Tooltip("When set, this blackboard will replace variables with matching names from target parent")]
        public Blackboard masterBlackboard;

        // IMPROVEMENT: https://docs.unity3d.com/ScriptReference/ISerializationCallbackReceiver.html
        private void Awake()
        {
            // Initialize variables by keys
            dictionary.Clear();
            foreach (var var in variables)
            {
                dictionary.Add(var.key, var);
            }
            // Replace variables from master blackboard
            if (masterBlackboard == null) return;
            var parentVars = masterBlackboard.variables;
            foreach (var parentVar in parentVars)
            {
                if (!dictionary.TryGetValue(parentVar.key, out var currentVar)) continue;
                // Ensure that both of them are the same type
                if (currentVar.GetType().IsInstanceOfType(parentVar))
                {
                    // There are matching variables, replace current one with master blackboard var
                    dictionary[parentVar.key] = parentVar;
                }
                else
                {
                    Debug.LogErrorFormat(this, 
                        "Blackboard variable key '{0}' cannot be replaced. " + 
                        "Master blackboard variable of type {1} cannot be assigned to {2}.", 
                        currentVar.key,
                        parentVar.GetType().Name,
                        currentVar.GetType().Name
                    );
                }
            }
        }

        public IEnumerable<BlackboardVariable> GetAllVariables()
        {
            return variables.ToArray();
        }

        public T GetVariable<T>(string key) where T : BlackboardVariable
        {
            return (dictionary.TryGetValue(key, out var val)) ? (T)val : null;
        }

        #if UNITY_EDITOR
        [ContextMenu("Delete all variables", false)]
        protected void DeleteAllVariables()
        {
            foreach (var t in variables)
            {
                UnityEditor.Undo.DestroyObjectImmediate(t);
            }

            variables.Clear();
        }

        [ContextMenu("Delete all variables", true)]
        protected bool HasVariables()
        {
            return variables.Count > 0;
        }

        private void OnValidate()
        {
            if (masterBlackboard != this) return;
            Debug.LogWarning("Master blackboard cannot be the same instance.");
            masterBlackboard = null;
        }
        #endif
    }
}
