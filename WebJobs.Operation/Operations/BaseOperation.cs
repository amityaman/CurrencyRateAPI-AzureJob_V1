using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zebpay.WebJobs.Operation.Operations
{
    public abstract class BaseOperation : IOperation
    {
        public abstract string OperationName { get; }

        public abstract  Task ProcessOperation();

        public async Task Execute()
        {
            try
            {
                await ProcessOperation();
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}