using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zebpay.Application.Dal.Contracts;
using Zebpay.Application.Entities.Response;
using Zebpay.Application.Repositories.Contracts;
using Zebpay.Web.ViewModels.ViewModels;

namespace Zebpay.Application.Repositories.Implementations
{
    public class CurrencyRepository : ICurrencyRepository
    {

        #region Data Member

        private readonly ICurrencyDal _currencyDal;

        #endregion

        #region Constructors

        public CurrencyRepository(ICurrencyDal currencyDal)
        {
            _currencyDal = currencyDal;
        }

        #endregion

        #region Public Methods
        public CurrencyResponse GetCurrencyRate(string currencyCode, decimal amount)
        {
            return _currencyDal.GetCurrencyRate(currencyCode, amount);
        }

        public async Task<bool> Create(CurrencyViewModel currencyViewModel)
        {
            return await _currencyDal.Create(currencyViewModel);
        }

        #endregion
    }
}
