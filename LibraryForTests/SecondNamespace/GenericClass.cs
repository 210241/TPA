using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryForTests.SecondNamespace
{
    public class GenericClass<T> : ITestable
    {
        public T GenericMethod()
        {
            throw new NotImplementedException();
        }
    }
}
