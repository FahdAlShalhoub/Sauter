
namespace Sauter.Features.Hooks
{
    [Binding]
    public class Hooks
    {
        [BeforeScenario]
        public void SetupInventoryManagement()
        {
            InventoryManagement.InventoryManagement.Reset();
        }
    }
}