using System;
using ApplicationLogic.Interfaces;


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
