
using Sauter.AuditLog;
using Sauter.InventoryManagement;

namespace Sauter.Features.Hooks
{
    [Binding]
    public class Hooks
    {
        [BeforeScenario]
        public void SetupInventoryManagement()
        {
            InventoryManagementActions.Reset();
            AuditLogActions.Reset();
        }
    }
}