using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using Base.Model;
using Reflection.LogicModel;


namespace Reflection.Model
{
    public class AssemblyReader : AssemblyBase
    {
        public AssemblyReader(AssemblyLogicReader assemblyLogic)
        {
            this.Name = assemblyLogic.Name;
            this.Namespaces = new List<NamespaceBase>();
            foreach (var baseElem in assemblyLogic.NamespaceLogicReader)
            {
                Namespaces.Add(new NamespaceReader(baseElem));
            }
        }

        public AssemblyReader(AssemblyBase assemblyLogic)
        {
            this.Name = assemblyLogic.Name;
            this.Namespaces = new List<NamespaceBase>();
            foreach (var baseElem in base.Namespaces)
            {
                Namespaces.Add(new NamespaceReader(baseElem));
            }
           
        }
    }
}
