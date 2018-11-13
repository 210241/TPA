using System.Collections.ObjectModel;
using Reflection.Model;

namespace ApplicationLogic.Model
{
    public class PropertyNodeItem : NodeItem
    {
        public PropertyReader PropertyReader { get; set; }

        public PropertyNodeItem(PropertyReader type, string name)
            : base(name, ItemTypeEnum.Property)
        {
            PropertyReader = type;
        }

        protected override void BuildTreeView(ObservableCollection<NodeItem> children)
        {
            if (PropertyReader.Type != null)
            {
                children.Add(new TypeNodeItem(TypeReader.TypeDictionary[PropertyReader.Type.Name], ItemTypeEnum.Type));
            }
        }
    }
}
