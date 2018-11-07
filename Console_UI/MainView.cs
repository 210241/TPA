using System;
using ApplicationLogic.Base;
using ApplicationLogic.ViewModel;
using Console_UI.CommandLineFramework;

namespace Console_UI
{
    public class MainView
    {
        private readonly MainViewModel _viewModel;

        public Menu Menu { get; } = new Menu();

        public ConsoleTreeView ConsoleTreeView { get; } = new ConsoleTreeView();

        public MainView(MainViewModel viewModel)
        {
            _viewModel = viewModel;
            Menu.Add(new MenuItem() { Command = _viewModel.GetFilePathCommand, Header = "1. Enter file path for .dll assembly" });
            Menu.Add(new MenuItem() { Command = _viewModel.LoadDataCommand, Header = "2. Load data of the chosen .dll assembly" });
            Menu.Add(new MenuItem() { Command = new RelayCommand(() =>
            {
                ConsoleTreeView.NodeItems = _viewModel.NodeItems;
                ConsoleTreeView.Display();
            }),
                Header = "3. Show assembly data"
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
