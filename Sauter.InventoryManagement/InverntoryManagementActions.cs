using System.Reflection;
using Sauter.AuditLog;

namespace Sauter.InventoryManagement;

public static class InventoryManagementActions
{
    public static string StoreItem(string itemName, int quantity)
    {
        Item item = new()
        {
            Id = Guid.NewGuid(),
            Name = itemName,
            Quantity = quantity,
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now
        };
        
        Inventory.Instance.Items.Add(item.Id, item);
        
        AuditLogActions.Log(MethodBase.GetCurrentMethod(), new List<object>{itemName, quantity});
        
        return item.Id.ToString();
    }

    public static ItemDto GetItemById(string itemIdentifier)
    {
        var item = Inventory.Instance.Items[Guid.Parse(itemIdentifier)];

        return item.ToDto;
    }

    public static void IncrementItem(string itemIdentifier, int incrementAmount)
    {
        var item = Inventory.Instance.Items[Guid.Parse(itemIdentifier)];
        item.Quantity += incrementAmount;
        item.UpdatedAt = DateTime.Now;

        AuditLogActions.Log(MethodBase.GetCurrentMethod(), new List<object>{itemIdentifier, incrementAmount});
    }

    public static void DecrementItem(string itemIdentifier, int decrementAmount)
    {
        var item = Inventory.Instance.Items[Guid.Parse(itemIdentifier)];
        item.Quantity -= decrementAmount;
        item.UpdatedAt = DateTime.Now;
        
        AuditLogActions.Log(MethodBase.GetCurrentMethod(), new List<object>{itemIdentifier, decrementAmount});
    }

    public static IEnumerable<ItemDto> SearchItemByName(string searchTerm)
    {
        return Inventory.Instance.Items.Values
            .AsParallel()
            .Where(item => item.Name.Contains(searchTerm))
            .Select(item => item.ToDto);
    }

    public static void Reset()
    {
        Inventory.Instance.Items.Clear();
    }
}