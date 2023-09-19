using UnityEngine;

namespace MBT
{
    [AddComponentMenu("")]
    [MBTNode(name = "Root", order = 200)]
    public class Root : Node, IParentNode
    {
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

        public override NodeResult Execute()
        {
            if (children.Count != 1) return NodeResult.failure;
            var child = children[0];
            return child.status is Status.Success or Status.Failure ? NodeResult.From(child.status) : child.runningNodeResult;
        }

        public override void RemoveChild(Node node)
        {
            if (!children.Contains(node)) return;
            children.Remove(node);
            node.parent = null;
        }
    }
}
