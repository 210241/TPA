using System;

namespace LibraryForTests.FirstNamespace
{
    public class AClass : IComparable
    {
        public int int1;
        public int int2;
        public string string1;
        public string string2;

        public BClass PropertyBClass { get; set; }
        public int CompareTo(object obj)
        {
            throw new NotImplementedException();
        }
    }
}
