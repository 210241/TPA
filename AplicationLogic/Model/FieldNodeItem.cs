using System.Collections.ObjectModel;
using ApplicationLogic.Interfaces;
using Reflection.LogicModel;

namespace ApplicationLogic.Model
{
    public class FieldNodeItem : NodeItem
    {
        public FieldLogicReader FieldLogicReader { get; set; }
        private readonly ILogger _logger;

        public FieldNodeItem(FieldLogicReader fieldLogicReader, ItemTypeEnum type, ILogger logger)
            : base(fieldLogicReader.Name, type)
        {
            _logger = logger;
            FieldLogicReader = fieldLogicReader;
        }

        protected override void BuildTreeView(ObservableCollection<NodeItem> children)
        {
            if (FieldLogicReader.Type != null)
            {
                ModelHelperMethods.CheckOrAdd(FieldLogicReader.Type);
                children.Add(new TypeNodeItem(TypeLogicReader.TypeDictionary[FieldLogicReader.Type.Name], ItemTypeEnum.Type, _logger));
            }
        }
    }
}