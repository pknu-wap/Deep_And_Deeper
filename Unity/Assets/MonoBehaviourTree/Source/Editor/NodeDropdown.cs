using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using System.Reflection;
using UnityEditor.IMGUI.Controls;
using MBT;

namespace MBTEditor
{
    public class ClassTypeDropdownItem : AdvancedDropdownItem
    {
        public readonly Type classType;
        public readonly int order;
        public readonly string path;

        public ClassTypeDropdownItem(string name, Type type = null, int order = 1000, string path = "") : base(name)
        {
            this.classType = type;
            this.order = order;
            this.path = path;
        }
    }

    public class NodeDropdown : AdvancedDropdown
    {
        private readonly Action<ClassTypeDropdownItem> Callback;
        
        public NodeDropdown(AdvancedDropdownState state, Action<ClassTypeDropdownItem> callback) : base(state)
        {
            this.Callback = callback;
            minimumSize = new Vector2(230,320);
        }

        protected override AdvancedDropdownItem BuildRoot()
        {
            var root = new ClassTypeDropdownItem("Nodes");

            // List for all found subclasses
            var results = new List<Type>();
            
            // Search all assemblies
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                // Find all subclasses of Node
                var enumerable = assembly.GetTypes()
                .Where(myType => myType.IsClass && !myType.IsAbstract && myType.IsSubclassOf(typeof(Node)));
                results.AddRange(enumerable);
            }

            // Keep track of all paths to correctly build tree later
            var nodePathsDictionary = new Dictionary<string, ClassTypeDropdownItem> { { "", root } };
            // Create list of items
            var items = new List<ClassTypeDropdownItem>();
            var index = 0;
            for (; index < results.Count; index++)
            {
                var type = results[index];
                if (!type.IsDefined(typeof(MBTNode), false)) continue;
                var nodeMeta = type.GetCustomAttribute<MBTNode>();
                string itemName;
                var nodePath = "";
                if (string.IsNullOrEmpty(nodeMeta.name))
                {
                    itemName = type.Name;
                }
                else
                {
                    var path = nodeMeta.name.Split('/');
                    itemName = path[^1];
                    nodePath = BuildPathIfNotExists(path, ref nodePathsDictionary);
                }

                var classTypeDropdownItem =
                    new ClassTypeDropdownItem(itemName, type, nodeMeta.order, nodePath);
                if (nodeMeta.icon != null)
                {
                    classTypeDropdownItem.icon = Resources.Load(nodeMeta.icon, typeof(Texture2D)) as Texture2D;
                }

                items.Add(classTypeDropdownItem);
            }

            // Sort items
            items.Sort((x, y) => {
                var result = x.order.CompareTo(y.order);
                return result != 0 ? result : string.Compare(x.name, y.name, StringComparison.Ordinal);
            });
            
            // Add all nodes to menu
            foreach (var t in items)
            {
                nodePathsDictionary[t.path].AddChild(t);
            }

            // Remove root to avoid infinite root folder loop
            nodePathsDictionary.Remove("");
            var parentNodes = nodePathsDictionary.Values.ToList();
            parentNodes.Sort((x, y) => string.Compare(x.name, y.name, StringComparison.Ordinal));

            // Add folders
            for (var i = 0; i < parentNodes.Count(); i++)
            {
                root.AddChild(parentNodes[i]);
            }

            return root;
        }

        protected override void ItemSelected(AdvancedDropdownItem item)
        {
            Callback(item as ClassTypeDropdownItem);
        }

        /// <summary>
        /// Creates nodes if path does not exists. Supports only single level folders.
        /// </summary>
        /// <param name="path">Path to build. Last element should be actual node name.</param>
        /// <param name="dictionary">Reference to dictionary to store references to items</param>
        /// <returns>Path to provided node in path</returns>
        private static string BuildPathIfNotExists(IReadOnlyList<string> path, ref Dictionary<string, ClassTypeDropdownItem> dictionary)
        {
            // IMPORTANT: This code supports only single level folders. Nodes can't be nested more than one level.
            if (path.Count != 2)
            {
                return "";
            }
            AdvancedDropdownItem root = dictionary[""];
            // // This code assumes the last element of path is actual name of node
            // string nodePath = String.Join("/", path, 0, path.Length-1);
            var nodePath = path[0];
            // Create path nodes if does not exists
            if (dictionary.ContainsKey(nodePath)) return nodePath;
            var node = new ClassTypeDropdownItem(nodePath);
            dictionary.Add(nodePath, node);
            return nodePath;
        }
    }
}
