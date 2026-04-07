// Hlavný program, ktorý spúšťa textovú RPG hru. Dokoncene.
using System.Numerics;

Console.WriteLine("Hello, dear traveler!");
Console.WriteLine("What is your name?");
string name = Console.ReadLine() ?? "Traveler";
Console.WriteLine($"Welcome, {name}! Your adventure begins now.");

Player player = new Player { Name = name };
Random random = new Random();

Console.WriteLine("Do you wish to look at your stats? (yes/no)");
if (IsYes(Console.ReadLine()))
{
    PrintPlayerStats(player);
}
else
{
    Console.WriteLine("Very well, let the adventure continue!");
}

RunAdventure(player, random);

// Vyhodnoti odpoved ano/yes pre jednoduche rozhodnutia. Dokoncene.
bool IsYes(string? input)
{
    string normalized = (input ?? string.Empty).Trim().ToLowerInvariant();
    return normalized is "yes" or "ano";
}

// Vyberie Enemy y lokacie, s ktorou sa hrac stretne. Dokoncene.
Enemy RandomEnemy(Location location, Random rng)
{
    if (location.EnemyTypes.Count == 0)
    {
        throw new InvalidOperationException("Location has no enemy types defined.");
    }
    int index = rng.Next(location.EnemyTypes.Count);
    return location.EnemyTypes[index]();
}


// Nahodne vygeneruje predmet (loot), ktory moze padnut po boji. Dokoncene.
Item GenerateRandomItem(Random rng)
{
    return rng.Next(0, 4) switch
    {
        0 => new Item { Name = "Small Health Potion", Type = ItemType.Consumable, Value = 30, Price = 15 },
        1 => new Item { Name = "Iron Sword", Type = ItemType.Weapon, Value = 8, Price = 45 },
        2 => new Item { Name = "Leather Armor", Type = ItemType.Armor, Value = 6, Price = 40 },
        _ => new Item { Name = "Lucky Ring", Type = ItemType.Accessory, Value = 4, Price = 60 },
    };
}

// Vypise aktualne statistiky hraca na konzolu. Dokoncene.
void PrintPlayerStats(Player currentPlayer)
{
    Console.WriteLine($"Name: {currentPlayer.Name}");
    Console.WriteLine($"Health: {currentPlayer.Health}");
    Console.WriteLine($"Level: {currentPlayer.Level}");
    Console.WriteLine($"Experience: {currentPlayer.Experience}");
    Console.WriteLine($"Attack Power: {currentPlayer.AttackPower}");
    Console.WriteLine($"Defense: {currentPlayer.Defense}");
    Console.WriteLine($"Gold: {currentPlayer.Gold}");
}

// Riadi hlavny cyklus dobrodruzstva (stretnutie, boj, pokracovanie). Dokoncene.
void RunAdventure(Player currentPlayer, Random rng)
{
    while (currentPlayer.Health > 0)
    {
        Console.WriteLine("You encounter a wild enemy!");
        Enemy currentEnemy = RandomEnemy(new ForgottenGrove(), rng);

        Console.WriteLine("Do you wish to fight the enemy? (yes/no)");
        if (!IsYes(Console.ReadLine()))
        {
            Console.WriteLine("You choose to end your adventure. Farewell, traveler!");
            break;
        }

        Console.WriteLine("You engage in battle!");
        BattleResult result = RunBattle(currentPlayer, currentEnemy, rng);

        if (result is BattleResult.PlayerDefeated or BattleResult.PlayerRanAway)
        {
            break;
        }

        if (!HandleAdventureChoice(currentPlayer))
        {
            Console.WriteLine("You decide to end your adventure here. Farewell, traveler!");
            break;
        }
    }
}

