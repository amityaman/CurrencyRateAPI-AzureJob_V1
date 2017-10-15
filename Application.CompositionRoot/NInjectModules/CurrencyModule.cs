using System;
using Ninject.Modules;
using Zebpay.Application.Repositories.Contracts;
using Zebpay.Application.Repositories.Implementations;
using Zebpay.Application.Dal.Contracts;
using Zebpay.Application.Dal.Implementations;
using Zebpay.Common.Utilies.Mapper;
using Zebpay.Application.Entities.Response;
using Zebpay.Web.ViewModels.ViewModels;
using Zebpay.Web.ViewModels.ModelToViewModelMappers;

namespace Zebpay.Application.CompositionRoot.NInjectModules
{
    public class CurrencyModule : NinjectModule
    {
        public override void Load()
        {
            Bind<ICurrencyRepository>().To<CurrencyRepository>();
            Bind<ICurrencyDal>().To<CurrencyDal>();
            Bind<IMapper<CurrencyResponse, CurrencyViewModel>>().To<CurrencyResponseToCurrencyViewModelMapper>();
        }
    }
}
