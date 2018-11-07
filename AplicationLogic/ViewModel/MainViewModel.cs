using System.Collections.Generic;
using System.Threading.Tasks;
using AplicationLogic.Base;
using AplicationLogic.Interfaces;
using AplicationLogic.Model;
using DataTransfer.Interfaces;
using DataTransfer.Model;


namespace AplicationLogic.ViewModel
{
    public class MainViewModel : BindableBaseWithValidation
    {
        private readonly IFilePathProvider _filePathGetter;
        private readonly ILogger _logger;
        private readonly IDataStorageProvider _dataProvider;
        private readonly IMapper<AssemblyDataStorage, NodeItem> _mapper;

        private string _filePath;

        public string FilePath
        {
            get => _filePath;
            set
            {
                SetPropertyAndValidate(ref _filePath, value);
                LoadDataCommand.RaiseCanExecuteChanged();
            }
        }

        public IMyCommand GetFilePathCommand { get; }

        public IMyCommand LoadDataCommand { get; }

        private List<NodeItem> _nodeItems;

        public List<NodeItem> NodeItems
        {
            get => _nodeItems;
            set => SetProperty(ref _nodeItems, value);
        }

        public MainViewModel(
            IFilePathProvider filePathGetter,
            ILogger logger,
            IDataStorageProvider dataProvider,
            IMapper<AssemblyDataStorage, NodeItem> mapper)
        {
            _filePathGetter = filePathGetter;
            _logger = logger;
            _dataProvider = dataProvider;
            _mapper = mapper;
            GetFilePathCommand = new RelayCommand(GetFilePath);
            LoadDataCommand = new BaseAsynchronousCommand(LoadData, CanLoadData);
            NodeItems = new List<NodeItem>();
        }

        private void GetFilePath()
        {
            FilePath = _filePathGetter.GetFilePath();
        }

        private async Task LoadData()
        {
            Task toDo = new Task(() =>
            {
                AssemblyDataStorage storage = _dataProvider.GetDataStorage(FilePath);
                NodeItems = new List<NodeItem>() { _mapper.Map(storage) };
            });
            toDo.Start();
            await toDo.ConfigureAwait(false);
        }

        public bool CanLoadData()
        {
            if (FilePath != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}