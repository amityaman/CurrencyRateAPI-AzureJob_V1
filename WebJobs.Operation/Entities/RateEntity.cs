using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zebpay.WebJobs.Operation.Entities
{
    public class RateEntity
    {
        public string SourceCurrency { get; set; }
        public decimal ConversionRate { get; set; }

    }
}