// Spracuje cely boj medzi hracom a jednym nepriatelom. Dokoncene.
BattleResult RunBattle(Player currentPlayer, Enemy currentEnemy, Random rng)
{
    while (currentPlayer.Health > 0 && currentEnemy.Health > 0)
    {
        if (!TryPlayerAttack(currentPlayer, currentEnemy, rng))
        {
            Console.WriteLine("You ran away from the fight!");
            return BattleResult.PlayerRanAway;
        }

        if (currentEnemy.Health <= 0)
        {
            HandleEnemyDefeat(currentPlayer, currentEnemy, rng);
            return BattleResult.EnemyDefeated;
        }

        if (!ResolveEnemyAttack(currentPlayer, currentEnemy, rng))
        {
            return BattleResult.PlayerDefeated;
        }
    }

    return currentPlayer.Health <= 0 ? BattleResult.PlayerDefeated : BattleResult.EnemyDefeated;
}

// Vykona utok hraca, alebo vrati false ak sa hrac rozhodol utiect. Dokoncene.
bool TryPlayerAttack(Player currentPlayer, Enemy currentEnemy, Random rng)
{
    Console.WriteLine("Do you wish to attack? (yes/no)");
    if (!IsYes(Console.ReadLine()))
    {
        return false;
    }

    Console.WriteLine("You choose to attack the enemy!");
    int playerDamage = currentPlayer.CalculateDamageAgainst(currentEnemy, rng, out bool criticalHit);
    if (criticalHit)
    {
        Console.WriteLine("Critical hit! You deal extra damage!");
    }

    currentEnemy.Health -= playerDamage;
    Console.WriteLine($"You deal {playerDamage} damage to the {currentEnemy.Name}. Enemy health is now {currentEnemy.Health}.");
    return true;
}

// Vykona utok nepriatela na hraca a skontroluje porazku. Dokoncene.
bool ResolveEnemyAttack(Player currentPlayer, Enemy currentEnemy, Random rng)
{
    int enemyDamage = currentEnemy.CalculateDamageAgainst(currentPlayer, rng, out bool enemyCriticalHit);
    if (enemyCriticalHit)
    {
        Console.WriteLine($"The {currentEnemy.Name} lands a critical hit! It deals extra damage!");
    }

    currentPlayer.Health -= enemyDamage;
    Console.WriteLine($"The {currentEnemy.Name} deals {enemyDamage} damage to you. Your health is now {currentPlayer.Health}.");

    if (currentPlayer.Health <= 0)
    {
        currentPlayer.Health = 0;
        Console.WriteLine("You have been defeated! Game over.");
        return false;
    }

    return true;
}

// Prideli odmeny za porazenie nepriatela (XP + zlato). Dokoncene.
void HandleEnemyDefeat(Player currentPlayer, Enemy currentEnemy, Random rng)
{
    currentEnemy.Health = 0;
    Console.WriteLine($"You have defeated the {currentEnemy.Name}!");
    currentPlayer.Experience += currentEnemy.XpReward;
    currentPlayer.Gold += currentEnemy.GoldReward;
    if (currentEnemy.GenerateRandomItemDrop(rng))
    {
        Item droppedItem = GenerateRandomItem(rng);
        currentPlayer.AddItemToInventory(droppedItem);
    }
    Console.WriteLine($"You gain {currentEnemy.XpReward} XP and {currentEnemy.GoldReward} gold.");
    Console.WriteLine($"Your current XP: {currentPlayer.Experience}, Gold: {currentPlayer.Gold}");
    if (currentPlayer.Experience >= currentPlayer.Level * 50)
    {
        PlayerLevelUp levelUpHandler = new PlayerLevelUp();
        levelUpHandler.LevelUpPlayer(currentPlayer);
    }
}

