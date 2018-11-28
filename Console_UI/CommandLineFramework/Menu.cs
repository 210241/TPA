using System;
using System.Collections.Generic;
using System.Linq;

namespace Console_UI.CommandLineFramework
{
    public class Menu
    {
        private readonly List<MenuItem> _menuItems = new List<MenuItem>();

        public IEnumerable<MenuItem> Items => _menuItems;

        public void Print(bool clearScreen = true)
        {
            if (clearScreen)
                Console.Clear();

            foreach (MenuItem menuItem in Items)
            {
                Console.WriteLine(menuItem.Header);
            }
        }

        public void InputLoop()
        {
            bool notDone = true;
            while (notDone)
            {
                ConsoleKeyInfo key = Console.ReadKey();
                Console.Clear();
                foreach (MenuItem menuItem in _menuItems.Where(item => item.Command != null && item.Header.Substring(0, 1) == key.KeyChar.ToString()))
                {
                    if (menuItem.Command.CanExecute(null))
                    {
                        menuItem.Command.Execute(null);
                        notDone = false;
                    }
                    else
                    {
                        Console.WriteLine("Program cannot execute this command now. Press any key...");
                        Console.ReadKey();
                        return;
                    }
                }
            }
        }

        public void Add(MenuItem menuItem)
        {
            _menuItems.Add(menuItem);
        }
    }
}