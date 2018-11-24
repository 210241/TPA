using System.Collections.ObjectModel;
using ApplicationLogic.Interfaces;
using Reflection.Model;

namespace ApplicationLogic.Model
{
    public class ParameterNodeItem : NodeItem
    {
        public ParameterReader ParameterReader { get; set; }
        private readonly ILogger _logger;

        public ParameterNodeItem(ParameterReader parameterReader, ItemTypeEnum type, ILogger logger)
            : base(parameterReader.Name, type)
        {
            _logger = logger;
            ParameterReader = parameterReader;
        }

        protected override void BuildTreeView(ObservableCollection<NodeItem> children)
        {
            if (ParameterReader.Type != null)
            {
                ModelHelperMethods.CheckOrAdd(ParameterReader.Type);
                children.Add(new TypeNodeItem(TypeReader.TypeDictionary[ParameterReader.Type.Name], ItemTypeEnum.Type, _logger));
            }
        }
    }
}
