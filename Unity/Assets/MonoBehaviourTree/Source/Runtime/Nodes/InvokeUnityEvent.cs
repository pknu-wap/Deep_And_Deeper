using UnityEngine;
using UnityEngine.Events;

namespace MBT
{
    [AddComponentMenu("")]
    [MBTNode(name = "Tasks/Invoke Unity Event")]
    public class InvokeUnityEvent : Leaf
    {
        public EventType type;

        public TransformReference transformReference = new TransformReference();
        public GameObjectReference gameObjectReference = new GameObjectReference();
        public FloatReference floatReference = new FloatReference();
        public IntReference intReference = new IntReference();
        public BoolReference boolReference = new BoolReference();
        public StringReference stringReference = new StringReference();
        public Vector3Reference vector3Reference = new Vector3Reference();
        public Vector2Reference vector2Reference = new Vector2Reference();

        public TransformEvent  transformEvent;
        public GameObjectEvent gameObjectEvent;
        public FloatEvent floatEvent;
        public IntEvent intEvent;
        public BoolEvent boolEvent;
        public StringEvent stringEvent;
        public Vector3Event vector3Event;
        public Vector2Event vector2Event;

        public override NodeResult Execute()
        {
            switch (type)
            {
                case EventType.Transform: transformEvent.Invoke(transformReference.Value);
                    break;
                case EventType.Float: floatEvent.Invoke(floatReference.Value);
                    break;
                case EventType.Bool: boolEvent.Invoke(boolReference.Value);
                    break;
                case EventType.String: stringEvent.Invoke(stringReference.Value);
                    break;
                case EventType.Vector3: vector3Event.Invoke(vector3Reference.Value);
                    break;
                case EventType.Vector2: vector2Event.Invoke(vector2Reference.Value);
                    break;
                case EventType.Int: intEvent.Invoke(intReference.Value);
                    break;
                case EventType.GameObject: gameObjectEvent.Invoke(gameObjectReference.Value);
                    break;
            }
            return NodeResult.success;
        }

        public override bool IsValid()
        {
            return type switch
            {
                EventType.Transform => !transformReference.isInvalid,
                EventType.Float => !floatReference.isInvalid,
                EventType.Bool => !boolReference.isInvalid,
                EventType.String => !stringReference.isInvalid,
                EventType.Vector3 => !vector3Reference.isInvalid,
                EventType.Vector2 => !vector2Reference.isInvalid,
                EventType.Int => !intReference.isInvalid,
                EventType.GameObject => !gameObjectReference.isInvalid,
                _ => true
            };
        }

        public enum EventType
        {
            Transform, GameObject, Float, Int, Bool, String, Vector3, Vector2
        }

        [System.Serializable] public class TransformEvent : UnityEvent<Transform>{}
        [System.Serializable] public class GameObjectEvent : UnityEvent<GameObject>{}
        [System.Serializable] public class FloatEvent : UnityEvent<float>{}
        [System.Serializable] public class IntEvent : UnityEvent<int>{}
        [System.Serializable] public class BoolEvent : UnityEvent<bool>{}
        [System.Serializable] public class StringEvent : UnityEvent<string>{}
        [System.Serializable] public class Vector3Event : UnityEvent<Vector3>{}
        [System.Serializable] public class Vector2Event : UnityEvent<Vector2>{}
    }
}
