using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Threading;
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

        private IFilePathProvider FilePathGetter { get; }

        private ILogger logger { get; }

        private Reflection.Reflector _reflector;

        private string _filePath;

        public ObservableCollection<NodeItem> HierarchicalAreas { get; set; }

        public string FilePath
        {
            get => _filePath;
            set
            {
                SetProperty(ref _filePath, value);
                LoadDataCommand.RaiseCanExecuteChanged();
            }
        }

        public MainViewModel(ILogger logger, IFilePathProvider pathLoader)
        {
            this.logger = logger;
            this.FilePathGetter = pathLoader;
            HierarchicalAreas = new ObservableCollection<NodeItem>();
            LoadDataCommand = new BaseAsynchronousCommand(LoadData, CanLoadData);
            GetFilePathCommand = new RelayCommand(GetFilePath);
        }

        public void GetFilePath()
        {
            FilePath = FilePathGetter.GetFilePath();
        }

        private async Task LoadData()
        {
            await Task.Run(() =>
            {
                _reflector = new Reflector(FilePath);
            });

            HierarchicalAreas.Add(new AssemblyNodeItem(_reflector.AssemblyReader, logger));
        }

        public bool CanLoadData()
        {
            if (FilePath != null)
            {
                return true;
            }
                return false;
        }
    }
}
