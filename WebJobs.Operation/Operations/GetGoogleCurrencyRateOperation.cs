using System;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using RestSharp;
using System.Globalization;
using System.Threading;
using System.Text.RegularExpressions;
using Zebpay.WebJobs.Operation.Entities;
using System.Net.Http;
using Newtonsoft.Json;

namespace Zebpay.WebJobs.Operation.Operations
{
    public class GetGoogleCurrencyRateOperation : BaseOperation, IOperation
    {
        private string _currencys = string.Empty;
        private string _api = string.Empty;
        public GetGoogleCurrencyRateOperation()
        {
            _currencys = Convert.ToString(ConfigurationManager.AppSettings["Currency"]);
            _api = Convert.ToString(ConfigurationManager.AppSettings["WebJobOperationAPI"]);
        }
        public override string OperationName
        {
            get
            {
                return Convert.ToString(ConfigurationManager.AppSettings["WebJobGetCurrencyRateName"]);
            }
        }


        /* I have tried to used Google’s API But none is giving the expected result so finally
                I am using Currency Converter using Yahoo’s API to get an expected result             
                */

        //Here is the below link I have followed to get an Real-Time rate using Google’s API  but none is giving the expected result
        //https://www.codeproject.com/Articles/421725/ASP-NET-Real-time-Currency-Converter-using-API-Goo
        //http://www.ashishblog.com/currency-exchange-rate-in-webpage-using-c-asp-net/
        //https://github.com/001saraju/Currency-Rate-api/blob/master/CurrencyRate/RateSchedular.cs
        //https://www.aspsnippets.com/Articles/Currency-Conversion-as-per-Exchange-Rates-using-Google-Finance-API-Web-Service-in-ASPNet.aspx
        //http://www.aspdotnet-suresh.com/2013/01/aspnet-google-currency-converter-json.html

        public override async Task ProcessOperation()
        {
            try
            {
                //List<string> currencyList = new List<string> { "USD", "GBP", "AUD", "EUR", "CAD", "SGD" };

                var currencys = _currencys;

                var currencyList = currencys.Split(',').ToList();

                var currenyTaskList = currencyList.Select(currency =>
                          LoadRateAsync(currency)).ToList();

                await Task.WhenAll(currenyTaskList);

                var result = currenyTaskList.Select(p => p.Result).ToList();

                var insertRateinDBTaskList = result.Select(request =>
                         InsertNewRateInDBAsync(request)).ToList();

                await Task.WhenAll(insertRateinDBTaskList);


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private async Task<RateEntity> LoadRateAsync(string currency)
        {

            RestClient client = new RestClient { BaseUrl = new Uri("http://finance.yahoo.com") };
            string resource = string.Format("/d/quotes.csv?e=.csv&f=sl1d1t1&s={0}{1}=X", currency, "INR");
            var request = new RestRequest(resource, Method.GET);

            var cancellationTokenSource = new CancellationTokenSource();

            var restResponse = await client.ExecuteTaskAsync(request, cancellationTokenSource.Token);

            var response = restResponse.Content;

            string[] values = Regex.Split(response, ",");

            decimal rate = System.Convert.ToDecimal(values[1]);

            decimal conversionRate = decimal.Parse(values[1], CultureInfo.InvariantCulture);

            var rateResp = new RateEntity
            {
                SourceCurrency = currency,
                ConversionRate = conversionRate,

            };

            return rateResp;
        }


        private async Task<bool> InsertNewRateInDBAsync(RateEntity request)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var insertNewRateInDBAUrl = _api;

                    var response =
                        await
                            client.PostAsJsonAsync(
                                insertNewRateInDBAUrl,
                                request);

                    string resultContent = await response.Content.ReadAsStringAsync();
                }
            }

            catch (HttpRequestException e)
            {
                string postBody = JsonConvert.SerializeObject(request);
               
            }
            return true;
        }
    }
}
