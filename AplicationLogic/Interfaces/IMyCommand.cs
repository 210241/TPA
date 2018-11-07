using System.Windows.Input;

namespace AplicationLogic.Interfaces
{
    public interface IMyCommand : ICommand
    {
        void RaiseCanExecuteChanged();
    }
}