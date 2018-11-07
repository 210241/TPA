using System;
using System.Collections.Generic;
using System.Linq;
using ApplicationLogic.Model;
using DataTransfer.Interfaces;
using DataTransfer.Model;

namespace ApplicationLogic.Mapper
{
    public class NodeItemMapper : IMapper<AssemblyDataStorage, NodeItem>
    {
        public NodeItem Map(AssemblyDataStorage objectToMap)
        {
            if (objectToMap == null)
            {
                throw new ArgumentNullException($"{nameof(objectToMap)} argument is null.");
            }

            Dictionary<string, NodeItem> instances = new Dictionary<string, NodeItem>();
            List<Connection> connections = new List<Connection>();

            // assembly
            bool hasChildren = objectToMap.AssemblyData?.Namespaces.Any() == true;
            NodeItem assemblyItem = new NodeItem(objectToMap.AssemblyData.Id, hasChildren);
            instances.Add(assemblyItem.Name, assemblyItem);

            ProcessNamespaceItems(objectToMap, instances, connections, assemblyItem);

            ProcessMutlipleConnectionItems(objectToMap.MethodsDictionary, instances, connections, GetConnections, MapItem);
            ProcessMutlipleConnectionItems(objectToMap.TypesDictionary, instances, connections, GetConnections, MapItem);
            ProcessSingleConnectionItems(objectToMap.ParametersDictionary, instances, connections, GetConnection, MapItem);
            ProcessSingleConnectionItems(objectToMap.PropertiesDictionary, instances, connections, GetConnection, MapItem);

            // lets get fornicating
            foreach (var connection in connections)
            {
                instances[connection.Parent].Children.Add(instances[connection.Child]);
            }

            return assemblyItem;
        }

        private void ProcessNamespaceItems
        (
            AssemblyDataStorage objectToMap,
            Dictionary<string, NodeItem> instances,
            List<Connection> connections,
            NodeItem assemblyItem)
        {
            foreach (var namespaceData in objectToMap.NamespacesDictionary)
            {
                NodeItem item = MapItem(namespaceData.Value);
                foreach (var connection in GetConnections(namespaceData.Value))
                {
                    connections.Add(connection);
                }

                connections.Add(new Connection(assemblyItem.Name, item.Name));
                instances.Add(item.Name, item);
            }
        }

        private void ProcessSingleConnectionItems<T>
        (
            Dictionary<string, T> itemsDictionary,
            Dictionary<string, NodeItem> instances,
            List<Connection> connections,
            Func<T, Connection> connectionFunction,
            Func<T, NodeItem> mapFunction)
        {
            foreach (var dictionaryItem in itemsDictionary)
            {
                NodeItem item = mapFunction(dictionaryItem.Value);
                connections.Add(connectionFunction(dictionaryItem.Value));
                instances.Add(dictionaryItem.Key, item);
            }
        }

        private void ProcessMutlipleConnectionItems<T>
        (
            Dictionary<string, T> itemsDictionary,
            Dictionary<string, NodeItem> instances,
            List<Connection> connections,
            Func<T, IEnumerable<Connection>> connectionFunction,
            Func<T, NodeItem> mapFunction)
        {
            foreach (var dictionaryItem in itemsDictionary)
            {
                NodeItem item = mapFunction(dictionaryItem.Value);
                foreach (var connection in connectionFunction(dictionaryItem.Value))
                {
                    connections.Add(connection);
                }

                instances.Add(dictionaryItem.Key, item);
            }
        }

        private Connection GetConnection(PropertyData value)
        {
            return new Connection(value.Id, value.TypeMetadata.Id);
        }

        private NodeItem MapItem(PropertyData value)
        {
            return new NodeItem($"Property: {value.Name}", true);
        }

        private Connection GetConnection(ParameterData value)
        {
            return new Connection(value.Id, value.TypeMetadata.Id);
        }

        private NodeItem MapItem(ParameterData value)
        {
            return new NodeItem($"Parameter: {value.Name}", true);
        }

        private IEnumerable<Connection> GetConnections(NamespaceData value)
        {
            foreach (var item in value.Types)
            {
                yield return new Connection($"Namespace: {value.Id}", item.Id);
            }
        }

        private NodeItem MapItem(NamespaceData value)
        {
            return new NodeItem($"Namespace: {value.Id}", value.Types.Any());
        }

        private IEnumerable<Connection> GetConnections(TypeData value)
        {
            foreach (var item in value.Constructors)
            {
                yield return new Connection(value.Id, item.Id);
            }

            foreach (var item in value.Methods)
            {
                yield return new Connection(value.Id, item.Id);
            }

            foreach (var item in value.Properties)
            {
                yield return new Connection(value.Id, item.Id);
            }

            foreach (var item in value.GenericArguments)
            {
                yield return new Connection(value.Id, item.Id);
            }

            foreach (var item in value.ImplementedInterfaces)
            {
                yield return new Connection(value.Id, item.Id);
            }

            foreach (var item in value.NestedTypes)
            {
                yield return new Connection(value.Id, item.Id);
            }
        }
       
        private NodeItem MapItem(TypeData objectToMap)
        {
            return new NodeItem(
                $"{objectToMap.TypeKind.ToString().Replace("Type", string.Empty)}: {objectToMap.Name}",
                objectToMap.BaseType != null
                || objectToMap.DeclaringType != null
                || objectToMap.Constructors?.Any() == true
                || objectToMap.Methods?.Any() == true
                || objectToMap.GenericArguments?.Any() == true
                || objectToMap.ImplementedInterfaces?.Any() == true
                || objectToMap.NestedTypes?.Any() == true
                || objectToMap.Properties?.Any() == true);
        }

        private IEnumerable<Connection> GetConnections(MethodData parent)
        {
            foreach (var argument in parent.GenericArguments)
            {
                yield return new Connection(parent.Id, argument.Id);
            }

            foreach (var parameter in parent.Parameters)
            {
                yield return new Connection(parent.Id, parameter.Id);
            }

            if (parent.ReturnType != null) yield return new Connection(parent.Id, parent.ReturnType.Id);
        }

        private NodeItem MapItem(MethodData objectToMap)
        {
            bool hasChildren =
                objectToMap.GenericArguments.Any() ||
                objectToMap.Parameters.Any();

            return new NodeItem(
                $"{objectToMap.Modifiers.Item1} " +
                $"{objectToMap.ReturnType?.Name ?? "void"} " +
                $"{objectToMap.Name}",
                hasChildren);
        }
    }
}