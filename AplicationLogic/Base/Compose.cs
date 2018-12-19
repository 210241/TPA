using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationLogic.Interfaces;
using ApplicationLogic.ViewModel;
using Reflection;

namespace ApplicationLogic.Base
{
    public static class Compose
    {
        public static MainViewModel ComposeViewModel(IFilePathProvider filePathProvider, IFatalErrorHandler errorHandler)
        {
            var catalog = new AggregateCatalog();
            //Adds all the parts found in the same assembly as the Program class
            catalog.Catalogs.Add(new DirectoryCatalog("C:\\Users\\Jakub\\source\\repos\\TPA\\AplicationLogic\\bin\\Debug\\plugins"));
            
            //Create the CompositionContainer with the parts in the catalog
            CompositionContainer _container;
            _container = new CompositionContainer(catalog);


            MainViewModel mv = new MainViewModel(filePathProvider);

            //Fill the imports of this object
            try
            {
                _container.ComposeParts(mv);
                mv.PersistanceManager = PersistanceManagerComposer.ComposePersistanceManager();
            }
            catch (CompositionException compositionException)
            {
                Console.WriteLine(compositionException.ToString());
                errorHandler.showMessageAndCloseApplication(compositionException.ToString());
            }
            
            return mv;

        }
    }
}
