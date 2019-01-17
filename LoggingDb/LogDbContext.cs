using System.Data.Entity;

namespace LoggingDb
{
    public class LogDbContext : DbContext
    {
        public LogDbContext() :base("name=LoggerDatabase")
        {
            Database.CommandTimeout = 900;
        }

        public virtual DbSet<LogInfo> Log { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

        }
    }
}
