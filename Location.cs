using Project_RPG;

public class Location
{
    public string Name { get; set; }
    public string Description { get; set; }
    public int MinLevel { get; set; }
    public int MaxLevel { get; set; }
    public bool IsUnlocked { get; set; }
    public List<Item> Items { get; set; }
    public List<Func<Enemy>> EnemyTypes { get; set; } = new List<Func<Enemy>>();

    public Location(string name, string description)
    {
        Name = name;
        Description = description;
        Items = new List<Item>();
    }

    public Location(string description)
    {
        Name = GetType().Name;
        Description = description;
        Items = new List<Item>();
    }
}
