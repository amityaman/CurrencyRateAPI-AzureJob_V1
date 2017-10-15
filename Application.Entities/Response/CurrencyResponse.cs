using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zebpay.Application.Entities.Response
{
    public class CurrencyResponse
    {

        public string SourceCurrency { get; set; }
        public decimal ConversionRate { get; set; }

        public string err { get; set; }
        public decimal returncode { get; set; }

        public decimal Amount { get; set; }

        public DateTime CurrRateDate { get; set; }

    }
}
