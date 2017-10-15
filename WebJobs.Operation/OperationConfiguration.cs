using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zebpay.WebJobs.Operation.Operations;

namespace Zebpay.WebJobs.Operation
{
    public class OperationConfiguration
    {
        public static List<IOperation> GetOperations()
        {
            return new List<IOperation>
            {
                new GetGoogleCurrencyRateOperation()               
            };
        }
    }
}