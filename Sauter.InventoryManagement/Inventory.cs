namespace Sauter.InventoryManagement;

internal class Inventory
{
    private static Inventory? _instance;
    internal Dictionary<Guid, Item> Items { get; }

    private static readonly object Padlock = new();

    Inventory()
    {
        Items = new Dictionary<Guid, Item>();
    }

    public static Inventory Instance
    {
        get
        {
            lock (Padlock)
            {
                return _instance ??= new Inventory();
            }
        }
    }
}

internal class Item
{
    public Guid Id { get; init; }
    public string Name { get; init; }
    public int Quantity { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    internal ItemDto ToDto => new()
    {
        Id = Id,
        Name = Name,
        Quantity = Quantity
    };
}

public struct ItemDto
{
    public Guid Id { get; init; }
    public string Name { get; init; }
    public int Quantity { get; init; }
}