using System;

namespace AplicationLogic.Base
{
    public interface IErrorHandler
    {
        void HandleError(Exception ex);
    }
}
