namespace Zebpay.Web.ViewModels.ViewModels
{
    public class CurrencyViewModel
    {

        public string SourceCurrency { get; set; }
        public decimal ConversionRate { get; set; }
        public decimal Amount { get; set; }

        public string Total { get; set; }

        public decimal returncode { get; set; }

        public string err { get; set; }

        public string timestamp { get; set; }

    }
}
