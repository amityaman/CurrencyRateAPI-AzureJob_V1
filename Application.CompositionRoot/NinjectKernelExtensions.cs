using System.Reflection;
using Ninject;
using Ninject.Activation.Strategies;

namespace Zebpay.Application.CompositionRoot
{
    public static class NinjectKernelExtensions
    {
        public static void ComposeWeb(this IKernel kernel)
        {
            kernel.Load(Assembly.GetExecutingAssembly());
            kernel.Components.Remove<IActivationStrategy, DisposableStrategy>();           
        }
    }
}