﻿namespace MBT
{
    public abstract class Leaf : Node, IChildrenNode
    {
        public sealed override void AddChild(Node node)
        {
            return;
        }

        public sealed override void RemoveChild(Node node)
        {
            return;
        }
    }
}
