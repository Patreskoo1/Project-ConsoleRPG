
public enum BattleResult 
{
    EnemyDefeated,
    PlayerDefeated,
    PlayerRanAway
}

public class CombatSystem
{
    private Random _random = new Random();
    private Dictionary<string, int> _abilityCooldowns = new();
    private Dictionary<string, int> _activeBuffs = new();
    private Dictionary<string, int> _buffAmounts = new();

    public BattleResult RunBattle(Player player, Enemy enemy)
    {
        _abilityCooldowns.Clear();
        _activeBuffs.Clear();
        _buffAmounts.Clear();

        StoryText.WriteLineColored($"\n⚔️  A {enemy.Name} appears!", ConsoleColor.Red);

        while (player.Health > 0 && enemy.Health > 0)
        {
            Console.WriteLine($"\n[YOUR HP: {player.Health}/{player.MaxHealth}] [MANA: {player.Mana}/{player.MaxMana}]");
            Console.WriteLine($"[{enemy.Name} HP: {enemy.Health}/{enemy.MaxHealth}]");

            bool playerActed = PlayerTurn(player, enemy);
            if (!playerActed)
                return BattleResult.PlayerRanAway;

            if (enemy.Health <= 0)
                return BattleResult.EnemyDefeated;

            EnemyTurn(player, enemy);

            if (player.Health <= 0)
                return BattleResult.PlayerDefeated;

            TickBuffs(player);
            TickCooldowns();
        }
        return BattleResult.EnemyDefeated; // Should never reach here, but just in case
    }

    private bool PlayerTurn(Player player, Enemy enemy)
    {
        Console.WriteLine("\nWhat will you do?");
        Console.WriteLine("[1] Attack");
        Console.WriteLine("[2] Use Ability");
        Console.WriteLine("[3] Use Item");
        Console.WriteLine("[4] Run Away");

        string? input = Console.ReadLine();

        switch (input)
        {
            case "1":
                int damage = CalculatePlayerDamage(player);
                enemy.Health -= damage;
                StoryText.WriteLineColored($"You attack {enemy.Name} for {damage} damage!", ConsoleColor.Yellow);
                return true;

            case "2":
                return UseAbility(player, enemy);

            case "3":
                UseItem(player);
                return true;

            case "4":
                return TryRunAway(player, enemy);

            default:
                Console.WriteLine("Invalid input, you lose your turn!");
                return true;
        }
    }

    private void EnemyTurn(Player player, Enemy enemy)
    {
        int damage = CalculateEnemyDamage(player, enemy);
        if (damage == 0)
            StoryText.WriteLineColored($"{enemy.Name}'s attack missed!", ConsoleColor.Cyan);
        else
            StoryText.WriteLineColored($"{enemy.Name} attacks you for {damage} damage!", ConsoleColor.Red);
    }

    private int CalculatePlayerDamage(Player player)
    {
        bool isCrit = _random.NextDouble() < player.CritChance;
        int damage = player.PhysicalDamage;
        if (isCrit)
        {
            damage *= 2;
            StoryText.WriteLineColored("💥 Critical hit!", ConsoleColor.Magenta);
        }
        return damage;
    }

    private int CalculateEnemyDamage(Player player, Enemy enemy)
    {
        // Dodge check
        bool dodged = _random.NextDouble() < player.DodgeChance;
        if (dodged) return 0;

        int damage = Math.Max(0, enemy.PhysicalDamage - player.Armor);
        player.Health -= damage;
        return damage;
    }

    private bool TryRunAway(Player player, Enemy enemy)
    {
        int chance = player.Agility - enemy.Agility;
        int roll = _random.Next(0, 20);
        if (roll + chance > 10)
        {
            StoryText.WriteLineColored("You successfully ran away!", ConsoleColor.Green);
            return false;
        }
        StoryText.WriteLineColored("You failed to run away!", ConsoleColor.Red);
        return true;
    }

