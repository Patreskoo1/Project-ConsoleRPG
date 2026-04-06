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

public class Goblin : Enemy
{
    // Nastavi meno typu nepriatela (Goblin). Dokoncene.
    public Goblin()
    {
        Name = "Goblin";
    }
}

public class Skeleton : Enemy
{
    // Nastavi staty a odmeny pre Skeletona. Dokoncene.
    public Skeleton()
    {
        Name = "Skeleton";
        Health = 40;
        XpReward = 20;
        AttackPower = 10;
        Defense = 3;
        GoldReward = 5;
    }
}

public class Zombie : Enemy
{
    // Nastavi staty a odmeny pre Zombie. Dokoncene.
    public Zombie()
    {
        Name = "Zombie";
        Health = 60;
        XpReward = 30;
        AttackPower = 20;
        Defense = 8;
        GoldReward = 15;
    }
}

public class HumanBandit : Enemy
{
    // Nastavi staty a odmeny pre Human Bandit-a. Dokoncene.
    public HumanBandit()
    {
        Name = "Human Bandit";
        Health = 70;
        XpReward = 35;
        AttackPower = 25;
        Defense = 10;
        GoldReward = 20;
    }
}

