using System;
using System.ComponentModel.Composition;
using System.Data.Entity;
using System.Xml.Serialization;
using ApplicationLogic.Interfaces;


namespace LoggingDb
{
    [Export(typeof(ILogger))]
    public class DbLogger : ILogger
    {
        public DbLogger()
        {
            Database.SetInitializer(new DropCreateDatabaseAlways<LogDbContext>());
        }
        public void Trace(string message)
        {
            using ( LogDbContext dbContext = new LogDbContext())
            {
                dbContext.Log.Add(new LogInfo()
                {
                    CallerName = "test",
                    Message = message,
                    Time = DateTime.Now.ToLocalTime()
                });
                dbContext.SaveChanges();
            }
        }
    }
}