using System.Windows.Input;

namespace ApplicationLogic.Interfaces
{
    public interface IMyCommand : ICommand
    {
        void RaiseCanExecuteChanged();
    }
}