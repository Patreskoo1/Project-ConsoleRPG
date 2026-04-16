public enum AbilityType
{
    Attack,
    Heal,
    Buff,
    Debuff,
    Special
}

public class Ability
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public int ManaCost { get; set; }
    public int Damage { get; set; }
    public int Healing { get; set; }
    public int BuffAmount { get; set; }
    public int BuffDuration { get; set; }
    public string? BuffStat { get; set; }
    public AbilityType Type { get; set; }
    public int Cooldown { get; set; }
}