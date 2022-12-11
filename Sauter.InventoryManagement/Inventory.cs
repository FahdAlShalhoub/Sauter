namespace Sauter.InventoryManagement;

internal class Inventory
{
    private static Inventory? instance;
    internal Dictionary<Guid, Item> Items { get; }

    private static readonly object padlock = new();

    Inventory()
    {
        Items = new Dictionary<Guid, Item>();
    }

    public static Inventory Instance
    {
        get
        {
            lock (padlock)
            {
                return instance ??= new Inventory();
            }
        }
    }
}

internal class Item
{
    public Guid Id { get; set; }
    public string Name { get; set; } 
    public int Quantity { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}

public struct ItemDto
{
    public Guid Id { get; init; }
    public string Name { get; init; }
    public int Quantity { get; init; }
}