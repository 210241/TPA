using System;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Windows.Input;
using ApplicationLogic.Base;
using ApplicationLogic.Interfaces;
using ApplicationLogic.Model;
using DataTransfer.Interfaces;
using Reflection.ReflectionPartials;

namespace ApplicationLogic.ViewModel
{
    public class MainViewModel : BindableBase
    {
        public ICommand LoadDataCommand { get; }

        public IFilePathProvider PathLoader { get; set; }

        public ILogger Logger { get; set; }

        private Reflection.Reflection _reflector;

        private string _pathVariable;

        public ObservableCollection<NodeItem> HierarchicalAreas { get; set; }

        public string PathVariable
        {
            get => _pathVariable;
            set => SetProperty(ref _pathVariable, value);
        }

        public MainViewModel()
        {
            HierarchicalAreas = new ObservableCollection<NodeItem>();
            LoadDataCommand = new RelayCommand(Open);
        }

        private void Open()
        {
            string path = PathLoader.GetFilePath();
            if (path == null || !path.Contains(".dll")) return;
            PathVariable = path;
            try
            {
                _reflector = new Reflection.Reflection(Assembly.LoadFrom(PathVariable));
            }
            catch (Exception)
            {
                // ignored
            }

            // TODO clear?
            HierarchicalAreas.Add(new AssemblyNodeItem(_reflector.AssemblyReader));
        }
    }
}
