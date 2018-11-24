using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Reflection.Enums;

namespace Reflection.Model
{
    public class Modifiers
    {
        public AccessLevel AccessLevel{ get; set; }

        public AbstractEnum AbstractEnum{ get; set; }
        
        public StaticEnum StaticEnum { get; set; }

        public VirtualEnum VirtualEnum { get; set; }

    }
}
