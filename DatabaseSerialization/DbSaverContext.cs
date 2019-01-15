using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SerializationXml.Model;

namespace DatabaseSerialization
{
    class DbSaverContext : DbContext
    {
        public DbSaverContext() : base("name=AssemblyDatabase")
        {

        }

        public DbSet<AssemblyDbSaver> AssemblyDbSavers { get; set; }
        public DbSet<FieldDbSaver> FieldDbSavers { get; set; }
        public DbSet<MethodDbSaver> MethodDbSavers { get; set; }
        public DbSet<NamespaceDbSaver> NamespaceDbSavers { get; set; }
        public DbSet<ParameterDbSaver> ParameterDbSavers { get; set; }
        public DbSet<PropertyDbSaver> PropertyDbSavers { get; set; }
        public DbSet<TypeDbSaver> TypeDbSavers { get; set; }
    }
}
