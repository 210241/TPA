using System.Collections.Generic;

namespace ApplicationLogic.Model
{
    public class NodeItem
    {
        public string Name { get; set; }

        public List<NodeItem> Children { get; }

        public bool IsExpendable { get; set; }

        public NodeItem(string name, bool hasChildren)
        {
            Name = name;
            Children = new List<NodeItem>();
            IsExpendable = hasChildren;
        }
    }
}
