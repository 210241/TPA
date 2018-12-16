using System.Collections.ObjectModel;
using ApplicationLogic.Interfaces;
using Base.Enums;
using Reflection.LogicModel;

namespace ApplicationLogic.Model
{
    public class MethodNodeItem : NodeItem
    {
        private readonly MethodLogicReader _methodLogicReader;
        private readonly ILogger _logger;

        public MethodNodeItem(MethodLogicReader methodLogicReader, ItemTypeEnum type, ILogger logger)
            : base(GetModifiers(methodLogicReader) + methodLogicReader.Name, type)
        {
            _logger = logger;
            _methodLogicReader = methodLogicReader;
        }

        public static string GetModifiers(MethodLogicReader methodLogicReader)
        {
            string type = null;
            type += methodLogicReader.AccessLevel.ToString().ToLower() + " ";
            type += methodLogicReader.AbstractEnum == AbstractEnum.Abstract ? AbstractEnum.Abstract.ToString().ToLower() + " " : string.Empty;
            type += methodLogicReader.StaticEnum == StaticEnum.Static ? StaticEnum.Static.ToString().ToLower() + " " : string.Empty;
            type += methodLogicReader.VirtualEnum == VirtualEnum.Virtual ? VirtualEnum.Virtual.ToString().ToLower() + " " : string.Empty;
            return type;
        }

        protected override void BuildTreeView(ObservableCollection<NodeItem> children)
        {
            if (_methodLogicReader.GenericArguments != null)
            {
                foreach (TypeLogicReader genericArgument in _methodLogicReader.GenericArguments)
                {
                    _logger.Trace($"Adding Type: [{ItemTypeEnum.GenericArgument.ToString()}] {genericArgument.Name} implemented in Method: {_methodLogicReader.Name}");
                    ModelHelperMethods.CheckOrAdd(genericArgument);
                    children.Add(new TypeNodeItem(TypeLogicReader.TypeDictionary[genericArgument.Name], ItemTypeEnum.GenericArgument, _logger));
                }
            }

            if (_methodLogicReader.Parameters != null)
            {
                foreach (ParameterLogicReader parameter in _methodLogicReader.Parameters)
                {
                    _logger.Trace($"Adding Parameter: [{ItemTypeEnum.Parameter.ToString()}] {parameter.Name} implemented in Method: {_methodLogicReader.Name}");
                    children.Add(new ParameterNodeItem(parameter, ItemTypeEnum.Parameter, _logger));
                }
            }

            if (_methodLogicReader.ReturnType != null)
            {
                _logger.Trace($"Adding Type: [{ItemTypeEnum.ReturnType.ToString()}] {_methodLogicReader.ReturnType.Name} implemented in Method: {_methodLogicReader.Name}");
                ModelHelperMethods.CheckOrAdd(_methodLogicReader.ReturnType);
                children.Add(new TypeNodeItem(TypeLogicReader.TypeDictionary[_methodLogicReader.ReturnType.Name], ItemTypeEnum.ReturnType, _logger));
            }
        }
    }
}
