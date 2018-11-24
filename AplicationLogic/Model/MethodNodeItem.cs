using System.Collections.ObjectModel;
using ApplicationLogic.Interfaces;
using Reflection.Enums;
using Reflection.Model;

namespace ApplicationLogic.Model
{
    public class MethodNodeItem : NodeItem
    {
        private readonly MethodReader _methodReader;
        private readonly ILogger _logger;

        public MethodNodeItem(MethodReader methodReader, ItemTypeEnum type, ILogger logger)
            : base(GetModifiers(methodReader) + methodReader.Name, type)
        {
            _logger = logger;
            _methodReader = methodReader;
        }

        public static string GetModifiers(MethodReader methodReader)
        {
            string type = null;
            type += methodReader.AccessLevel.ToString().ToLower() + " ";
            type += methodReader.AbstractEnum == AbstractEnum.Abstract ? AbstractEnum.Abstract.ToString().ToLower() + " " : string.Empty;
            type += methodReader.StaticEnum == StaticEnum.Static ? StaticEnum.Static.ToString().ToLower() + " " : string.Empty;
            type += methodReader.VirtualEnum == VirtualEnum.Virtual ? VirtualEnum.Virtual.ToString().ToLower() + " " : string.Empty;
            return type;
        }

        protected override void BuildTreeView(ObservableCollection<NodeItem> children)
        {
            if (_methodReader.GenericArguments != null)
            {
                foreach (TypeReader genericArgument in _methodReader.GenericArguments)
                {
                    _logger.Trace($"Adding Type: [{ItemTypeEnum.GenericArgument.ToString()}] {genericArgument.Name} implemented in Method: {_methodReader.Name}");
                    ModelHelperMethods.CheckOrAdd(genericArgument);
                    children.Add(new TypeNodeItem(TypeReader.TypeDictionary[genericArgument.Name], ItemTypeEnum.GenericArgument, _logger));
                }
            }

            if (_methodReader.Parameters != null)
            {
                foreach (ParameterReader parameter in _methodReader.Parameters)
                {
                    _logger.Trace($"Adding Parameter: [{ItemTypeEnum.Parameter.ToString()}] {parameter.Name} implemented in Method: {_methodReader.Name}");
                    children.Add(new ParameterNodeItem(parameter, ItemTypeEnum.Parameter, _logger));
                }
            }

            if (_methodReader.ReturnType != null)
            {
                _logger.Trace($"Adding Type: [{ItemTypeEnum.ReturnType.ToString()}] {_methodReader.ReturnType.Name} implemented in Method: {_methodReader.Name}");
                ModelHelperMethods.CheckOrAdd(_methodReader.ReturnType);
                children.Add(new TypeNodeItem(TypeReader.TypeDictionary[_methodReader.ReturnType.Name], ItemTypeEnum.ReturnType, _logger));
            }
        }
    }
}
