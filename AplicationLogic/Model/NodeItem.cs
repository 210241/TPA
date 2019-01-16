using System.Collections.ObjectModel;

namespace ApplicationLogic.Model
{
    public abstract class NodeItem
    {
        private bool _wasBuilt;
        private bool _isExpanded;

        public string Name { get; set; }
        public string Accessibility { get; set; }

        public ItemTypeEnum ItemType { get; set; }

        public ObservableCollection<NodeItem> Children { get; }

        protected NodeItem(string name, ItemTypeEnum itemType)
        {
            Children = new ObservableCollection<NodeItem>() { null };
            _wasBuilt = false;
            Name = name;
            ItemType = itemType;
        }

        public bool IsExpanded
        {
            get => _isExpanded;

            set
            {
                _isExpanded = value;
                if (_wasBuilt)
                    return;
                Children.Clear();
                BuildTreeView(Children);
                _wasBuilt = true;
            }
        }

        protected abstract void BuildTreeView(ObservableCollection<NodeItem> children);
    }
}
