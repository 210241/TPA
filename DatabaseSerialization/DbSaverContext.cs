using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseSerialization.Model;

namespace DatabaseSerialization
{
    public class DbSaverContext : DbContext
    {
        public DbSaverContext() : base("name=AssemblyDatabase")
        {
            Database.CommandTimeout = 900;
        }

        public virtual DbSet<AssemblyDbSaver> AssemblyDbSavers { get; set; }
        public virtual DbSet<FieldDbSaver> FieldDbSavers { get; set; }
        public virtual DbSet<MethodDbSaver> MethodDbSavers { get; set; }
        public virtual DbSet<NamespaceDbSaver> NamespaceDbSavers { get; set; }
        public virtual DbSet<ParameterDbSaver> ParameterDbSavers { get; set; }
        public virtual DbSet<PropertyDbSaver> PropertyDbSavers { get; set; }
        public virtual DbSet<TypeDbSaver> TypeDbSavers { get; set; }
        
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

        }
    }
}
