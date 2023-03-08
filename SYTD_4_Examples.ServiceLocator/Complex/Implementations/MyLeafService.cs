using SYTD_4_Examples.ServiceLocator.Complex.Interfaces;

namespace SYTD_4_Examples.ServiceLocator.Complex.Implementations
{
    public class MyLeafService : ILeafService
    {
        public string GetText()
        {
            return "This is leaf.";
        }
    }
}
