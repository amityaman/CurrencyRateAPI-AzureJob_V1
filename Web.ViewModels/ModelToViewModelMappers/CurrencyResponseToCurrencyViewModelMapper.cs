using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zebpay.Application.Entities.Response;
using Zebpay.Common.Utilies.Mapper;
using Zebpay.Web.ViewModels.ViewModels;

namespace Zebpay.Web.ViewModels.ModelToViewModelMappers
{
    public class CurrencyResponseToCurrencyViewModelMapper : IMapper<CurrencyResponse, CurrencyViewModel>
    {

        public CurrencyViewModel MapToNew(CurrencyResponse source)
        {
            if (source == null) return null;
            var currViewModel = new CurrencyViewModel();
            MapToExisting(source, currViewModel);
            return currViewModel;
        }

        public void MapToExisting(CurrencyResponse source, CurrencyViewModel target)
        {
            if (source == null || target == null) return;

            target.SourceCurrency = source.SourceCurrency;
            target.ConversionRate = source.ConversionRate;
            target.Amount = source.Amount;
            target.Total = Math.Round(source.ConversionRate * source.Amount, 2).ToString() ;
            target.err = source.err;
            target.returncode = source.returncode;
            target.timestamp = source.CurrRateDate.Ticks.ToString();
        }
    }
}
