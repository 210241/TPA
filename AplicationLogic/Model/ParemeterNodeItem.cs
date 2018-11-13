using System.Collections.ObjectModel;
using Reflection.Model;

namespace ApplicationLogic.Model
{
    public class ParameterNodeItem : NodeItem
    {
        public ParameterReader ParameterReader { get; set; }

        public ParameterNodeItem(ParameterReader parameterReader, ItemTypeEnum type)
            : base(parameterReader.Name, type)
        {
            ParameterReader = parameterReader;
        }

        protected override void BuildTreeView(ObservableCollection<NodeItem> children)
        {
            if (ParameterReader.Type != null)
            {
                children.Add(new TypeNodeItem(TypeReader.TypeDictionary[ParameterReader.Type.Name], ItemTypeEnum.Type));
            }
        }
    }
}
