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
        public void Serialize(AssemblyBase assemblyBase)
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

        public AssemblyBase Deserialize()
        {
            AssemblyBase assembly = new AssemblyBase();
            using (var context = new DbSaverContext())
            {

                context.AssemblyDbSavers.Load();
                context.NamespaceDbSavers.Load();
                context.TypeDbSavers.Load();
                context.MethodDbSavers.Load();
                context.PropertyDbSavers.Load();
                context.ParameterDbSavers.Load();

                assembly = DataTransferGraphMapper.AssemblyBase(context.AssemblyDbSavers.First());
            }

            return assembly;
        }
    }
}