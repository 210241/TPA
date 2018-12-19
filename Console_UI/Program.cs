using ApplicationLogic.Base;
using ApplicationLogic.Interfaces;
using ApplicationLogic.ViewModel;

namespace Console_UI
{
    internal class Program
    {
        internal static void Main()
        {

            IFilePathProvider pathloader = new ConsoleFilePathProvider();
            
            MainViewModel vm = Compose.ComposeViewModel(pathloader, new ErrorInfo());

            MainView main = new MainView(vm);
            main.Display();
        }
    }
}
