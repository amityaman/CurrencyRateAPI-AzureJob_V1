using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Zebpay.Application.Entities.Response;
using Zebpay.Application.Repositories.Contracts;
using Zebpay.Common.Utilies.Mapper;
using Zebpay.Web.ViewModels.ViewModels;

namespace Zebpay.Application.API.Controllers
{
    [RoutePrefix("api")]
    public class CurrencyController : ApiController
    {
        #region Data Member

        private readonly ICurrencyRepository _currencyRepository;
        private readonly IMapper<CurrencyResponse, CurrencyViewModel> _responseMapper;

        #endregion

        #region Constructors

        public CurrencyController(ICurrencyRepository currencyRepository,
            IMapper<CurrencyResponse, CurrencyViewModel> responseMapper)
        {
            _currencyRepository = currencyRepository;
            _responseMapper = responseMapper;
        }

        #endregion


        #region Public API Methods
        
        /// <summary>
        /// Get the currency rate
        /// </summary>
        /// <param name="currencyCode"></param>
        /// <param name="amount"></param>       
        /// <returns></returns>
        // /// Here we can create complex object like class CurrencyRequest and within that we have two properties currencyCode & amount
        // POST api/v0/rate

        [HttpPost]
        [Route("v0/rate")]
        
        public IHttpActionResult GetCurrencyRate(string currencyCode = "USD", decimal amount = 1)
        {
            //Here we can create complex object like class CurrencyRequest and
            //within that we have two properties currencyCode & amount

            var model = _currencyRepository.GetCurrencyRate(currencyCode, amount);
           
            var viewModel = _responseMapper.MapToNew(model);

            if (String.IsNullOrEmpty(viewModel.SourceCurrency))
            {
                //return Content(HttpStatusCode.NotFound, "Currency Rate based on " + currencyCode.ToString() + " not found");
                return Content(HttpStatusCode.NotFound, viewModel);
            }            

            return Ok(viewModel);
        }

        /// <summary>
        /// Insert New rate in Database
        /// </summary>
        /// <param name="currencyViewModel"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("exchrate/create")]        
        public async Task<IHttpActionResult> Create(CurrencyViewModel currencyViewModel)
        {
           
            var result = await _currencyRepository.Create(currencyViewModel);          
            
            return Ok();
        }

        #endregion


    }
}