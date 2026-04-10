using System;
using System.Collections.Generic;
using System.Text;


public static class Bosses
{
    public class ForgottenGroveBoss : Enemy
    {
        public ForgottenGroveBoss() : base("Whispering Ent", 320, 20, 8)
        {
            Description = "An ancient tree awakened by dark magic. Its twisted branches move without wind, whispering the names of those who vanished in the forest.";
            Level = 5;
            CriticalChance = 15;
            XpReward = 100;
            GoldReward = 200;
        }
    }

    public class AbandonedVillageBoss : Enemy
    {
        public AbandonedVillageBoss() : base("The Hollow Mayor", 450, 25, 12)
        {
            Description = "Once a respected leader, now a hollow shell of his former self. He commands the shadows of the abandoned village, seeking to expand his influence.";
            Level = 8;
            CriticalChance = 20;
            XpReward = 150;
            GoldReward = 300;
        }
    }

    public class DarkCavernBoss : Enemy
    {
        public DarkCavernBoss() : base("The Blind Devourer", 600, 35, 15)
        {
            Description = "A massive, sightless creature that hunts using sound and vibration. Its presence fills the cavern with dread.";
            Level = 12;
            CriticalChance = 25;
            XpReward = 200;
            GoldReward = 400;
        }
    }

    public class BurndenRuinsBoss : Enemy
    {
        public BurndenRuinsBoss() : base("The Ashen Warlord", 800, 40, 20)
        {
            Description = "A burned warrior bound to the ruins, eternally fighting in a war that already ended. Flames still rage within his armor.";
            Level = 16;
            CriticalChance = 30;
            XpReward = 250;
            GoldReward = 500;
        }
    }

    public class FrozenPeaksBoss : Enemy
    {
        public FrozenPeaksBoss() : base("Frost Titan", 1000, 50, 25)
        {
            Description = "A towering giant of ice and stone that guards the frozen summit. Each step shakes the mountain.";
            Level = 20;
            CriticalChance = 35;
            XpReward = 300;
            GoldReward = 600;
        }
    }
}