using System;

namespace LoggingDb
{
    public class LogInfo
    {
        public int LogInfoId { get; set; }
        public string Message { get; set; }
        public DateTime Time { get; set; }
        public string CallerName { get; set; }
    }
    
}