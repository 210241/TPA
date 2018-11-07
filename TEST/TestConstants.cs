using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationLogic.Mapper;
using DataTransfer.Model;
using DataTransfer.Model.Enums;

namespace TEST
{
    class TestConstants
    {
        public static NodeItemMapper Context;
        public static AssemblyData AssemblyData;
        public static AssemblyDataStorage Storage;

        public static string AssemblyName = "BestAssembly";
        public static string NamespaceName = "NewNamespace";
        public static string TypeName = "SuperType";
        public static string SecondTypeName = "NotSoSuperType(2)";
        public static string PropertyName = "MegaPropertyTest";
        public static string MethodName = "BestMethodInTheWorld";
        public static AccessLevel TypeAccessLevel = AccessLevel.IsPublic;
        public static AccessLevel MethodAccessLevel = AccessLevel.IsPublic;
    }
}
