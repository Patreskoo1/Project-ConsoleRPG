public class Character
{
    public string? Name { get; set; }
    public int Health { get; set; } = 100;
    public int Level { get; set; } = 1;
    public int AttackPower { get; set; } = 10;
    public int Defense { get; set; } = 5;
    public int CriticalChance { get; set; } = 5;
    public double CriticalMultiplier { get; set; } = 1.5;
    public int ItemDropChance { get; set; } = 30;

    // Vypocita zakladne poskodenie utoku v rozumnom rozmedzi. Dokoncene.
    public int GetAttackDamage(Random random)
    {
        int minimumDamage = Math.Max(1, AttackPower - 3);
        return random.Next(minimumDamage, AttackPower + 4);
    }

    // Hodi sancu na critical hit podla CriticalChance. Dokoncene.
    public bool RollCritical(Random random)
    {
        return random.Next(0, 100) < CriticalChance;
    }

    // Vypocita finalne damage proti cielu (obrana + kriticky zasah). Dokoncene.
    public int CalculateDamageAgainst(Character target, Random random, out bool isCritical)
    {
        int damage = Math.Max(0, GetAttackDamage(random) - target.Defense);
        isCritical = RollCritical(random);

        if (isCritical)
        {
            damage = (int)Math.Round(damage * CriticalMultiplier);
        }

        return Math.Max(0, damage);
    }

    public Item? GenerateRandomItemDrop(Random random, Location currentLocation)
    {
        if (random.Next(0, 100) >= ItemDropChance)
        {

            return null;
        }

        if (currentLocation.Items == null || currentLocation.Items.Count == 0)
        {
            return null;
        }

        return currentLocation.Items[random.Next(currentLocation.Items.Count)];
    }

}
