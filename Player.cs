using Project_RPG;

public class Player : Character
{
    public Item? EquippedWeapon { get; set; }
    public Item? EquippedArmor { get; set; }
    public Item? EquippedAccessory { get; set; }
    public int Experience { get; set; } = 0;
    public int Gold { get; set; } = 0;

    // Nastavi zakladne staty hraca na zaciatku hry. Dokoncene.
    public Player()
    {
        AttackPower = 25;
        Defense = 5;
        CriticalChance = 10;
    }

    public List<Item> Inventory { get; set; } = new List<Item>();

    // Prida item do inventara hraca. Dokoncene.
    public void AddItemToInventory(Item item)
    {
        Inventory.Add(item);
        Console.WriteLine($"You have obtained: {item.Name ?? "Unknown"} (Type: {item.Type}, Value: {item.Value}, Price: {item.Price} gold)");
    }

    // Vypise obsah inventara hraca. Dokoncene.
    public void PrintInventory()
    {
        if (Inventory.Count == 0)
        {
            Console.WriteLine("Your inventory is empty.");
            return;
        }

        Console.WriteLine("Your Inventory:");
        foreach (var item in Inventory)
        {
            Console.WriteLine($"- {item.Name} (Type: {item.Type}, Value: {item.Value}, Price: {item.Price} gold)");
        }
    }
}


public class PlayerLevelUp
{
    // Zvysi level hraca a vylepsi jeho staty. Dokoncene.
    public bool LevelUpPlayer(Player player)
    {
        int requiredExperience = player.Level * 50;
        if (player.Experience < requiredExperience)
        {
            return false;
        }
        player.Experience -= requiredExperience;
        player.Level++;
        player.AttackPower += 5;
        player.Defense += 2;
        player.CriticalChance += 2;
        player.Health = 100 + (player.Level - 1) * 20;
        Console.WriteLine($"Congratulations! You've reached level {player.Level}!");
        return true;
    }
}

public class EquipmentManager
{
    // Vybavi hraca zbranou, brnenim alebo doplnkom. Dokoncene.
    public void EquipItem(Player player, Item item)
    {
        if (!player.Inventory.Contains(item))
        {
            Console.WriteLine("You don't have that item in your inventory.");
            return;
        }

        switch (item.Type)
        {
            case ItemType.Weapon:
                if (player.EquippedWeapon != null)
                {
                    player.AttackPower -= player.EquippedWeapon.Value;
                    player.Inventory.Add(player.EquippedWeapon);
                }
                player.Inventory.Remove(item);
                player.EquippedWeapon = item;
                player.AttackPower += item.Value;
                Console.WriteLine($"You have equipped: {item.Name} (Attack Power: +{item.Value})");
                break;
            case ItemType.Armor:
                if (player.EquippedArmor != null)
                {
                    player.Defense -= player.EquippedArmor.Value;
                    player.Inventory.Add(player.EquippedArmor);
                }
                player.Inventory.Remove(item);
                player.EquippedArmor = item;
                player.Defense += item.Value;
                Console.WriteLine($"You have equipped: {item.Name} (Defense: +{item.Value})");
                break;
            case ItemType.Accessory:
                if (player.EquippedAccessory != null)
                {
                    player.CriticalChance -= player.EquippedAccessory.Value;
                    player.Inventory.Add(player.EquippedAccessory);
                }
                player.Inventory.Remove(item);
                player.EquippedAccessory = item;
                player.CriticalChance += item.Value;
                Console.WriteLine($"You have equipped: {item.Name} (Critical Chance: +{item.Value}%)");
                break;
            default:
                Console.WriteLine("This item cannot be equipped.");
                break;
        }
    }
}

public class UseItemManager
{
    // Pouzije konzumovatelny item z inventara hraca. Dokoncene.
    public void UseConsumableItem(Player player, Item item)
    {
        if (!player.Inventory.Contains(item))
        {
            Console.WriteLine("You don't have that item in your inventory.");
            return;
        }

        if (item.Type != ItemType.Consumable)
        {
            Console.WriteLine("This item cannot be used.");
            return;
        }

        player.Inventory.Remove(item);
        player.Health = Math.Min(player.Health + item.Value, 100 + (player.Level - 1) * 20);
        Console.WriteLine($"You used: {item.Name} and restored {item.Value} health!");
    }
}




