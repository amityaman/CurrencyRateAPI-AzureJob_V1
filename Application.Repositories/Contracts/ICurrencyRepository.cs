using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zebpay.Application.Entities.Response;
using Zebpay.Web.ViewModels.ViewModels;

namespace Zebpay.Application.Repositories.Contracts
{
    #region Interfaces
    public interface ICurrencyRepository
    {
        CurrencyResponse GetCurrencyRate(string currencyCode, decimal amount);

        Task<bool> Create(CurrencyViewModel currencyViewModel);
    }

    #endregion
}
