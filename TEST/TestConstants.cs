using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AplicationLogic.Mapper;
using DataTransfer.Model;
using DataTransfer.Model.Enums;

namespace TEST
{
    class TestConstants
    {
        public static NodeItemMapper Context;
        public static AssemblyData AssemblyData;
        public static AssemblyDataStorage Storage;

        public static string AssemblyName = "TestAssembly";
        public static string NamespaceName = "TestNamespace";
        public static string TypeName = "TestType";
        public static string SecondTypeName = "2TestType";
        public static string PropertyName = "TestProperty";
        public static string MethodName = "TestMethod";
        public static AccessLevel TypeAccessLevel = AccessLevel.IsPublic;
        public static AccessLevel MethodAccessLevel = AccessLevel.IsPublic;
    }
}
