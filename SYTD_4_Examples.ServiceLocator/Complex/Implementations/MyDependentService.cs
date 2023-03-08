using SYTD_4_Examples.ServiceLocator.Complex.Interfaces;

namespace SYTD_4_Examples.ServiceLocator.Complex.Implementations
{
    internal class MyDependentService : IDependentService
    {
        private readonly ILeafService leafService;

        public MyDependentService(ILeafService leafService, IServiceProvider serviceProvider)
        {
            this.leafService = leafService;
        }

        public string GetText()
        {
            return $"Text received: {leafService.GetText()}";
        }
    }
}