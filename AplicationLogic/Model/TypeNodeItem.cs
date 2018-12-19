using System.Collections.ObjectModel;
using ApplicationLogic.Interfaces;
using Reflection.Enums;
using Reflection.LogicModel;

namespace ApplicationLogic.Model
{
    public class TypeNodeItem : NodeItem
    {
        private readonly TypeLogicReader _typeLogicReader;
        private readonly ILogger _logger;

        public TypeNodeItem(TypeLogicReader typeLogicReader, ItemTypeEnum type, ILogger logger)
            : base(GetModifiers(typeLogicReader) + typeLogicReader.Name, type)
        {
            _typeLogicReader = typeLogicReader;
            _logger = logger;
        }

        public static string GetModifiers(TypeLogicReader model)
        {
                string type = null;
                type += model.AccessLevel == AccessLevel.Default ? string.Empty : model.AccessLevel.ToString().ToLower() + " ";
                type += model.SealedEnum == SealedEnum.Sealed ? SealedEnum.Sealed.ToString().ToLower() + " " : string.Empty;
                type += model.AbstractEnum == AbstractEnum.Abstract ? AbstractEnum.Abstract.ToString().ToLower() + " " : string.Empty;
                type += model.StaticEnum == StaticEnum.Static ? StaticEnum.Static.ToString().ToLower() + " " : string.Empty;
                return type;
        }

        protected override void BuildTreeView(ObservableCollection<NodeItem> children)
        {
            if (_typeLogicReader.BaseType != null)
            {
                _logger.Trace($"Adding BaseType: [{ItemTypeEnum.BaseType.ToString()}] {_typeLogicReader.BaseType.Name} implemented in Type: {_typeLogicReader.Name}");
                ModelHelperMethods.CheckOrAdd(_typeLogicReader.BaseType);
                children.Add(new TypeNodeItem(TypeLogicReader.TypeDictionary[_typeLogicReader.BaseType.Name], ItemTypeEnum.BaseType, _logger));
            }

            if (_typeLogicReader.DeclaringType != null)
            {
                _logger.Trace($"Adding DeclaringType: [{ItemTypeEnum.Type.ToString()}] {_typeLogicReader.DeclaringType.Name} implemented in Type: {_typeLogicReader.Name}");
                ModelHelperMethods.CheckOrAdd(_typeLogicReader.DeclaringType);
                children.Add(new TypeNodeItem(TypeLogicReader.TypeDictionary[_typeLogicReader.DeclaringType.Name], ItemTypeEnum.Type, _logger));
            }

            if (_typeLogicReader.Properties != null)
            {
                foreach (PropertyLogicReader propertyLogicReader in _typeLogicReader.Properties)
                {
                    _logger.Trace($"Adding Property: [{GetModifiers(propertyLogicReader.Type) + propertyLogicReader.Type.Name}] {propertyLogicReader.Name} implemented in Type: {_typeLogicReader.Name}");
                    children.Add(new PropertyNodeItem(propertyLogicReader, GetModifiers(propertyLogicReader.Type) + propertyLogicReader.Type.Name + " " + propertyLogicReader.Name, _logger));
                }
            }

            if (_typeLogicReader.Fields != null)
            {
                foreach (ParameterLogicReader parameterLogicReader in _typeLogicReader.Fields)
                {
                    _logger.Trace($"Adding Parameter: [{ItemTypeEnum.Field.ToString()}] {parameterLogicReader.Name} implemented in Type: {_typeLogicReader.Name}");
                    children.Add(new ParameterNodeItem(parameterLogicReader, ItemTypeEnum.Field, _logger));
                }
            }

            if (_typeLogicReader.GenericArguments != null)
            {
                foreach (TypeLogicReader typeLogicReader in _typeLogicReader.GenericArguments)
                {
                    _logger.Trace($"Adding Type: [{ItemTypeEnum.GenericArgument.ToString()}] {typeLogicReader.Name} implemented in Type: {_typeLogicReader.Name}");
                    ModelHelperMethods.CheckOrAdd(typeLogicReader);
                    children.Add(new TypeNodeItem(TypeLogicReader.TypeDictionary[typeLogicReader.Name], ItemTypeEnum.GenericArgument, _logger));
                }
            }

            if (_typeLogicReader.ImplementedInterfaces != null)
            {
                foreach (TypeLogicReader typeLogicReader in _typeLogicReader.ImplementedInterfaces)
                {
                    _logger.Trace($"Adding Type: [{ItemTypeEnum.ImplementedInterface.ToString()}] {typeLogicReader.Name} implemented in Type: {_typeLogicReader.Name}");
                    ModelHelperMethods.CheckOrAdd(typeLogicReader);
                    children.Add(new TypeNodeItem(TypeLogicReader.TypeDictionary[typeLogicReader.Name], ItemTypeEnum.ImplementedInterface, _logger));
                }
            }

            if (_typeLogicReader.NestedTypes != null)
            {
                foreach (TypeLogicReader typeLogicReader in _typeLogicReader.NestedTypes)
                {
                    ItemTypeEnum type = typeLogicReader.Type == TypeKind.ClassType ? ItemTypeEnum.NestedClass :
                        typeLogicReader.Type == TypeKind.StructType ? ItemTypeEnum.NestedStructure :
                        typeLogicReader.Type == TypeKind.EnumType ? ItemTypeEnum.NestedEnum : ItemTypeEnum.NestedType;

                    _logger.Trace($"Adding Type: [{type.ToString()}] {typeLogicReader.Name} implemented in Type: {_typeLogicReader.Name}");
                    ModelHelperMethods.CheckOrAdd(typeLogicReader);
                    children.Add(new TypeNodeItem(TypeLogicReader.TypeDictionary[typeLogicReader.Name], type, _logger));
                }
            }

            if (_typeLogicReader.Methods != null)
            {
                foreach (MethodLogicReader methodLogicReader in _typeLogicReader.Methods)
                {
                    ItemTypeEnum type = methodLogicReader.Extension ? ItemTypeEnum.ExtensionMethod : ItemTypeEnum.Method;
                    _logger.Trace($"Adding Method: [{type.ToString()}] {methodLogicReader.Name} implemented in Type: {_typeLogicReader.Name}");
                    children.Add(new MethodNodeItem(methodLogicReader, type, _logger));
                }
            }

            if (_typeLogicReader.Constructors != null)
            {
                foreach (MethodLogicReader methodLogicReader in _typeLogicReader.Constructors)
                {
                    _logger.Trace($"Adding Method: [{ItemTypeEnum.Constructor.ToString()}] {methodLogicReader.Name} implemented in Type: {_typeLogicReader.Name}");
                    children.Add(new MethodNodeItem(methodLogicReader, ItemTypeEnum.Constructor, _logger));
                }
            }
        }
    }
}
