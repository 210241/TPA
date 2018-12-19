using ApplicationLogic.Interfaces;
using System;
using System.ComponentModel.Composition;

namespace Logging
{
    [Export(typeof(ILogger))]
    public class Logger : ILogger
    {
        public void Trace(string message)
        {
            System.Diagnostics.Trace.WriteLine(DateTime.Now + " | " + message);
        }
    }
}