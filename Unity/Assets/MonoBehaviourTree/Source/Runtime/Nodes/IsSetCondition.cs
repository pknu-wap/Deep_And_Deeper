using UnityEngine;

namespace MBT
{
    [AddComponentMenu("")]
    [MBTNode(name = "Conditions/Is Set Condition")]
    public class IsSetCondition : Condition
    {
        public Abort abort;
        public bool invert;
        public Type type = Type.Boolean;
        public BoolReference boolReference = new BoolReference(VarRefMode.DisableConstant);
        public GameObjectReference objectReference = new GameObjectReference(VarRefMode.DisableConstant);
        public TransformReference transformReference = new TransformReference(VarRefMode.DisableConstant);

        protected override bool Check()
        {
            return type switch
            {
                Type.Boolean => (boolReference.Value) ^ invert,
                Type.GameObject => (objectReference.Value != null) ^ invert,
                Type.Transform => (transformReference.Value != null) ^ invert,
                _ => invert
            };
        }

        public override void OnAllowInterrupt()
        {
            if (abort == Abort.None) return;
            ObtainTreeSnapshot();
            switch (type)
            {
                case Type.Boolean:
                    boolReference.GetVariable().AddListener(OnVariableChange);
                    break;
                case Type.GameObject:
                    objectReference.GetVariable().AddListener(OnVariableChange);
                    break;
                case Type.Transform:
                    transformReference.GetVariable().AddListener(OnVariableChange);
                    break;
            }
        }

        public override void OnDisallowInterrupt()
        {
            if (abort == Abort.None) return;
            switch (type)
            {
                case Type.Boolean:
                    boolReference.GetVariable().RemoveListener(OnVariableChange);
                    break;
                case Type.GameObject:
                    objectReference.GetVariable().RemoveListener(OnVariableChange);
                    break;
                case Type.Transform:
                    transformReference.GetVariable().RemoveListener(OnVariableChange);
                    break;
            }
        }

        private void OnVariableChange(bool oldValue, bool newValue)
        {
            EvaluateConditionAndTryAbort(abort);
        }

        private void OnVariableChange(GameObject oldValue, GameObject newValue)
        {
            EvaluateConditionAndTryAbort(abort);
        }

        private void OnVariableChange(Transform oldValue, Transform newValue)
        {
            EvaluateConditionAndTryAbort(abort);
        }

        public override bool IsValid()
        {
            return type switch
            {
                Type.Boolean => !boolReference.isInvalid,
                Type.GameObject => !objectReference.isInvalid,
                Type.Transform => !transformReference.isInvalid,
                _ => true
            };
        }

        public enum Type
        {
            Boolean, GameObject, Transform
        }
    }
}
