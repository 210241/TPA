using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryForTests.SecondNamespace
{
    public class Car : Vehicle, ITestable
    {
        public override void TypeOfEngine()
        {
            throw new NotImplementedException();
        }
    }
}
