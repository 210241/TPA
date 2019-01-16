using System.Windows;
using ApplicationLogic.Base;
using ApplicationLogic.ViewModel;

namespace WPF_UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            DataContext = Compose.ComposeViewModel( new FileDialog(), new ErrorDialog());
        }
    }
}