using System.ComponentModel.Composition;
using System.Data.Entity;
using System.Linq;
using System.Media;
using Base.Interfaces;
using Base.Model;
using DatabaseSerialization.Model;

namespace DatabaseSerialization
{
    [Export(typeof(ISerializator))]
    public class DbManager : ISerializator
    {
        public void Serialize(AssemblyBase assemblyBase, string connectionString)
        {
            // TODO maybe clear
            Database.SetInitializer(new DropCreateDatabaseAlways<DbSaverContext>());
//            ClearDB();
            AssemblyDbSaver assembly = new AssemblyDbSaver(assemblyBase);
            
            using (var context = new DbSaverContext())
            {
                context.AssemblyDbSavers.Add(assembly);
                context.SaveChanges();
                SystemSounds.Beep.Play();
            }
        }

        public AssemblyBase Deserialize(string connectionString)
        {
            AssemblyBase assembly = new AssemblyBase();
            using (var context = new DbSaverContext())
            {
//                context.Configuration.LazyLoadingEnabled = false;
                context.AssemblyDbSavers.Load();
                context.NamespaceDbSavers.Load();
                context.TypeDbSavers.Load();
                context.MethodDbSavers.Load();
                context.PropertyDbSavers.Load();
                context.ParameterDbSavers.Load();
//                context.Configuration.ProxyCreationEnabled = false;
//                context.AssemblyDbSavers
//                    .Include(a => a.Namespaces)
//                    .Load();
//                context.NamespaceDbSavers
//                    .Include(n => n.Types)
//                    .Load();
//                context.TypeDbSavers
//                    .Include(t => t.Constructors)
//                    .Include(t => t.BaseType)
//                    .Include(t => t.DeclaringType)
//                    .Include(t => t.Fields)
//                    .Include(t => t.ImplementedInterfaces)
//                    .Include(t => t.GenericArguments)
//                    .Include(t => t.Methods)
//                    .Include(t => t.NestedTypes)
//                    .Include(t => t.Properties)
//                    .Include(t => t.TypeGenericArguments)
//                    .Include(t => t.TypeImplementedInterfaces)
//                    .Include(t => t.TypeNestedTypes)
//                    .Include(t => t.MethodGenericArguments)
//                    .Include(t => t.TypeBaseTypes)
//                    .Include(t=> t.TypeDeclaringTypes)
//                    .Load();
//                context.ParameterDbSavers
//                    .Include(p => p.Type)
//                    .Include(p => p.TypeFields)
//                    .Include(p => p.MethodParameters)
//                    .Load();
//                context.MethodDbSavers
//                    .Include(m => m.GenericArguments)
//                    .Include(m => m.Parameters)
//                    .Include(m => m.ReturnType)
//                    .Include(m => m.TypeConstructors)
//                    .Include(m => m.TypeMethods)
//                    .Load();
//                context.PropertyDbSavers
//                    .Include(p => p.Type)
//                    .Include(p => p.TypeProperties)
//                    .Load();


                assembly = DataTransferGraphMapper.AssemblyBase(context.AssemblyDbSavers.First());
            }

            return assembly;
        }
        
//        private void ClearDB()
//        {
//            using (DbSaverContext context = new DbSaverContext())
//            {
//                context.Database.ExecuteSqlCommand("DELETE FROM ParameterDbSavers WHERE ParameterDbSaverId != -1");
//                context.Database.ExecuteSqlCommand("DELETE FROM FieldDbSavers WHERE FieldDbSaverId != -1");
//                context.Database.ExecuteSqlCommand("DELETE FROM PropertyDbSavers WHERE PropertyDbSaverId != -1");
//                context.Database.ExecuteSqlCommand("DELETE FROM MethodDbSavers WHERE MethodDbSaverId != -1");
//                context.Database.ExecuteSqlCommand("DELETE FROM TypeDbSavers ");
//                context.Database.ExecuteSqlCommand("DELETE FROM NamespaceDbSavers WHERE NamespaceDbSaverId != -1");
//                context.Database.ExecuteSqlCommand("DELETE FROM AssemblyDbSavers WHERE AssemblyDbSaverId != -1");
//                context.SaveChanges();
//            }
//        }
    }
}