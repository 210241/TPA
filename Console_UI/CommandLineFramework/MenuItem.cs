using System.Windows.Input;

namespace Console_UI.CommandLineFramework
{
    public class MenuItem
    {
        public string Header { get; set; }

        public ICommand Command { get; set; }
    }
}