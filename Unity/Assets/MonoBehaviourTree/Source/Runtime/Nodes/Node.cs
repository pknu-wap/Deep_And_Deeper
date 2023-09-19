using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MBT
{ 
    [RequireComponent(typeof(MonoBehaviourTree))]
    public abstract class Node : MonoBehaviour
    {
        private const float NODE_DEFAULT_WIDTH = 160f;

        public string title;
        [HideInInspector]
        public Rect rect = new Rect(0, 0, NODE_DEFAULT_WIDTH, 50);
        [HideInInspector]
        public Node parent;
        [HideInInspector]
        public List<Node> children = new List<Node>();
        [NonSerialized]
        public Status status = Status.Ready;
        [HideInInspector]
        public MonoBehaviourTree behaviourTree;
        // [HideInInspector]
        public NodeResult runningNodeResult { get; internal set;}
        [HideInInspector]
        public int runtimePriority;
        [HideInInspector]
        public bool breakpoint;

        public bool selected { get; set; }

        /// <summary>
        /// Time of last tick retrieved from Time.time
        /// </summary>
        public float LastTick => behaviourTree.LastTick;
        /// <summary>
        /// The interval in seconds from the last tick of behaviour tree.
        /// </summary>
        protected float DeltaTime => Time.time - behaviourTree.LastTick;

        public virtual void OnAllowInterrupt() {}
        public virtual void OnEnter() {}
        public abstract NodeResult Execute();
        public virtual void OnExit() {}
        public virtual void OnDisallowInterrupt() {}

        public virtual void OnBehaviourTreeAbort() {}

        public abstract void AddChild(Node node);
        public abstract void RemoveChild(Node node);

        public virtual Node GetParent()
        {
            return parent;
        }

        public virtual List<Node> GetChildren()
        {
            return children;
        }

        public bool IsDescendantOf(Node node)
        {
            if (parent == null) {
                return false;
            }

            return parent == node || parent.IsDescendantOf(node);
        }

        public List<Node> GetAllSuccessors()
        {
            var result = new List<Node>();
            foreach (var t in children)
            {
                result.Add(t);
                result.AddRange(t.GetAllSuccessors());
            }
            return result;
        }

        public void SortChildren()
        {
            children.Sort((c, d) => c.rect.x.CompareTo(d.rect.x));
        }

        /// <summary>
        /// Check if node setup is valid
        /// </summary>
        /// <returns>Returns true if node is configured correctly</returns>
        public virtual bool IsValid()
        {
            #if UNITY_EDITOR
            var propertyInfos = GetType().GetFields();
            return (from t in propertyInfos where t.FieldType.IsSubclassOf(typeof(BaseVariableReference)) select t.GetValue(this) as BaseVariableReference).All(varReference => varReference is not
            {
                isInvalid: true
            });
#endif
        }
    }

    public enum Status
    {
        Success, Failure, Running, Ready
    }

    public enum Abort
    {
        None, Self, LowerPriority, Both
    }

    public class NodeResult
    {
        public Status status {get; private set;}
        public Node child {get; private set;}

        public NodeResult(Status status, Node child = null)
        {
            this.status = status;
            this.child = child;
        }

        public static NodeResult From(Status s)
        {
            return s switch
            {
                Status.Success => success,
                Status.Failure => failure,
                _ => running
            };
        }

        public static readonly NodeResult success = new NodeResult(Status.Success);
        public static readonly NodeResult failure = new NodeResult(Status.Failure);
        public static readonly NodeResult running = new NodeResult(Status.Running);
    }

    public interface IChildrenNode{
        // void SetParent(Node node);
    }

    public interface IParentNode{
        // void AddChild(Node node);
    }
}
