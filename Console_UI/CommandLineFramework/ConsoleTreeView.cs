using System;
using System.Collections.Generic;
using System.Linq;
using ApplicationLogic.Model;


namespace Console_UI.CommandLineFramework
{
    public class ConsoleTreeView
    {
        public List<NodeItem> NodeItems
        {
            get => _items;
            set
            {
                _history.Clear();
                _currentItems.Clear();
                _items = value;
                _currentItems.Add("A", _items[0]);
            }
        }

        private List<NodeItem> _items = new List<NodeItem>();

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
                    if (_currentItems.ContainsKey(choice) && _currentItems[choice].IsExpendable)
                    {
                        UpdateCurrentItems(_currentItems[choice], false);
                        correctOption = true;
                    }
                    else if (choice == "back")
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
            char firstChar = 'A'; // (char) 65;
            if (!isBack) _history.Add(currentParent);
            _currentItems = currentParent.Children.ToDictionary(x => (firstChar++).ToString(), x => x);
        }

        public void DisplayElements()
        {
            foreach (KeyValuePair<string, NodeItem> currentItem in _currentItems)
            {
                if (currentItem.Value.IsExpendable == false)
                {
                    Console.WriteLine($"{currentItem.Key} - {currentItem.Value.Name} -- not expendable");
                }
                else
                {
                    Console.WriteLine($"{currentItem.Key} - {currentItem.Value.Name}");
                }
            }
        }
    }
}