    private bool UseAbility(Player player, Enemy enemy)
    {
        var abilities = player.Abilities;
        if (abilities == null || abilities.Count == 0)
        {
            Console.WriteLine("You have no abilities!");
            return true;
        }

        Console.WriteLine("\nChoose ability:");
        for (int i = 0; i < abilities.Count; i++)
        {
            var ab = abilities[i];
            bool onCooldown = _abilityCooldowns.ContainsKey(ab.Name!) && _abilityCooldowns[ab.Name!] > 0;
            string cdText = onCooldown ? $" [CD: {_abilityCooldowns[ab.Name!]}]" : "";
            Console.WriteLine($"[{i + 1}] {ab.Name} - {ab.Description} (Mana: {ab.ManaCost}){cdText}");
        }
        Console.WriteLine("[0] Back");

        if (!int.TryParse(Console.ReadLine(), out int choice) || choice == 0)
            return PlayerTurn(player, enemy);

        if (choice < 1 || choice > abilities.Count)
        {
            Console.WriteLine("Invalid choice!");
            return PlayerTurn(player, enemy);
        }

        var ability = abilities[choice - 1];

        if (_abilityCooldowns.ContainsKey(ability.Name!) && _abilityCooldowns[ability.Name!] > 0)
        {
            StoryText.WriteLineColored($"{ability.Name} is on cooldown for {_abilityCooldowns[ability.Name!]} more turns!", ConsoleColor.Red);
            return PlayerTurn(player, enemy);
        }

        if (player.Mana < ability.ManaCost)
        {
            StoryText.WriteLineColored("Not enough mana!", ConsoleColor.Red);
            return PlayerTurn(player, enemy);
        }

        player.Mana -= ability.ManaCost;
        if (ability.Cooldown > 0)
            _abilityCooldowns[ability.Name!] = ability.Cooldown;

        switch (ability.Type)
        {
            case AbilityType.Attack:
                bool isCrit = _random.NextDouble() < player.CritChance;
                int dmg = isCrit ? ability.Damage * 2 : ability.Damage;
                if (isCrit) StoryText.WriteLineColored("💥 Critical hit!", ConsoleColor.Magenta);
                enemy.Health -= dmg;
                StoryText.WriteLineColored($"You use {ability.Name} for {dmg} damage!", ConsoleColor.Yellow);
                break;

            case AbilityType.Heal:
                int healed = Math.Min(ability.Healing, player.MaxHealth - player.Health);
                player.Health += healed;
                StoryText.WriteLineColored($"You use {ability.Name} and heal {healed} HP!", ConsoleColor.Green);
                break;

            case AbilityType.Buff:
                ApplyBuff(player, ability);
                break;
        }

        return true;
    }

    private void ApplyBuff(Player player, Ability ability)
    {
        if (ability.BuffStat == null) return;

        // Ak buff už beží, resetuj duration
        _activeBuffs[ability.BuffStat] = ability.BuffDuration;
        _buffAmounts[ability.BuffStat] = ability.BuffAmount;

        switch (ability.BuffStat)
        {
            case "Strength": player.Strength += ability.BuffAmount; break;
            case "Endurance": player.Endurance += ability.BuffAmount; break;
            case "Agility": player.Agility += ability.BuffAmount; break;
            case "Luck": player.Luck += ability.BuffAmount; break;
        }

        StoryText.WriteLineColored($"You use {ability.Name}! {ability.BuffStat} +{ability.BuffAmount} for {ability.BuffDuration} turns!", ConsoleColor.Cyan);
    }

    private void TickBuffs(Player player)
    {
        var expired = new List<string>();
        foreach (var buff in _activeBuffs.Keys.ToList())
        {
            _activeBuffs[buff]--;
            if (_activeBuffs[buff] <= 0)
                expired.Add(buff);
        }

        foreach (var stat in expired)
        {
            int amount = _buffAmounts[stat];
            switch (stat)
            {
                case "Strength": player.Strength -= amount; break;
                case "Endurance": player.Endurance -= amount; break;
                case "Agility": player.Agility -= amount; break;
                case "Luck": player.Luck -= amount; break;
            }
            _activeBuffs.Remove(stat);
            _buffAmounts.Remove(stat);
            StoryText.WriteLineColored($"{stat} buff expired!", ConsoleColor.DarkGray);
        }
    }

    private void TickCooldowns()
    {
        foreach (var key in _abilityCooldowns.Keys.ToList())
            if (_abilityCooldowns[key] > 0)
                _abilityCooldowns[key]--;
    }

    private void UseItem(Player player)
    {
        var consumables = player.Inventory.Where(i => i.Type == ItemType.Consumable).ToList();
        if (consumables.Count == 0)
        {
            Console.WriteLine("No consumable items!");
            return;
        }

        Console.WriteLine("\nChoose item:");
        for (int i = 0; i < consumables.Count; i++)
            Console.WriteLine($"[{i + 1}] {consumables[i].Name} - {consumables[i].Description}");

        if (!int.TryParse(Console.ReadLine(), out int choice) || choice < 1 || choice > consumables.Count)
        {
            Console.WriteLine("Invalid choice!");
            return;
        }

        var item = consumables[choice - 1];
        player.Health = Math.Min(player.MaxHealth, player.Health + item.Value);
        player.Inventory.Remove(item);
        StoryText.WriteLineColored($"You use {item.Name} and restore {item.Value} HP!", ConsoleColor.Green);
    }

}