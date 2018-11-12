using ApplicationLogic.ViewModel;
using Logging;

namespace Console_UI
{
    internal class Program
    {
        internal static void Main()
        {
            Logger logger = new Logger();

            MainViewModel vm = new MainViewModel(
                logger,
                new ConsoleFilePathProvider()

            );

            MainView main = new MainView(vm);
            main.Display();
        }
    }
}
