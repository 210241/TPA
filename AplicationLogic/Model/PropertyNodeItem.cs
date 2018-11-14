using System.Collections.ObjectModel;
using ApplicationLogic.Interfaces;
using Reflection.Model;

namespace ApplicationLogic.Model
{
    public class PropertyNodeItem : NodeItem
    {
        public PropertyReader PropertyReader { get; set; }
        private readonly ILogger _logger;

        public PropertyNodeItem(PropertyReader type, string name, ILogger logger)
            : base(name, ItemTypeEnum.Property)
        {
            _logger = logger;
            PropertyReader = type;
        }

        protected override void BuildTreeView(ObservableCollection<NodeItem> children)
        {
            if (PropertyReader.Type != null)
            {
                children.Add(new TypeNodeItem(TypeReader.TypeDictionary[PropertyReader.Type.Name], ItemTypeEnum.Type, _logger));
            }
        }
    }
}
