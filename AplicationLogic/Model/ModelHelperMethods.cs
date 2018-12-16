using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Reflection.LogicModel;

namespace ApplicationLogic.Model
{
    public static class ModelHelperMethods
    {
        public static void CheckOrAdd(TypeLogicReader type)
        {
            if (!TypeLogicReader.TypeDictionary.ContainsKey(type.Name))
            {
                TypeLogicReader.TypeDictionary.Add(type.Name, type);
            }
        }
    }
}
