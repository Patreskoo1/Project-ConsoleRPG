public class Item
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public ItemType Type { get; set; }
    public int Value { get; set; }
    public int Price { get; set; }

    public Item() { }

    public Item(string name, string description, ItemType type, int value, int price)
    {
        Name = name;
        Description = description;
        Type = type;
        Value = value;
        Price = price;
    }
}

public enum ItemType
{
    Weapon,
    Armor,
    Consumable,
    Accessory
}