// Po boji vyriesi dalsiu volbu hraca (pokracovat, obchod, koniec). Dokoncene.
bool HandleAdventureChoice(Player currentPlayer)
{
    Console.WriteLine("Do you wish to continue your adventure? (yes/no)");
    if (!IsYes(Console.ReadLine()))
    {
        return false;
    }

    Console.WriteLine("You chose to continue your adventure! Nice choice, traveler! You can choose from this options :");
    Console.WriteLine("1. Visit the shop.");
    Console.WriteLine("2. View your stats");
    Console.WriteLine("3. Check your inventory");
    Console.WriteLine("4. Equip an item");
    Console.WriteLine("5. Use a health potion");
    Console.WriteLine("6. Quit the adventure");
    Console.WriteLine("7. DEBUG: Give gold, potion, weapon");

    string response = (Console.ReadLine() ?? "3").Trim();
    if (response == "1")
    {
        Shop.OpenShop(currentPlayer);
        return true;
    }

    if (response == "2")
    {
        PrintPlayerStats(currentPlayer);
        return true;
    }

    if (response == "3")
    {
        currentPlayer.PrintInventory();
        Console.WriteLine("Do you wish to go back to options menu? (yes/no)");
        if (IsYes(Console.ReadLine()))
        {
            return HandleAdventureChoice(currentPlayer);
        }
        return true;
    }

    if (response == "4")
    {
        TryEquipItemFromInventory(currentPlayer);
        return true;
    }

    if (response == "5")
    {
        TryUseConsumableItem(currentPlayer);
        return true;
    }

    if (response == "6")
    {
        return false;
    }

    if (response == "7")
    {
        currentPlayer.Gold += 500;
        currentPlayer.Inventory.Add(new Item { Name = "Debug Sword", Type = ItemType.Weapon, Value = 99, Price = 1 });
        currentPlayer.Inventory.Add(new Item { Name = "Debug Potion", Type = ItemType.Consumable, Value = 100, Price = 1 });
        Console.WriteLine("DEBUG: Added 500 gold, Debug Sword, Debug Potion.");
        return true;
    }

    Console.WriteLine("Invalid choice. Please try again.");
    return true;
}

// Zobrazi inventar a necha hraca vybrat item na equipnutie.
void TryEquipItemFromInventory(Player currentPlayer)
{
    if (currentPlayer.Inventory.Count == 0)
    {
        Console.WriteLine("Your inventory is empty.");
        return;
    }

    Console.WriteLine("Your Inventory:");
    for (int i = 0; i < currentPlayer.Inventory.Count; i++)
    {
        var it = currentPlayer.Inventory[i];
        Console.WriteLine($"{i + 1}. {it.Name} (Type: {it.Type}, Value: {it.Value})");
    }

    Console.WriteLine("Enter the number of the item to equip (or 0 to cancel):");
    if (int.TryParse(Console.ReadLine(), out int choice) && choice >= 1 && choice <= currentPlayer.Inventory.Count)
    {
        EquipmentManager equipManager = new EquipmentManager();
        equipManager.EquipItem(currentPlayer, currentPlayer.Inventory[choice - 1]);
    }
    else
    {
        Console.WriteLine("Cancelled.");
    }
}

void TryUseConsumableItem(Player currentPlayer)
{
    var consumables = currentPlayer.Inventory.Where(i => i.Type == ItemType.Consumable).ToList();
    if (consumables.Count == 0)
    {
        Console.WriteLine("You have no consumable items in your inventory.");
        return;
    }

    Console.WriteLine("Your Consumable Items:");
    for (int i = 0; i < consumables.Count; i++)
    {
        var it = consumables[i];
        Console.WriteLine($"{i + 1}. {it.Name} (Value: {it.Value})");
    }

    Console.WriteLine("Enter the number of the item to use (or 0 to cancel):");
    if (int.TryParse(Console.ReadLine(), out int choice) && choice >= 1 && choice <= consumables.Count)
    {
        Item selectedItem = consumables[choice - 1];
        if (selectedItem.Name?.Contains("Health Potion") == true)
        {
            currentPlayer.Health = Math.Min(currentPlayer.Health + selectedItem.Value, 100 + (currentPlayer.Level - 1) * 20);
            currentPlayer.Inventory.Remove(selectedItem);
            Console.WriteLine($"You used {selectedItem.Name} and restored {selectedItem.Value} health. Current health: {currentPlayer.Health}");
        }
        else
        {
            Console.WriteLine("This item cannot be used right now.");
        }
    }
    else
    {
        Console.WriteLine("Cancelled.");
    }
}
enum BattleResult
{
    EnemyDefeated,
    PlayerDefeated,
    PlayerRanAway,
}

