﻿using System;

namespace MBT
{ 
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public class MBTNode : Attribute
    {
        public string name;
        public int order;
        public readonly string icon;

        public MBTNode(string name = null, int order = 1000, string icon = null)
        {
            this.name = name;
            this.order = order;
            this.icon = icon;
        }
    }
}
