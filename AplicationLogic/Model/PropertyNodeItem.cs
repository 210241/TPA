using System.Collections.ObjectModel;
using ApplicationLogic.Interfaces;
using Reflection.LogicModel;

namespace ApplicationLogic.Model
{
    public class PropertyNodeItem : NodeItem
    {
        public PropertyLogicReader PropertyLogicReader { get; set; }
        private readonly ILogger _logger;

        public PropertyNodeItem(PropertyLogicReader type, string name, ILogger logger)
            : base(name, ItemTypeEnum.Property)
        {
            _logger = logger;
            PropertyLogicReader = type;
        }

        protected override void BuildTreeView(ObservableCollection<NodeItem> children)
        {
            if (PropertyLogicReader.Type != null)
            {
                ModelHelperMethods.CheckOrAdd(PropertyLogicReader.Type);
                children.Add(new TypeNodeItem(TypeLogicReader.TypeDictionary[PropertyLogicReader.Type.Name], ItemTypeEnum.Type, _logger));
            }
        }
    }
}
