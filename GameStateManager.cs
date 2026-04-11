using System.Text.Json;


public class SaveData
{
    public string? Name { get; set; }
    public int Level { get; set; }
    public int Health { get; set; }
    public int Experience { get; set; } 
    public int Gold { get; set; }
    public int AttackPower { get; set; }
    public int Defense { get; set; }
    public int CriticalChance { get; set; }
    public List<Item> Inventory { get; set; } = new List<Item>();
    public Item? EquippedWeapon { get; set; }
    public Item? EquippedArmor { get; set; }
    public Item? EquippedAccessory { get; set; }
}

public static class GameStateManager
{
    public static void SaveGame(Player player)
    {
        try
        {
            SaveData savedata = new SaveData
            {
                Name = player.Name,
                Level = player.Level,
                Health = player.Health,
                Experience = player.Experience,
                Gold = player.Gold,
                AttackPower = player.AttackPower,
                Defense = player.Defense,
                CriticalChance = player.CriticalChance,
                Inventory = player.Inventory,
                EquippedWeapon = player.EquippedWeapon,
                EquippedArmor = player.EquippedArmor,
                EquippedAccessory = player.EquippedAccessory
            };

            string json = JsonSerializer.Serialize(savedata, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText("savegame.json", json);
            Console.WriteLine("Game saved successfully!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saving game: {ex.Message}");
        }

    }

    public static Player? LoadGame()
    {
        try
        {
            if (!File.Exists("savegame.json"))
            {
                Console.WriteLine("No save file found.");
                return null;
            }
            string json = File.ReadAllText("savegame.json");
            SaveData? savedata = JsonSerializer.Deserialize<SaveData>(json);

            if (savedata == null)
            {
                Console.WriteLine("Failed to load save data.");
                return null;
            }
            Player player = new Player
            {
                Name = savedata.Name,
                Level = savedata.Level,
                Health = savedata.Health,
                Experience = savedata.Experience,
                Gold = savedata.Gold,
                AttackPower = savedata.AttackPower,
                Defense = savedata.Defense,
                CriticalChance = savedata.CriticalChance,
                Inventory = savedata.Inventory,
                EquippedWeapon = savedata.EquippedWeapon,
                EquippedArmor = savedata.EquippedArmor,
                EquippedAccessory = savedata.EquippedAccessory,
            };
            Console.WriteLine("Game loaded successfully!");
            return player;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading game: {ex.Message}");
            return null;
        }
    }
}

