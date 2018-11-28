using System;
using ApplicationLogic.Base;
using ApplicationLogic.ViewModel;
using Console_UI.CommandLineFramework;

namespace Console_UI
{
    public class MainView
    {
        public Menu Menu { get; } = new Menu();

        public ConsoleTreeView ConsoleTreeView { get; } = new ConsoleTreeView();

        public MainView(MainViewModel vm)
        {
            MainViewModel viewModel = vm;
            Menu.Add(new MenuItem() { Command = viewModel.GetAssemblyFilePathCommand, Header = "1. Enter file path for .dll assembly" });
            Menu.Add(new MenuItem() { Command = viewModel.LoadDataCommand, Header = "2. Load data of the chosen .dll assembly" });
            Menu.Add(new MenuItem() { Command = viewModel.SerializeToXmlCommand, Header = "3. Serialize to chosen .xml assembly" });
            Menu.Add(new MenuItem() { Command = viewModel.DeserializeFromXmlCommand, Header = "4. Load data from chosen .xml file" });
            Menu.Add(new MenuItem() { Command = new RelayCommand(() =>
            {
                if (viewModel.CanLoadData())
                {
                    ConsoleTreeView.NodeItems = viewModel.HierarchicalAreas;
                    ConsoleTreeView.Display();
                }
                else
                {
                    Console.WriteLine("Choose assembly first! Press any key...");
                    Console.ReadKey();
                }

            }),
                Header = "5. Show assembly data"
            });
            Menu.Add(new MenuItem() { Command = new RelayCommand(() => Environment.Exit(0)), Header = "q. Exit" });
        }

        public void Display()
        {
            while (true)
            {
                Menu.Print();
                Menu.InputLoop();
            }
        }
    }
}
