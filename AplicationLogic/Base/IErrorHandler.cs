using System;

namespace ApplicationLogic.Base
{
    public interface IErrorHandler
    {
        void HandleError(Exception ex);
    }
}
