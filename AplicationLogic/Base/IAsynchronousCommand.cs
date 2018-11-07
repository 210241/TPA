using System.Threading.Tasks;
using AplicationLogic.Interfaces;

namespace AplicationLogic.Base
{
    public interface IAsynchronousCommand : IMyCommand
    {
        Task ExecuteAsync();

        bool CanExecute();
    }
}
