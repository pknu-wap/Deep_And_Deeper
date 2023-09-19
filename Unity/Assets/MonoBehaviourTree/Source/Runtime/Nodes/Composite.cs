using System.Collections.Generic;

namespace MBT
{
    public abstract class Composite : Node, IParentNode, IChildrenNode
    {
        private static readonly System.Random rng = new System.Random();
        
        public bool random;

        public override void AddChild(Node node)
        {
            if (children.Contains(node)) return;
            // Remove parent in case there is one already
            if (node.parent != null) {
                node.parent.RemoveChild(node);
            }
            children.Add(node);
            node.parent = this;
        }

        public override void RemoveChild(Node node)
        {
            if (!children.Contains(node)) return;
            children.Remove(node);
            node.parent = null;
        }

        protected static void ShuffleList<T>(List<T> list)
        {  
            var n = list.Count;  
            while (n > 1) {
                n--;
                var k = rng.Next(n + 1);
                (list[k], list[n]) = (list[n], list[k]);
            }
        }
    }
}
