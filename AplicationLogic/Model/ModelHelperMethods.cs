using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Reflection.Model;

namespace ApplicationLogic.Model
{
    public static class ModelHelperMethods
    {
        public static void CheckOrAdd(TypeReader type)
        {
            if (!TypeReader.TypeDictionary.ContainsKey(type.Name))
            {
                TypeReader.TypeDictionary.Add(type.Name, type);
            }
        }
    }
}
