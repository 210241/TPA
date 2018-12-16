using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Base.Model;
using Reflection.LogicModel;

namespace Reflection.Model
{
    public class NamespaceReader : NamespaceBase
    {

        public NamespaceReader(NamespaceLogicReader namespaceBase)
        {
            this.Name = namespaceBase.Name;
            this.Types = new List<TypeBase>();
            foreach (TypeLogicReader baseElem in namespaceBase.Types)
            {
                Types.Add(TypeReader.GetOrAdd(baseElem));
            }
        }
        public NamespaceReader(NamespaceBase namespaceBase)
        {
            this.Name = namespaceBase.Name;
            this.Types = new List<TypeBase>();
            foreach (TypeBase baseElem in namespaceBase.Types)
            {
                Types.Add(TypeReader.GetOrAdd(baseElem));
            }
        }

    }
}
