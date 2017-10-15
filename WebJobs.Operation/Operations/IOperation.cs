using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zebpay.WebJobs.Operation.Operations
{
    public interface IOperation
    {
        string OperationName { get; }

        Task Execute();
    }
}