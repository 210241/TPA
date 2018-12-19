using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationLogic.Interfaces;

namespace Console_UI
{
    public class ErrorInfo : IFatalErrorHandler
    {
        public void showMessageAndCloseApplication(string message)
        {
            Console.Clear();
            Console.WriteLine(message);
            Console.WriteLine("To close press any key...");
            Console.ReadKey();
            Environment.Exit(1);
        }
    }
}
