using System.Collections.ObjectModel;
using ApplicationLogic.Interfaces;
using Reflection.LogicModel;

namespace ApplicationLogic.Model
{
    public class ParameterNodeItem : NodeItem
    {
        public ParameterLogicReader ParameterLogicReader { get; set; }
        private readonly ILogger _logger;

        public ParameterNodeItem(ParameterLogicReader parameterLogicReader, ItemTypeEnum type, ILogger logger)
            : base(parameterLogicReader.Name, type)
        {
            _logger = logger;
            ParameterLogicReader = parameterLogicReader;
        }

        protected override void BuildTreeView(ObservableCollection<NodeItem> children)
        {
            if (ParameterLogicReader.Type != null)
            {
                ModelHelperMethods.CheckOrAdd(ParameterLogicReader.Type);
                children.Add(new TypeNodeItem(TypeLogicReader.TypeDictionary[ParameterLogicReader.Type.Name], ItemTypeEnum.Type, _logger));
            }
        }
    }
}
