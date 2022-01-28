using System;
using System.Collections.Generic;
using System.Text;

namespace SalesRepServices.Services_Interfaces
{
    interface IOperationStatus
    {
        bool isSuccess();
        string ErrorMessage();
    }
}
