using System;
using UnityEngine;

namespace MBT
{
    // [RequireComponent(typeof(Blackboard))]
    public abstract class Variable<T> : BlackboardVariable, Observable<T>
    {
        // [HideInInspector]
        [SerializeField]
        protected T val;
        protected event ChangeListener<T> listeners = delegate {};

        // This is required to correctly compare Unity Objects as generic fields
        protected abstract bool ValueEquals(T val1, T val2);

        public void AddListener(ChangeListener<T> listener)
        {
            listeners += listener;
        }

        public void RemoveListener(ChangeListener<T> listener)
        {
            listeners -= listener;
        }

        public T Value
        {
            get => val;
            set
            {
                if (ValueEquals(val, value)) return;
                var oldValue = val;
                val = value;
                listeners.Invoke(oldValue, value);
            }
        }

        protected virtual void OnValidate()
        {
            // Special case: Invoke listeners when there was change in inspector
            listeners.Invoke(val, val);
        }
    }

    public delegate void ChangeListener<in T>(T oldValue, T newValue);

    public interface Observable<out T>
    {
        void AddListener(ChangeListener<T> listener);
        void RemoveListener(ChangeListener<T> listener);
    }

    [Serializable]
    public class VariableReference<T, U> : BaseVariableReference where T : BlackboardVariable
    {
        // Cache
        protected T value;
        [SerializeField]
        protected U constantValue;

        /// <summary>
        /// Returns observable Variable or null if it doesn't exists on blackboard.
        /// </summary>
        public T GetVariable()
        {
            if (value != null) {
                return value;
            }
            if (blackboard == null || string.IsNullOrEmpty(key)) {
                return null;
            }
            value = blackboard.GetVariable<T>(key);
            #if UNITY_EDITOR
            if (value == null)
            {
                Debug.LogWarningFormat(blackboard, "Variable '{0}' does not exists on blackboard.", key);
            }
            #endif
            return value;
        }

        public U GetConstant()
        {
            return constantValue;
        }
    }

    public enum VarRefMode { EnableConstant, DisableConstant }

    [Serializable]
    public abstract class BaseVariableReference
    {
        [SerializeField]
        protected bool useConstant;
        // Additional editor feature to lock switch
        [SerializeField]
        protected VarRefMode mode = VarRefMode.EnableConstant;
        public Blackboard blackboard;
        public string key;

        public virtual bool isConstant => useConstant;

        /// <summary>
        /// Returns true when constant value is valid
        /// </summary>
        /// <value></value>
        protected virtual bool isConstantValid => true;

        /// <summary>
        /// Returns true when variable setup is invalid
        /// </summary>
        public bool isInvalid => (isConstant)? !isConstantValid : blackboard == null || string.IsNullOrEmpty(key);

        protected void SetMode(VarRefMode refMode)
        {
            this.mode = refMode;
            useConstant = (refMode != VarRefMode.DisableConstant) && useConstant;
        }
    }
}
