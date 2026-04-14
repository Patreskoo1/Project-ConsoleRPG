public enum PlayerClass
{
    Warrior,
    Mage,
    Rogue,
    Archer,
}

public abstract class BasePlayer
{
    public abstract string Name { get; }
    public abstract string Description { get; }
    public abstract PlayerStats BaseStats { get; }
    public abstract List<Ability> StartingAbilities { get; }
}

public class PlayerStats
{
    public int Strength { get; set; }
    public int Intelligence { get; set; }
    public int Agility { get; set; }
    public int Endurance { get; set; }
    public int CriticalChance { get; set; }
    public int Luck { get; set; }
    public PlayerStats(int strength, int intelligence, int agility, int endurance, int luck)
    {
        Strength = strength;
        Intelligence = intelligence;
        Agility = agility;
        Endurance = endurance;
        Luck = luck;
    }
}


public class Warrior : BasePlayer
{
    public override string Name => "Warrior";
    public override string Description => "A strong and resilient fighter, excelling in melee combat.";
    public override PlayerStats BaseStats => new PlayerStats(strength: 10, intelligence: 3, agility: 5, endurance: 8, luck: 2);
    public override List<Ability> StartingAbilities => new List<Ability>
    {
        new Ability { Name = "Slash", Description = "A powerful melee attack.", ManaCost = 0, Damage = 15, Type = AbilityType.Attack },
        new Ability { Name = "Shield Block", Description = "Reduces incoming damage for a short time.", ManaCost = 5, Type = AbilityType.Buff },
        new Ability { Name = "War Cry", Description = "Increases attack power for a short time.", ManaCost = 10, Type = AbilityType.Buff }
    };
}

public class Mage : BasePlayer
{
    public override string Name => "Mage";
    public override string Description => "A master of arcane arts, capable of casting powerful spells.";
    public override PlayerStats BaseStats => new PlayerStats(strength: 3, intelligence: 10, agility: 5, endurance: 4, luck: 5);
    public override List<Ability> StartingAbilities => new List<Ability>
    {
        new Ability { Name = "Fireball", Description = "A fiery projectile that damages enemies.", ManaCost = 10, Damage = 20, Type = AbilityType.Attack },
        new Ability { Name = "Ice Shield", Description = "Creates a shield of ice that absorbs damage.", ManaCost = 8, Type = AbilityType.Buff },
        new Ability { Name = "Arcane Blast", Description = "A burst of arcane energy that damages and stuns enemies.", ManaCost = 12, Damage = 25, Type = AbilityType.Attack }
    };
}

public class Rogue : BasePlayer
{
    public override string Name => "Rogue";
    public override string Description => "A stealthy and agile fighter, excelling in quick attacks and evasion.";
    public override PlayerStats BaseStats => new PlayerStats(strength: 5, intelligence: 4, agility: 10, endurance: 4, luck: 4);
    public override List<Ability> StartingAbilities => new List<Ability>
    {
        new Ability { Name = "Backstab", Description = "A powerful attack that deals extra damage when hitting from behind.", ManaCost = 0, Damage = 20, Type = AbilityType.Attack },
        new Ability { Name = "Smoke Bomb", Description = "Creates a cloud of smoke that increases evasion for a short time.", ManaCost = 5, Type = AbilityType.Buff },
        new Ability { Name = "Poison Dagger", Description = "A dagger coated with poison that damages enemies over time.", ManaCost = 8, Damage = 10, Type = AbilityType.Attack }
    };
}

public class Archer : BasePlayer
{
    public override string Name => "Archer";
    public override string Description => "A skilled marksman, excelling in ranged combat and precision.";
    public override PlayerStats BaseStats => new PlayerStats(strength: 4, intelligence: 5, agility: 8, endurance: 5, luck: 3);
    public override List<Ability> StartingAbilities => new List<Ability>
    {
        new Ability { Name = "Arrow Shot", Description = "A basic ranged attack.", ManaCost = 0, Damage = 15, Type = AbilityType.Attack },
        new Ability { Name = "Eagle Eye", Description = "Increases accuracy and critical chance for a short time.", ManaCost = 5, Type = AbilityType.Buff },
        new Ability { Name = "Multi-Shot", Description = "Fires multiple arrows at once, hitting multiple enemies.", ManaCost = 10, Damage = 10, Type = AbilityType.Attack }
    };
}