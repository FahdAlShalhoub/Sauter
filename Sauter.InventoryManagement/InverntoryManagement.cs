namespace Sauter.InventoryManagement;

public class InventoryManagement
{
    public static string StoreItem(string itemName, int quantity)
    {
        Item item = new()
        {
            Id = Guid.NewGuid(),
            Name = itemName,
            Quantity = quantity
        };
        
        Inventory.Instance.Items.Add(item.Id, item);

        return item.Id.ToString();
    }

    public static ItemDto GetItem(string itemIdentifier)
    {
        var item = Inventory.Instance.Items[Guid.Parse(itemIdentifier)];

        return new ItemDto
        {
            Id = item.Id,
            Name = item.Name,
            Quantity = item.Quantity
        };
    }

    public static void IncrementItem(string itemIdentifier, int incrementAmount)
    {
        var item = Inventory.Instance.Items[Guid.Parse(itemIdentifier)];
        item.Quantity += incrementAmount;
    }

    public static void DecrementItem(string itemIdentifier, int decrementAmount)
    {
        var item = Inventory.Instance.Items[Guid.Parse(itemIdentifier)];
        item.Quantity -= decrementAmount;
    }
}

public struct ItemDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } 
    public int Quantity { get; set; }
}