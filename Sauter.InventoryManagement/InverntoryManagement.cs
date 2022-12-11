namespace Sauter.InventoryManagement;

public class InventoryManagement
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

        AuditLog auditLog = new AuditLog
        {
            Action = $"Action StoreItem Was Committed With Input {itemName} - {quantity}",
            DateOfOccurence = DateTime.Now
        };
        
        Inventory.Instance.Items.Add(item.Id, item);
        AuditLogger.Instance.Logs.Add(auditLog);
        
        return item.Id.ToString();
    }

    public static ItemDto GetItemById(string itemIdentifier)
    {
        var item = Inventory.Instance.Items[Guid.Parse(itemIdentifier)];

        return ToDto(item);
    }

    public static void IncrementItem(string itemIdentifier, int incrementAmount)
    {
        var item = Inventory.Instance.Items[Guid.Parse(itemIdentifier)];
        item.Quantity += incrementAmount;
        item.UpdatedAt = DateTime.Now;
        
        AuditLog auditLog = new AuditLog
        {
            Action = $"Action IncreaseItem Was Committed With Input {itemIdentifier} - {incrementAmount}",
            DateOfOccurence = DateTime.Now
        };
        AuditLogger.Instance.Logs.Add(auditLog);

    }

    public static void DecrementItem(string itemIdentifier, int decrementAmount)
    {
        var item = Inventory.Instance.Items[Guid.Parse(itemIdentifier)];
        item.Quantity -= decrementAmount;
        item.UpdatedAt = DateTime.Now;
        
        AuditLog auditLog = new AuditLog
        {
            Action = $"Action DecreaseItem Was Committed With Input {itemIdentifier} - {decrementAmount}",
            DateOfOccurence = DateTime.Now
        };
        AuditLogger.Instance.Logs.Add(auditLog);
    }

    public static IEnumerable<ItemDto> SearchItemByName(string searchTerm)
    {
        return Inventory.Instance.Items.Values
            .AsParallel()
            .Where(item => item.Name.Contains(searchTerm))
            .Select(ToDto);
    }
    
    public static List<AuditLogDto> GetAuditLogs()
    {
        return AuditLogger.Instance.Logs.Select(ToDto).ToList();
    }

    private static ItemDto ToDto(Item item)
    {
        return new ItemDto
        {
            Id = item.Id,
            Name = item.Name,
            Quantity = item.Quantity
        };
    }

    private static AuditLogDto ToDto(AuditLog log)
    {
        return new AuditLogDto
        {
            Action = log.Action,
            DateOfOccurence = log.DateOfOccurence
        };
    }

    public static void Reset()
    {
        Inventory.Instance.Items.Clear();
        AuditLogger.Instance.Logs.Clear();
    }
}