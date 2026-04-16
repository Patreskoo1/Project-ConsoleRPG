using System;
using System.Collections.Generic;
using System.Text;


public static class Bosses
{
    public class ForgottenGroveBoss : Enemy
    {
        public ForgottenGroveBoss() : base("Whispering Ent", 320, strength: 15, endurance: 8, agility: 4, luck: 6, level: 5)
        {
            Description = "An ancient tree awakened by dark magic. Its twisted branches move without wind, whispering the names of those who vanished in the forest.";
            XpReward = 100;
            GoldReward = 200;
        }
    }

    public class AbandonedVillageBoss : Enemy
    {
        public AbandonedVillageBoss() : base("The Hollow Mayor", 450, strength: 20, endurance: 12, agility: 6, luck: 4, level: 8)
        {
            Description = "Once a respected leader, now a hollow shell of his former self. He commands the shadows of the abandoned village, seeking to expand his influence.";
            XpReward = 150;
            GoldReward = 300;
        }
    }

    public class DarkCavernBoss : Enemy
    {
        public DarkCavernBoss() : base("The Blind Devourer", 600, strength: 25, endurance: 15, agility: 10, luck: 2, level: 12)
        {
            Description = "A massive, sightless creature that hunts using sound and vibration. Its presence fills the cavern with dread.";
            XpReward = 200;
            GoldReward = 400;
        }
    }

    public class BurndenRuinsBoss : Enemy
    {
        public BurndenRuinsBoss() : base("The Ashen Warlord", 800, strength: 30, endurance: 20, agility: 12, luck: 5, level: 15)
        {
            Description = "A burned warrior bound to the ruins, eternally fighting in a war that already ended. Flames still rage within his armor.";
            XpReward = 250;
            GoldReward = 500;
        }
    }

    public class FrozenPeaksBoss : Enemy
    {
        public FrozenPeaksBoss() : base("Frost Titan", 1000, strength: 35, endurance: 25, agility: 15, luck: 8, level: 20)
        {
            Description = "A towering giant of ice and stone that guards the frozen summit. Each step shakes the mountain.";
            XpReward = 300;
            GoldReward = 600;
        }
    }
}