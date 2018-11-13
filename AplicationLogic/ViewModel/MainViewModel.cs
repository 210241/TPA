using System;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Windows.Input;
using ApplicationLogic.Base;
using ApplicationLogic.Interfaces;
using ApplicationLogic.Model;
using ApplicationLogic.Interfaces;
using Reflection;
using Reflection.Model;

namespace ApplicationLogic.ViewModel
{
    public class MainViewModel : BindableBase
    {
        public IMyCommand LoadDataCommand { get; }

        public IMyCommand GetFilePathCommand { get; }

        private IFilePathProvider filePathGetter { get; set; }

        private ILogger logger { get; set; }

        private Reflection.Reflector _reflector;

        private string _filePath;

        public ObservableCollection<NodeItem> HierarchicalAreas { get; set; }

        public string FilePath
        {
            get => _filePath;
            set => SetProperty(ref _filePath, value);
        }

        public MainViewModel(ILogger logger, IFilePathProvider pathLoader)
        {
            this.logger = logger;
            this.filePathGetter = pathLoader;
            HierarchicalAreas = new ObservableCollection<NodeItem>();
            LoadDataCommand = new RelayCommand(Open);
            GetFilePathCommand = new RelayCommand(GetFilePath);
        }

        public void GetFilePath()
        {
            FilePath = filePathGetter.GetFilePath();
        }


        private void Open()
        {
            try
            {
                _reflector = new Reflector(FilePath);
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
