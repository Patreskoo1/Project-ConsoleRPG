public class Enemy : Character
{
    public int XpReward { get; set; } = 25;
    public int GoldReward { get; set; } = 10;

    // Nastavi zakladne staty nepriatela. Dokoncene.
    public Enemy()
    {
        Health = 50;
        AttackPower = 15;
        Defense = 5;
        CriticalChance = 5;
    }

    public Enemy(string name, int health, int attackPower, int defense)
    {
        Name = name;
        Health = health;
        AttackPower = attackPower;
        Defense = defense;
        CriticalChance = 5;
    }
}
