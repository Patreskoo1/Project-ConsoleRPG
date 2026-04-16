
public class Player : Character
{
    public PlayerClass SelectedClass { get; set; }
    public int Mana { get; set; }
    public int MaxMana { get; set; }
    public List<Ability> Abilities { get; set; } = new List<Ability>();
    public Item? EquippedWeapon { get; set; }
    public Item? EquippedArmor { get; set; }
    public Item? EquippedAccessory { get; set; }
    public int Experience { get; set; } = 0;
    public int Gold { get; set; } = 0;

    public void SetPlayerClass(PlayerClass playerClass)
    {
        SelectedClass = playerClass;

        BasePlayer classInstance = playerClass switch
        {
            PlayerClass.Soldier => new Soldier(),
            PlayerClass.Scholar => new Scholar(),
            PlayerClass.Rogue => new Rogue(),
            PlayerClass.Archer => new Archer(),
            _ => new Soldier()
        };

        var stats = classInstance.BaseStats;
        Strength = stats.Strength;
        Endurance = stats.Endurance;
        Agility = stats.Agility;
        Luck = stats.Luck;
        Abilities = classInstance.StartingAbilities;
        Mana = MaxMana = 50 + (Intelligence * 5);
    }

    // Nastavi zakladne staty hraca na zaciatku hry. Dokoncene.
    public Player()
    {
        Health = 100;
        MaxHealth = 100;
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
        player.Intelligence += 1;
        player.Strength += 2;
        player.Endurance += 2;
        player.Agility += 1;
        player.Luck += 1;
        player.MaxHealth = 100 + (player.Level - 1) * 20;
        player.Health = player.MaxHealth;
        player.MaxMana = 50 + (player.Intelligence * 5);
        player.Mana = player.MaxMana;
        Console.WriteLine($"Congratulations! You've reached level {player.Level}!");
        return true;
    }
}