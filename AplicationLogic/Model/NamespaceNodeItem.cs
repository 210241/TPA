using System.Collections.ObjectModel;
using ApplicationLogic.Interfaces;
using Reflection.Enums;
using Reflection.LogicModel;

namespace ApplicationLogic.Model
{
    public class NamespaceNodeItem : NodeItem
    {
        private readonly NamespaceLogicReader _namespaceLogicReader;
        private readonly ILogger _logger;

        public NamespaceNodeItem(NamespaceLogicReader namespaceLogicReader, ILogger logger)
            : base(namespaceLogicReader.Name, ItemTypeEnum.Namespace)
        {
            _namespaceLogicReader = namespaceLogicReader;
            _logger = logger;
        }

        protected override void BuildTreeView(ObservableCollection<NodeItem> children)
        {
            if (_namespaceLogicReader?.Types != null)
            {
                foreach (TypeLogicReader typeLogicReader in _namespaceLogicReader?.Types)
                {
                    ItemTypeEnum typeEnum = typeLogicReader.Type == TypeKind.ClassType ?
                        ItemTypeEnum.Class : typeLogicReader.Type == TypeKind.EnumType ?
                            ItemTypeEnum.Enum : typeLogicReader.Type == TypeKind.InterfaceType ?
                                ItemTypeEnum.Interface : ItemTypeEnum.Struct;

                    _logger.Trace($"Adding Type: [{typeEnum.ToString()}] {typeLogicReader.Name} implemented in Namespace: {_namespaceLogicReader.Name}");
                    ModelHelperMethods.CheckOrAdd(typeLogicReader);
                    children.Add(new TypeNodeItem(TypeLogicReader.TypeDictionary[typeLogicReader.Name], typeEnum, _logger));
                }
            }
        }
    }
}
