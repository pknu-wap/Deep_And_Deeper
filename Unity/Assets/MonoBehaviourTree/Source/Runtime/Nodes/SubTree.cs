using UnityEngine;

namespace MBT
{
    [AddComponentMenu("")]
    [MBTNode(name = "SubTree", order = 250)]
    public class SubTree : Node, IChildrenNode
    {
        public MonoBehaviourTree tree;
        
        public override void AddChild(Node node)
        {
            return;
        }

        public override NodeResult Execute()
        {
            // Return failure when subtree is not defined
            if (tree == null) {
                return NodeResult.failure;
            }
            var root = tree.GetRoot();
            return root.status is Status.Success or Status.Failure ? NodeResult.From(root.status) : root.runningNodeResult;
        }

        public override void RemoveChild(Node node)
        {
            return;
        }

        public override bool IsValid()
        {
            return tree != null;
        }
    }
}
