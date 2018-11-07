using System.Threading.Tasks;
using ApplicationLogic.Interfaces;

namespace ApplicationLogic.Base
{
    public interface IAsynchronousCommand : IMyCommand
    {
        Task ExecuteAsync();

        bool CanExecute();
    }
}
