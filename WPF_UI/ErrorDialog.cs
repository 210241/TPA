using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ApplicationLogic.Interfaces;

namespace WPF_UI
{
    public class ErrorDialog : IFatalErrorHandler
    {
        public void showMessageAndCloseApplication(string message)
        {
            MessageBox.Show(message);
            System.Windows.Application.Current.Shutdown();
        }
    }
}
