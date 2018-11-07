using AplicationLogic.Mapper;
using AplicationLogic.ViewModel;
using Logging;

namespace Console_UI
{
    internal class Program
    {
        internal static void Main()
        {
            Logger logger = new Logger();

            MainViewModel vm = new MainViewModel(
                new ConsoleFilePathProvider(),
                logger,
                new Reflection.Reflection(logger), 
                new NodeItemMapper()
            );

            MainView main = new MainView(vm);
            main.Display();
        }
    }
}
