using Project_RPG;

public class ForgottenGrove : Location
{
    public ForgottenGrove() : base("A dense forest covered in mist where travelers often lose their way. The silence is broken only by strange rustling sounds.")
    {
        MinLevel = 1;
        MaxLevel = 3;
        IsUnlocked = true;
        Items.Add(new Item("Old Sword", "A rusty old sword that has seen better days. It has a faint magical aura.", ItemType.Weapon, 5, 20));
        Items.Add(new Item("Leather Armor", "Basic armor made of leather. Provides minimal protection.", ItemType.Armor, 3, 15));
        Items.Add(new Item("Health Potion", "A small vial containing a red liquid that restores health when consumed.", ItemType.Consumable, 20, 10));
        EnemyTypes.Add(() => new Enemy("Forest Wolf", 30, 5, 2));
        EnemyTypes.Add(() => new Enemy("Lost Spirit", 20, 3, 1));
        EnemyTypes.Add(() => new Enemy("Small Bandit", 25, 4, 1));
    }
}


public class AbandonedVillage : Location
{
    public AbandonedVillage() : base("Crumbling houses and empty streets. Something… or someone… still lingers here.")
    {
        MinLevel = 3;
        MaxLevel = 5;
        IsUnlocked = false;
        Items.Add(new Item("Rusty Axe", "An old axe with a dull blade. It has seen better days.", ItemType.Weapon, 7, 30));
        Items.Add(new Item("Calm Amulet", "An amulet that seems to radiate a calming energy. It might help reduce damage taken.", ItemType.Accessory, 5, 25));
        Items.Add(new Item("Old Elixir", "A mysterious elixir that restores a small amount of health and mana.", ItemType.Consumable, 15, 20));
        EnemyTypes.Add(() => new Enemy("Undead Farmer", 40, 8, 4));
        EnemyTypes.Add(() => new Enemy("Shadowy Figure", 35, 10, 3));
        EnemyTypes.Add(() => new Enemy("Raider", 45, 12, 5));
    }
}


public class DarkCavern : Location
{
    public DarkCavern() : base("A deep cave filled with echoes and unknown creatures. Light does not last long here.")
    {
        MinLevel = 5;
        MaxLevel = 8;
        IsUnlocked = false;
        Items.Add(new Item("Iron Sword", "A sturdy iron sword that can deal decent damage. It has a simple design.", ItemType.Weapon, 15, 50));
        Items.Add(new Item("Ring of Endurance", "A ring that enhances the wearer's endurance, providing extra health.", ItemType.Accessory, 10, 40));
        Items.Add(new Item("Cooper Armor", "Basic armor made of copper. It offers moderate protection.", ItemType.Armor, 8, 35));
        EnemyTypes.Add(() => new Enemy("Cave Spider", 50, 10, 5));
        EnemyTypes.Add(() => new Enemy("Stone Golem", 60, 6, 10));
        EnemyTypes.Add(() => new Enemy("Bat Swarm", 45, 12, 3));
    }
}


public class BurndenRuins : Location
{
    public BurndenRuins() : base("The remains of a city destroyed by fire. Ash still falls from the sky.")
    {
        MinLevel = 8;
        MaxLevel = 12;
        IsUnlocked = false;
        Items.Add(new Item("Fire dragger", "A sword imbued with the essence of fire. It can deal extra damage to enemies weak to fire.", ItemType.Weapon, 25, 100));
        Items.Add(new Item("Ashen Cloak", "A cloak that provides resistance to fire damage. It has a charred appearance.", ItemType.Armor, 12, 80));
        Items.Add(new Item("Phoenix Feather", "A rare feather that can be used to craft powerful items. It is said to have regenerative properties.", ItemType.Consumable, 30, 60));
        EnemyTypes.Add(() => new Enemy("Flame Wraith", 70, 15, 5));
        EnemyTypes.Add(() => new Enemy("Ashen Knight", 80, 20, 10));
        EnemyTypes.Add(() => new Enemy("Fire Elemental", 90, 25, 8));
    }
}


public class FrozenPeak : Location
{
    public FrozenPeak() : base("Icy mountains where only the strongest survive. Every step could be your last.")
    {
        MinLevel = 12;
        MaxLevel = 16;
        IsUnlocked = false;
        Items.Add(new Item("Ice Blade", "A sword forged from enchanted ice. It can freeze enemies on hit.", ItemType.Weapon, 30, 150));
        Items.Add(new Item("Frost Shield", "A shield that provides resistance to ice damage. It has a frosty appearance.", ItemType.Armor, 15, 120));
        Items.Add(new Item("Glacial Elixir", "A powerful elixir that restores a large amount of health and mana. It has a chilling effect when consumed.", ItemType.Consumable, 40, 100));
        EnemyTypes.Add(() => new Enemy("Ice Golem", 100, 20, 15));
        EnemyTypes.Add(() => new Enemy("Frost Sorcerer", 80, 25, 5));
        EnemyTypes.Add(() => new Enemy("Yeti", 120, 30, 20));
    }
}
