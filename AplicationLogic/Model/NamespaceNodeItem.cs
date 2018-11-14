using System.Collections.ObjectModel;
using ApplicationLogic.Interfaces;
using Reflection.Enums;
using Reflection.Model;

namespace ApplicationLogic.Model
{
    public class NamespaceNodeItem : NodeItem
    {
        private readonly NamespaceReader _namespaceReader;
        private readonly ILogger _logger;

        public NamespaceNodeItem(NamespaceReader namespaceReader, ILogger logger)
            : base(namespaceReader.Name, ItemTypeEnum.Namespace)
        {
            _namespaceReader = namespaceReader;
            _logger = logger;
        }

        protected override void BuildTreeView(ObservableCollection<NodeItem> children)
        {
            if (_namespaceReader?.Types != null)
            {
                foreach (TypeReader typeReader in _namespaceReader?.Types)
                {
                    ItemTypeEnum typeEnum = typeReader.Type == TypeKind.ClassType ?
                        ItemTypeEnum.Class : typeReader.Type == TypeKind.EnumType ?
                            ItemTypeEnum.Enum : typeReader.Type == TypeKind.InterfaceType ?
                                ItemTypeEnum.Interface : ItemTypeEnum.Struct;

                    _logger.Trace($"Adding Type: [{typeEnum.ToString()}] {typeReader.Name} implemented in Namespace: {_namespaceReader.Name}");
                    children.Add(new TypeNodeItem(TypeReader.TypeDictionary[typeReader.Name], typeEnum, _logger));
                }
            }
        }
    }
}
