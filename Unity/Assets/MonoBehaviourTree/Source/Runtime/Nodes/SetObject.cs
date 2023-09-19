using UnityEngine;

namespace MBT
{
    [AddComponentMenu("")]
    [MBTNode(name = "Tasks/Set Object")]
    public class SetObject : Leaf
    {
        [SerializeField]
        private Type type = Type.Transform;
        public TransformReference sourceTransform;
        public GameObjectReference sourceGameObject;
        public TransformReference destinationTransform = new TransformReference(VarRefMode.DisableConstant);
        public GameObjectReference destinationGameObject = new GameObjectReference(VarRefMode.DisableConstant);

        public override NodeResult Execute()
        {
            if (type == Type.Transform)
            {
                destinationTransform.Value = sourceTransform.Value;
            }
            else
            {
                destinationGameObject.Value = sourceGameObject.Value;
            }
            return NodeResult.success;
        }

        public override bool IsValid()
        {
            // Custom validation to allow nulls in source objects
            return type switch
            {
                Type.Transform => !(sourceTransform == null || IsInvalid(sourceTransform) ||
                                    destinationTransform.isInvalid),
                Type.GameObject => !(sourceGameObject == null || IsInvalid(sourceGameObject) ||
                                     destinationGameObject.isInvalid),
                _ => true
            };
        }

        private static bool IsInvalid(BaseVariableReference variable)
        {
            // Custom validation to allow null objects without warnings
            return (!variable.isConstant) && (variable.blackboard == null || string.IsNullOrEmpty(variable.key));
        }

        private enum Type
        {
            Transform, GameObject
        }
    }
}
