using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Threading;
using ApplicationLogic.Base;
using ApplicationLogic.Interfaces;
using ApplicationLogic.Model;
//using Base.Interfaces;
using Reflection;
using Reflection.LogicModel;
using Reflection.MyException;


namespace ApplicationLogic.ViewModel
{
    public class MainViewModel : BindableBase
    {
        public IFatalErrorHandler ErrorHandler { get; set; }
        public IMyCommand LoadDataCommand { get; }

        public IMyCommand GetAssemblyFilePathCommand { get; }

        public IMyCommand SerializeToXmlCommand { get; }
        public IMyCommand DeserializeFromXmlCommand { get; }

        private IFilePathProvider FilePathGetter { get; }

        [Import(typeof(ILogger), AllowDefault = false)]
        private ILogger logger { get; set; }

        public PersistanceManager PersistanceManager { get; set; }

        private Reflector _reflector;

        private string _assemblyFilePath;

        private bool isDataFromSerialization = false;

        public ObservableCollection<NodeItem> HierarchicalAreas { get; set; }

        public string AssemblyFilePath
        {
            get => _assemblyFilePath;
            set
            {
                isDataFromSerialization = false;
                SetProperty(ref _assemblyFilePath, value);
                LoadDataCommand.RaiseCanExecuteChanged();
            }
        }

        public MainViewModel(IFilePathProvider pathLoader, IFatalErrorHandler errorHandler)
        {
            //this.logger = logger;
            this.FilePathGetter = pathLoader;
            HierarchicalAreas = new ObservableCollection<NodeItem>();
            LoadDataCommand = new BaseAsynchronousCommand(LoadData, CanLoadData);
            GetAssemblyFilePathCommand = new RelayCommand(GetAssemblyFilePath);
            SerializeToXmlCommand = new RelayCommand(Serialize, CanSerialize);
            DeserializeFromXmlCommand = new RelayCommand(Deserialize);
            ErrorHandler = errorHandler;
        }

        public void GetAssemblyFilePath()
        {
            AssemblyFilePath = FilePathGetter.GetFilePath(".dll");
        }

        public void Serialize()
        {
            try
            {
                PersistanceManager.Serialize(_reflector.AssemblyLogicReader);
            }
            catch(IoException e)
            {
                ErrorHandler.showMessage(e.Message);
            }
        }

        public void Deserialize()
        {
            try
            {
                AssemblyLogicReader assembly = PersistanceManager.Deserialize();
            
                _reflector = new Reflector(assembly);

                HierarchicalAreas.Clear();
                HierarchicalAreas.Add(new AssemblyNodeItem(_reflector.AssemblyLogicReader, logger));
            }
            catch (IoException e)
            {
                ErrorHandler.showMessage(e.Message);
            }
        }

        private async Task LoadData()
        {
            await Task.Run(() =>
            {
                _reflector = new Reflector(AssemblyFilePath);
            });

            HierarchicalAreas.Add(new AssemblyNodeItem(_reflector.AssemblyLogicReader, logger));
            Console.WriteLine(TypeLogicReader.TypeDictionary);
            SerializeToXmlCommand.RaiseCanExecuteChanged();
        }

        public bool CanLoadData()
        {
            if (AssemblyFilePath != null)
            {
                return true;
            }
                return false;
        }

        public bool CanSerialize()
        {
            if (_reflector != null)
                return true;
            else
                return false;
        }
    }
}
