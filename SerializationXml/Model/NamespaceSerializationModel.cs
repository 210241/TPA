﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Base.Model;

namespace SerializationXml.Model
{
    [DataContract(Name = "NamespaceSerializationModel", IsReference = true)]
    public class NamespaceSerializationModel
    {
        private NamespaceSerializationModel()
        {

        }

        public NamespaceSerializationModel(NamespaceBase namespaceBase)
        {
            this.Name = namespaceBase.Name;
            Types = namespaceBase.Types?.Select(t => TypeSerializationModel.GetOrAdd(t)).ToList();
        }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public List<TypeSerializationModel> Types { get; set; }
    }
}
