using System.Data.Entity;

namespace LoggingDb
{
    public class LogDbContext : DbContext
    {
        public LogDbContext() :base("name=LoggerDatabase")
        {
            
        }

        public virtual DbSet<LogInfo> Log { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

        }
    }
}
