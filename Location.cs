
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


public class LocationManager
{
    public List<Location> Locations { get; set; } = new List<Location>();
    public LocationManager()
    {
        Locations.Add(new ForgottenGrove());
        Locations.Add(new AbandonedVillage());
        Locations.Add(new DarkCavern());
        Locations.Add(new BurndenRuins());
    }
}

public class LocationUnlocker
{
    // Odblokuje lokaci, ak hrac dosiahol potrebny level. Dokoncene.
    public bool UnlockLocation(Location location, Player player)
    {
        if (player.Level >= location.MinLevel)
        {
            location.IsUnlocked = true;
            Console.WriteLine($"You have unlocked the location: {location.Name}!");
            return true;
        }
        Console.WriteLine($"You need to reach level {location.MinLevel} to unlock this location.");
        return false;
    }
}

public class LocationSelector
{
    // Umozni hracovi vyberat lokacie, ktore su odblokovane. Dokoncene.
    public Location? SelectLocation(LocationManager locationManager, Player player)
    {
        var unlockedLocations = locationManager.Locations.Where(loc => loc.IsUnlocked).ToList();
        if (unlockedLocations.Count == 0)
        {
            Console.WriteLine("No locations are currently unlocked. Please level up to unlock new locations.");
            return null;
        }
        Console.WriteLine("Select a location to explore:");
        for (int i = 0; i < unlockedLocations.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {unlockedLocations[i].Name} - {unlockedLocations[i].Description}");
        }
        int selection;
        while (true)
        {
            Console.Write("Enter the number of the location you want to explore: ");
            if (int.TryParse(Console.ReadLine(), out selection) && selection >= 1 && selection <= unlockedLocations.Count)
            {
                return unlockedLocations[selection - 1];
            }
            Console.WriteLine("Invalid selection. Please try again.");
        }
    }
}

public class LocationItems
{
    // Vypise dostupne itemy v danej lokacii. Dokoncene.
    public void PrintLocationItems(Location location)
    {
        if (location.Items.Count == 0)
        {
            Console.WriteLine("There are no items available in this location.");
            return;
        }
        Console.WriteLine($"Items available in {location.Name}:");
        foreach (var item in location.Items)
        {
            Console.WriteLine($"- {item.Name} (Type: {item.Type}, Value: {item.Value}, Price: {item.Price} gold)");
        }
    }
}

public class LocationDropItems
{
    // Hodi sancu na drop itemu z lokacie po porazeni nepriatela. Dokoncene.
    public void DropLocationItem(Location location, Player player, Random random)
    {
        if (location.Items.Count == 0)
        {
            Console.WriteLine("There are no items to drop in this location.");
            return;
        }
        if (random.Next(0, 100) < 50) // 50% chance to drop an item
        {
            var droppedItem = location.Items[random.Next(location.Items.Count)];
            player.AddItemToInventory(droppedItem);
            Console.WriteLine($"You found an item: {droppedItem.Name}!");
        }
        else
        {
            Console.WriteLine("No items were found this time.");
        }
    }
}