using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using Base.Model;
using Reflection.LogicModel;

namespace Reflection.Model
{
    public class PropertyReader : PropertyBase
    {

        public PropertyReader(PropertyBase baseProperty)
        {
            Name = baseProperty.Name;
            Type = baseProperty.Type;
        }

        public PropertyReader(PropertyLogicReader baseProperty)
        {
            Name = baseProperty.Name;
            Type = TypeReader.GetOrAdd(baseProperty.Type);
        }

    }
}
