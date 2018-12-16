using Reflection.Enums;

namespace Reflection.LogicModel
{
    public class Modifiers
    {
        public AccessLevel AccessLevel{ get; set; }

        public AbstractEnum AbstractEnum{ get; set; }
        
        public StaticEnum StaticEnum { get; set; }

        public VirtualEnum VirtualEnum { get; set; }

    }
}
