// Hlavný program, ktorý spúšťa textovú RPG hru. Dokoncene.
using System.Linq;

int enemiesDefeated = 0;

Console.WriteLine("Hello, dear traveler!");
Console.WriteLine("1. Start a new adventure");
Console.WriteLine("2. Load saved adventure");
string choice = Console.ReadLine()?.Trim() ?? "1";
Player? player;

if (choice == "2")
{
	player = GameStateManager.LoadGame();
	if (player == null)
	{
		Console.WriteLine("Starting a new adventure instead.");
		Console.Write("What is your name, adventurer? ");
		string name = Console.ReadLine()?.Trim() ?? "Adventurer";
		player = new Player { Name = name };
	}
}
else
{
	Console.Write("What is your name, adventurer? ");
	string name = Console.ReadLine()?.Trim() ?? "Adventurer";
	player = new Player { Name = name };
}

Console.WriteLine($"Welcome, {player.Name}! Your adventure begins now.");


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

Enemy GetBossForLocation(Location location)
{
	return location.Name switch
	{
		"ForgottenGrove" => new Bosses.ForgottenGroveBoss(),
		"AbandonedVillage" => new Bosses.AbandonedVillageBoss(),
		"DarkCavern" => new Bosses.DarkCavernBoss(),
		"BurndenRuins" => new Bosses.BurndenRuinsBoss(),
		"FrozenPeak" => new Bosses.FrozenPeaksBoss(),
		_ => new Bosses.ForgottenGroveBoss()
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
		Location currentLocation = new LocationSelector().SelectLocation(new LocationManager(), currentPlayer) ?? new ForgottenGrove();
		Enemy currentEnemy = RandomEnemy(currentLocation, rng);

		Console.WriteLine("Do you wish to fight the enemy? (yes/no)");
		if (!IsYes(Console.ReadLine()))
		{
			Console.WriteLine("You choose to end your adventure. Farewell, traveler!");
			break;
		}

		Console.WriteLine("You engage in battle!");
		BattleResult result = RunBattle(currentPlayer, currentEnemy, rng, currentLocation);

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
BattleResult RunBattle(Player currentPlayer, Enemy currentEnemy, Random rng, Location currentLocation)
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
			HandleEnemyDefeat(currentPlayer, currentEnemy, rng, currentLocation);
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
void HandleEnemyDefeat(Player currentPlayer, Enemy currentEnemy, Random rng, Location currentLocation)
{
	currentEnemy.Health = 0;
	Console.WriteLine($"You have defeated the {currentEnemy.Name}!");
	enemiesDefeated++;
	currentPlayer.Experience += currentEnemy.XpReward;
	currentPlayer.Gold += currentEnemy.GoldReward;
	Item? droppedItem = currentEnemy.GenerateRandomItemDrop(rng, currentLocation);
	if (droppedItem != null)
	{
		currentPlayer.AddItemToInventory(droppedItem);
		Console.WriteLine($"The {currentEnemy.Name} dropped an item: {droppedItem.Name}!");
	}

    Console.WriteLine($"You gain {currentEnemy.XpReward} XP and {currentEnemy.GoldReward} gold.");
    Console.WriteLine($"Your current XP: {currentPlayer.Experience}, Gold: {currentPlayer.Gold}");
    if (currentPlayer.Experience >= currentPlayer.Level * 50)
    {
        PlayerLevelUp levelUpHandler = new PlayerLevelUp();
        levelUpHandler.LevelUpPlayer(currentPlayer);
    }

    if (enemiesDefeated == 3)
	{
		Console.WriteLine("You have defeated enough enemies to fight the boss of this location!");
		Console.WriteLine("Do you wish to fight the boss? (yes/no)");
		Console.WriteLine("Warning: Boss fights are very difficult and may result in your defeat. Make sure you are well prepared before accepting the challenge.");

		if (!IsYes(Console.ReadLine()))
		{
			Console.WriteLine("You chose not to fight the boss right now. You can continue exploring the location and fight the boss later when you feel more prepared.");
			Console.WriteLine("Remember, you can only fight the boss after defeating 3 enemies in the location. Keep going and good luck!");
			enemiesDefeated = 0; // Reset enemy count to allow fighting the boss later
		}

		else
		{
			Enemy Boss = GetBossForLocation(currentLocation);
            Console.WriteLine($"You have chosen to fight the boss: {Boss.Name}!");
			Console.WriteLine($"{Boss.Description}");
			BattleResult bossResult = RunBattle(currentPlayer, Boss , rng , currentLocation);

            if (bossResult == BattleResult.EnemyDefeated)
			{
				Console.WriteLine($"Congratulations! You have defeated the boss {Boss.Name} and completed the adventure in this location!");
				currentPlayer.Experience += 100; // Additional XP for defeating the boss
				currentPlayer.Gold += 200; // Additional gold for defeating the boss
				return; // End the adventure after defeating the boss
			}
			else
			{
				Console.WriteLine($"You were defeated by the boss {Boss.Name}. Better luck next time!");
				return; // End the adventure after being defeated by the boss
			}
		}
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
		Console.WriteLine("6. Continue exploring the location");
		Console.WriteLine("7. Quit the adventure");
		Console.WriteLine("8. DEBUG: Give gold, potion, weapon");
		Console.WriteLine("9. Save and quit.");

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
			Console.WriteLine("You continue exploring the location, looking for more enemies to fight and treasures to find. Good luck, traveler!");
			return true;
		}

        if (response == "7")
		{
			return false;
		}

		if (response == "8")
		{
			currentPlayer.Gold += 500;
			currentPlayer.Inventory.Add(new Item { Name = "Debug Sword", Type = ItemType.Weapon, Value = 99, Price = 1 });
			currentPlayer.Inventory.Add(new Item { Name = "Debug Potion", Type = ItemType.Consumable, Value = 100, Price = 1 });
			Console.WriteLine("DEBUG: Added 500 gold, Debug Sword, Debug Potion.");
			return true;
		}
		if (response == "9")
		{
			GameStateManager.SaveGame(currentPlayer);
			Console.WriteLine("Game saved. Goodbye, traveler!");
			return false;
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

