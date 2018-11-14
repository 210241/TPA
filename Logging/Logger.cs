using ApplicationLogic.Interfaces;
using System;

namespace Logging
{
    public class Logger : ILogger
    {
        public void Trace(string message)
        {
            System.Diagnostics.Trace.WriteLine(DateTime.Now + " | " + message);
        }
    }
}