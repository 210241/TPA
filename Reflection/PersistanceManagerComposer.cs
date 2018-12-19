using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reflection
{
    public static class PersistanceManagerComposer
    {
        public static PersistanceManager ComposePersistanceManager()
        {
            var catalog = new AggregateCatalog();
            //Adds all the parts found in the same assembly as the Program class
            catalog.Catalogs.Add(new DirectoryCatalog("C:\\Users\\Jakub\\source\\repos\\TPA\\AplicationLogic\\bin\\Debug\\plugins"));

            //Create the CompositionContainer with the parts in the catalog
            CompositionContainer _container;
            _container = new CompositionContainer(catalog);


            PersistanceManager manager = new PersistanceManager();

            //Fill the imports of this object
                _container.ComposeParts(manager);


            return manager;
        }
    }
}
