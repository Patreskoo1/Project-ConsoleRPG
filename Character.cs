public class Character
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public int Health { get; set; } 
    public int MaxHealth { get; set; }
    public int Level { get; set; } = 1;
    public int Strength { get; set; }
    public int Intelligence { get; set; }
    public int Agility { get; set; }
    public int Endurance { get; set; }
    public int Luck { get; set; }

    //Vypočítané vlastnosti
    public int PhysicalDamage => Strength * 2 + Level;
    public int MagicalDamage => Intelligence * 2 + Level;
    public int Armor => Endurance / 2;
    public double CritChance => Luck * 0.5;
    public double DodgeChance => Agility * 0.4;


}
