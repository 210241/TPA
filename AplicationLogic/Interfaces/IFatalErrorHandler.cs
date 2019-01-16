using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLogic.Interfaces
{
    public interface IFatalErrorHandler
    {
        void showMessageAndCloseApplication(string message);
        void showMessage(string message);
    }
}
