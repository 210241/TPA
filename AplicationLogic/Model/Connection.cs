namespace AplicationLogic.Model
{
    public class Connection
    {
        public Connection(string parent, string child)
        {
            Parent = parent;
            Child = child;
        }

        public string Parent { get; set; }

        public string Child { get; set; }
    }
}
