using System;
using System.Collections.Generic;

namespace MBT
{
    public abstract class Decorator : Node, IParentNode, IChildrenNode
    {
        private Node[] stackState = Array.Empty<Node>();

        public override void AddChild(Node node)
        {
            // Allow only one children
            if (this.children.Count > 0)
            {
                var child = this.children[0];
                if (child == node) {
                    return;
                }
                child.parent.RemoveChild(child);
                this.children.Clear();
            }
            // Remove parent in case there is one already
            if (node.parent != null) {
                node.parent.RemoveChild(node);
            }
            this.children.Add(node);
            node.parent = this;
        }

        protected Node GetChild()
        {
            return children.Count > 0 ? children[0] : null;
        }

        public override void RemoveChild(Node node)
        {
            if (!children.Contains(node)) return;
            children.Remove(node);
            node.parent = null;
        }

        /// <summary>
        /// Copy and store current state of execution stack if it was not saved before.
        /// </summary>
        protected void ObtainTreeSnapshot()
        {
            // Copy stack only when this method is called for the first time
            if (stackState.Length == 0)
            {
                behaviourTree.GetStack(ref stackState);
            }
        }

        [Obsolete]
        protected void DisposeBTState()
        {
            stackState = Array.Empty<Node>();
        }

        internal IEnumerable<Node> GetStoredTreeSnapshot()
        {
            return stackState;
        }

        /// <summary>
        /// Helper method used to abort nodes in valid case
        /// </summary>
        /// <param name="abort">Abort type</param>
        protected void TryAbort(Abort abort)
        {
            switch (abort)
            {
                case Abort.Self:
                    if (status == Status.Running) {
                        behaviourTree.Interrupt(this);
                    }
                    break;
                case Abort.LowerPriority:
                    if (status is Status.Success or Status.Failure) {
                        behaviourTree.Interrupt(this);
                    }
                    break;
                case Abort.Both:
                    behaviourTree.Interrupt(this);
                    break;
            }
        }
    }
}
