﻿using System;
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


namespace ApplicationLogic.ViewModel
{
    public class MainViewModel : BindableBase
    {
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

        public MainViewModel(IFilePathProvider pathLoader)
        {
            //this.logger = logger;
            this.FilePathGetter = pathLoader;
            HierarchicalAreas = new ObservableCollection<NodeItem>();
            LoadDataCommand = new BaseAsynchronousCommand(LoadData, CanLoadData);
            GetAssemblyFilePathCommand = new RelayCommand(GetAssemblyFilePath);
            SerializeToXmlCommand = new RelayCommand(SerializeToXml, CanSerialize);
            DeserializeFromXmlCommand = new RelayCommand(DeserializeFromXml);
        }

        public void GetAssemblyFilePath()
        {
            AssemblyFilePath = FilePathGetter.GetFilePath(".dll");
        }

        public void SerializeToXml()
        {
            string pathToSaveSerializedFile = FilePathGetter.GetFilePath(".xml");

            PersistanceManager.SerializeToXml(_reflector.AssemblyLogicReader, pathToSaveSerializedFile);

        }

        public void DeserializeFromXml()
        {

            string pathToSerializedFile = FilePathGetter.GetFilePath(".xml");

            if(pathToSerializedFile != null)
            { 
                _reflector = new Reflector(PersistanceManager.DeserializeFromXml(pathToSerializedFile));

                HierarchicalAreas.Clear();
                HierarchicalAreas.Add(new AssemblyNodeItem(_reflector.AssemblyLogicReader, logger));
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
