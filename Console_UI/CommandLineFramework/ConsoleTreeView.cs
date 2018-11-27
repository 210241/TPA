using ApplicationLogic.Model;
using Console_UI.TypeConverter;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Console_UI.CommandLineFramework
{
    public class ConsoleTreeView
    {
        public ObservableCollection<NodeItem> NodeItems
        {
            get => _items;
            set
            {
                _history.Clear();
                _currentItems.Clear();
                _items = value;
                _currentItems.Add("1", _items[0]);
            }
        }

        private ObservableCollection<NodeItem> _items = new ObservableCollection<NodeItem>();

        private List<NodeItem> _history = new List<NodeItem>();

        private IDictionary<string, NodeItem> _currentItems = new Dictionary<string, NodeItem>();

        public void Display()
        {
            bool insideTree = true;
            bool correctOption = false;
            while (insideTree)
            {
                correctOption = false;
                DisplayElements();
                while (!correctOption)
                {
                    Console.WriteLine("Choose node to expand: ");
                    string choice = Console.ReadLine();

                    if (_currentItems.ContainsKey(choice))
                    {
                        _currentItems[choice].IsExpanded = true;
                        UpdateCurrentItems(_currentItems[choice], false);
                        correctOption = true;
                    }
                    else if (choice == "back")
                    {
                        backChoice();

                        correctOption = true;
                    }
                    else if (choice == "quit")
                    {
                        correctOption = true;
                        insideTree = false;
                    }
                }
            }
        }

        public void UpdateCurrentItems(NodeItem currentParent, bool isBack)
        {
            int firstNumber = 1; // (char) 65;
            if (!isBack) _history.Add(currentParent);
            _currentItems = currentParent.Children.ToDictionary(x => (firstNumber++).ToString(), x => x);
            if (_currentItems.Count == 0)
            {
                backChoice();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("This item is not expandable. Choose another or type \"back\" ");
                Console.ResetColor();
            }
        }

        public void DisplayElements()
        {
            foreach (KeyValuePair<string, NodeItem> currentItem in _currentItems)
            {
                Console.Write($"{currentItem.Key} - ");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write(TypeToStringConverter.GetStringFromType(currentItem.Value));
                Console.ResetColor();
                Console.WriteLine(currentItem.Value.Name);
            }
        }

        private void backChoice()
        {
            if (_history.Count > 1)
            {
                _history.RemoveAt(_history.Count - 1);
            }

            if (_history.Any())
            {
                NodeItem currentParent = _history.Last();
                UpdateCurrentItems(currentParent, true);
            }
            else
            {
                _currentItems.Clear();
                _currentItems.Add("A", _items[0]);
            }
        }
    }
}