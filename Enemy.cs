public class Enemy : Character
{
    public int XpReward { get; set; } = 25;
    public int GoldReward { get; set; } = 10;

    
    public Enemy(string name, int health, int strength, int endurance, int agility, int luck,int level = 1)
    {
        Name = name;
        MaxHealth = health;
        Health = health;
        Strength = strength;
        Endurance = endurance;
        Agility = agility;
        Luck = luck;
        Level = level;
        
    }
}
