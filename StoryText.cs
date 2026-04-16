using System;
using System.Threading;

public static class StoryText
{
    public static void WriteWithDelay(string text, int delayMs = 30)
    {
        foreach (char c in text)
        {
            Console.Write(c);
            Thread.Sleep(delayMs);
        }
    }

    public static void WriteLineWithDelay(string text, int delayMs = 30)
    {
        WriteWithDelay(text + Environment.NewLine, delayMs);
        Console.WriteLine();
    }

    public static void WriteColored(string text, ConsoleColor color, int delayMs = 30)
    {
        Console.ForegroundColor = color;
        WriteWithDelay(text, delayMs);
        Console.ResetColor();
    }

    public static void WriteLineColored(string text, ConsoleColor color, int delayMs = 30)
    {
        WriteColored(text + Environment.NewLine, color, delayMs);
        Console.WriteLine();
    }

    public static void DisplayTitleScreen()
    {
        Console.Clear();
        WriteLineColored(@"
    ╔══════════════════════════════════════╗
    ║          CONSOLE RPG ADVENTURE       ║
    ║              ~ Welcome ~             ║
    ╚══════════════════════════════════════╝
    ", ConsoleColor.Cyan, 20);
    }

    public static void ShowLoadingBar(string message)
    {
        Console.WriteLine(message);
        WriteColored("[", ConsoleColor.White);
        for (int i = 0; i < 20; i++)
        {
            WriteColored("█", ConsoleColor.Green);
            Thread.Sleep(150);
        }
        WriteColored("] Complete!", ConsoleColor.White);
        Console.WriteLine();
    }

    public static string GetColorInput(string prompt, ConsoleColor promotColor = ConsoleColor.Green)
    {
        WriteColored(prompt, promotColor);
        return Console.ReadLine() ?? "";
    }

    public static void DisplayDamage(int damage, bool isCritical = false)
    {
        if (isCritical)
        {
            WriteLineColored($"💥 CRITICAL HIT! {damage} damage! 💥", ConsoleColor.Red, 15);
        }
        else
        {
            WriteLineColored($"⚔️ {damage} damage dealt!", ConsoleColor.Yellow, 25);
        }
    }

    public static void ShowTavernIntro()
    {
        Console.Clear();
        ShowLoadingBar("Loading story...");

        WriteLineWithDelay("The year is 1347.", 40);
        WriteLineWithDelay("The war ended three years ago.", 40);
        WriteLineWithDelay("Nobody won.", 40);

        Thread.Sleep(1000);
        Console.WriteLine();

        WriteLineWithDelay("You open your eyes.", 35);
        WriteLineWithDelay("Wooden ceiling. Candlelight. The smell of ale and ash.", 35);
        WriteLineWithDelay("Someone is snoring in the corner.", 35);

        Thread.Sleep(600);
        Console.WriteLine();

        WriteLineWithDelay("You are in the Broken Antler — a tavern on the edge of the Ashfeld region.", 30);
        WriteLineWithDelay("Your boots are muddy. Your coin purse is empty.", 30);
        WriteLineWithDelay("On the table in front of you: a knife, and a crumpled letter.", 30);

        Thread.Sleep(500);
        Console.WriteLine();

        WriteLineColored("The letter is addressed to you.", ConsoleColor.DarkRed, 50);

        Thread.Sleep(800);
        Console.WriteLine();

        WriteLineWithDelay("You don't remember the last three days.", 30);

        Thread.Sleep(400);

        WriteLineWithDelay("You unfold the letter.", 25);

        Thread.Sleep(600);
        Console.WriteLine();

        WriteLineColored("   \"Come to Morthal. Before they find you.\"", ConsoleColor.DarkYellow, 35);

        Thread.Sleep(1200);
        Console.WriteLine();

        WriteLineWithDelay("No signature.", 40);

        Thread.Sleep(800);
        Console.WriteLine();

        WriteLineColored("Press any key to rise from your seat...", ConsoleColor.White, 20);
        Console.ReadKey(true);
        
    }

    public static Player ShowClassSelection()
    {
        Console.Clear();

        WriteLineColored("The barkeeper eyes you from across the room.", ConsoleColor.DarkGray, 35);
        Thread.Sleep(400);
        WriteLineColored("\"You've got that look,\" he mutters. \"Like someone who's not done yet.\"", ConsoleColor.DarkGray, 30);
        Thread.Sleep(600);
        Console.WriteLine();

        WriteLineColored("He slides a drink across the bar.", ConsoleColor.DarkGray, 35);
        Thread.Sleep(400);
        WriteLineColored("\"So. What do they call you?\"", ConsoleColor.DarkGray, 35);
        Thread.Sleep(400);
        Console.WriteLine();

        WriteLineColored("Your name: ", ConsoleColor.White, 20);
        string playerName = Console.ReadLine() ?? "Stranger";
        if (string.IsNullOrWhiteSpace(playerName)) playerName = "Stranger";

        Console.WriteLine();
        Thread.Sleep(400);
        WriteLineColored($"The barkeeper repeats it quietly, as if testing the weight of it.", ConsoleColor.DarkGray, 35);
        Thread.Sleep(300);
        WriteLineColored($"   \"{playerName}.\"", ConsoleColor.DarkYellow, 40);
        Thread.Sleep(500);
        WriteLineColored("\"Alright. And what are you?\"", ConsoleColor.DarkGray, 30);

        Thread.Sleep(800);
        Console.WriteLine();
        Console.WriteLine();

        WriteLineColored("[1] SOLDIER", ConsoleColor.Red, 15);
        WriteLineColored("    You fought in the war. You know pain — and you know how to cause it.", ConsoleColor.DarkGray, 20);
        WriteLineColored("    + High HP   + Strong attacks   - Slow   - No magic", ConsoleColor.DarkRed, 20);
        Console.WriteLine();

        WriteLineColored("[2] ROGUE", ConsoleColor.Green, 15);
        WriteLineColored("    You survived by staying unseen. Shadows are your oldest friends.", ConsoleColor.DarkGray, 20);
        WriteLineColored("    + Fast   + Critical hits   - Low HP   - Weak head-on", ConsoleColor.DarkGreen, 20);
        Console.WriteLine();

        WriteLineColored("[3] SCHOLAR", ConsoleColor.Cyan, 15);
        WriteLineColored("    You studied things men weren't meant to know. It cost you.", ConsoleColor.DarkGray, 20);
        WriteLineColored("    + Powerful spells   + Potions   - Fragile   - Limited resources", ConsoleColor.DarkCyan, 20);
        Console.WriteLine();

        WriteLineColored("[4] ARCHER", ConsoleColor.DarkYellow, 15);
        WriteLineColored("    Distance is your advantage. You never liked being too close.", ConsoleColor.DarkGray, 20);
        WriteLineColored("    + Always strikes first   + Bonus on flee   - Weak in melee   - Low armor", ConsoleColor.DarkYellow, 20);
        Console.WriteLine();

        WriteLineColored("Your choice: ", ConsoleColor.White, 20);

        string playerClass = "";
        while (true)
        {
            var key = Console.ReadKey(true).KeyChar;
            if (key == '1') { playerClass = "SOLDIER"; break; }
            if (key == '2') { playerClass = "ROGUE"; break; }
            if (key == '3') { playerClass = "SCHOLAR"; break; }
            if (key == '4') { playerClass = "ARCHER"; break; }
        }

        Player player = new Player { Name = playerName };
        PlayerClass selectedClass = playerClass switch
        {
            "SOLDIER" => PlayerClass.Soldier,
            "ROGUE" => PlayerClass.Rogue,
            "SCHOLAR" => PlayerClass.Scholar,
            "ARCHER" => PlayerClass.Archer,
            _ => PlayerClass.Soldier
        };
        
        player.SetPlayerClass(selectedClass);
       

        Console.WriteLine();
        Thread.Sleep(400);
        WriteLineColored("The barkeeper nods slowly.", ConsoleColor.DarkGray, 35);
        Thread.Sleep(500);
        WriteLineColored($"\"A {playerClass.ToLower()}. Yeah.\"", ConsoleColor.DarkGray, 30);
        Thread.Sleep(400);
        WriteLineColored($"\"Dangerous times for someone like you, {playerName}.\"", ConsoleColor.DarkGray, 30);
        Thread.Sleep(600);
        WriteLineColored("He turns back to his glasses without another word.", ConsoleColor.DarkGray, 35);
        Thread.Sleep(800);
        Console.WriteLine();
        WriteLineColored("Press any key to step outside...", ConsoleColor.White, 20);
        Console.ReadKey(true);

        return player;
    }

    public static void ShortScene()
    {
        Console.Clear();

        WriteLineColored("The door creaks as you step outside.", ConsoleColor.Gray, 35);
        Thread.Sleep(400);
        WriteLineColored("Cold air. The kind that bites.", ConsoleColor.Gray, 35);
        Thread.Sleep(600);
        Console.WriteLine();

        WriteLineColored("The village is quiet.", ConsoleColor.DarkGray, 40);
        Thread.Sleep(300);
        WriteLineColored("Too quiet for midday.", ConsoleColor.DarkGray, 40);
        Thread.Sleep(700);
        Console.WriteLine();

        WriteLineColored("A child stares at you from a doorway, then disappears inside.", ConsoleColor.DarkGray, 35);
        Thread.Sleep(500);
        WriteLineColored("An old woman across the street won't meet your eyes.", ConsoleColor.DarkGray, 35);
        Thread.Sleep(600);
        Console.WriteLine();

        WriteLineColored("You look north.", ConsoleColor.Gray, 30);
        Thread.Sleep(400);
        WriteLineColored("The road to Morthal cuts straight through the Forgotten Grove.", ConsoleColor.Gray, 30);
        Thread.Sleep(300);
        WriteLineColored("You've heard stories about that forest.", ConsoleColor.Gray, 30);
        Thread.Sleep(300);
        WriteLineColored("Everyone has.", ConsoleColor.DarkGray, 40);
        Thread.Sleep(800);
        Console.WriteLine();

        WriteLineColored("You fold the letter and put it in your pocket.", ConsoleColor.Gray, 35);
        Thread.Sleep(400);
        WriteLineColored("There's nothing left for you here anyway.", ConsoleColor.DarkGray, 35);
        Thread.Sleep(800);
        Console.WriteLine();

        WriteLineColored("The grove awaits.", ConsoleColor.DarkGreen, 40);
        Thread.Sleep(600);
        Console.WriteLine();

        WriteLineColored("Press any key to enter the Forgotten Grove...", ConsoleColor.White, 20);
        Console.ReadKey(true);
    }

    public static void ForgottenGroveIntro()
    {
        Console.Clear();

        WriteLineColored("The trees close in behind you the moment you step off the road.", ConsoleColor.DarkGreen, 35);
        Thread.Sleep(500);
        WriteLineColored("You didn't notice it happening. They just... did.", ConsoleColor.DarkGray, 35);
        Thread.Sleep(700);
        Console.WriteLine();

        WriteLineColored("The Forgotten Grove.", ConsoleColor.Green, 50);
        Thread.Sleep(600);
        Console.WriteLine();

        WriteLineColored("Mist clings to the roots. The light here is wrong — too pale, too still.", ConsoleColor.DarkGray, 30);
        Thread.Sleep(500);
        WriteLineColored("Somewhere deep in the trees, something moves.", ConsoleColor.DarkGray, 30);
        Thread.Sleep(400);
        WriteLineColored("You tell yourself it's the wind.", ConsoleColor.DarkGray, 30);
        Thread.Sleep(800);
        Console.WriteLine();

        WriteLineColored("The path to Morthal runs through here.", ConsoleColor.Gray, 35);
        Thread.Sleep(400);
        WriteLineColored("Assuming you can find the path.", ConsoleColor.DarkGray, 40);
        Thread.Sleep(900);
        Console.WriteLine();

        WriteLineColored("You grip the knife from the table.", ConsoleColor.Gray, 35);
        Thread.Sleep(400);
        WriteLineColored("It's not much. But it's something.", ConsoleColor.DarkGray, 35);
        Thread.Sleep(800);
        Console.WriteLine();

        WriteLineColored("  [1] Move deeper into the grove", ConsoleColor.White, 20);
        WriteLineColored("  [2] Check your surroundings first", ConsoleColor.White, 20);
        WriteLineColored("  [3] Look for tracks on the ground", ConsoleColor.White, 20);
        Console.WriteLine();

        WriteLineColored("What do you do?", ConsoleColor.DarkYellow, 25);

        while (true)
        {
            var key = Console.ReadKey(true).KeyChar;

            if (key == '1')
            {
                Console.WriteLine();
                WriteLineColored("You push forward into the mist.", ConsoleColor.DarkGray, 35);
                Thread.Sleep(500);
                WriteLineColored("Something growls nearby.", ConsoleColor.DarkRed, 40);
                Thread.Sleep(600);
                // -> trigger combat
                break;
            }
            if (key == '2')
            {
                Console.WriteLine();
                WriteLineColored("You scan the treeline. Nothing moves.", ConsoleColor.DarkGray, 35);
                Thread.Sleep(400);
                WriteLineColored("But the silence itself feels like a warning.", ConsoleColor.DarkGray, 35);
                Thread.Sleep(600);
                WriteLineColored("You spot a broken lantern on the ground. Someone was here recently.", ConsoleColor.Gray, 30);
                Thread.Sleep(700);
                // -> small lore detail, potom combat
                break;
            }
            if (key == '3')
            {
                Console.WriteLine();
                WriteLineColored("You crouch down and study the mud.", ConsoleColor.DarkGray, 35);
                Thread.Sleep(400);
                WriteLineColored("Bootprints. Fresh ones. And something else alongside them.", ConsoleColor.DarkGray, 35);
                Thread.Sleep(500);
                WriteLineColored("Too large for a man. Too deliberate for an animal.", ConsoleColor.Gray, 30);
                Thread.Sleep(800);
                // -> small lore detail, potom combat
                break;
            }
        }
    }

    public static void ForgottenGroveBossInto()
    {
        Console.Clear();

        WriteLineColored("You hear it before you see it.", ConsoleColor.DarkGray, 35);
        Thread.Sleep(500);
        WriteLineColored("Your name.", ConsoleColor.DarkRed, 50);
        Thread.Sleep(700);
        WriteLineColored("Spoken softly. From everywhere at once.", ConsoleColor.DarkGray, 35);
        Thread.Sleep(800);
        Console.WriteLine();

        WriteLineColored("The mist parts.", ConsoleColor.DarkGray, 40);
        Thread.Sleep(600);
        Console.WriteLine();

        WriteLineColored("It stands at the heart of the grove.", ConsoleColor.Gray, 30);
        Thread.Sleep(400);
        WriteLineColored("Ancient. Massive. Wrong.", ConsoleColor.DarkGray, 45);
        Thread.Sleep(700);
        WriteLineColored("Branches twist without wind. Bark darker than it should be.", ConsoleColor.DarkGray, 30);
        Thread.Sleep(500);
        WriteLineColored("And from somewhere deep within the wood — voices.", ConsoleColor.DarkGray, 30);
        Thread.Sleep(400);
        WriteLineColored("Names. Dozens of them. All at once.", ConsoleColor.DarkGray, 30);
        Thread.Sleep(800);
        Console.WriteLine();

        WriteLineColored($"Then yours again. Clear. Patient.", ConsoleColor.DarkRed, 35);
        Thread.Sleep(900);
        Console.WriteLine();

        WriteLineColored("                — WHISPERING END —", ConsoleColor.DarkGreen, 25);
        Thread.Sleep(400);
        WriteLineColored("              Ancient of the Forgotten Grove", ConsoleColor.DarkGray, 20);
        Thread.Sleep(1000);
        Console.WriteLine();

        WriteLineColored("The branches reach toward you.", ConsoleColor.Gray, 35);
        Thread.Sleep(500);
        WriteLineColored("It already knows your name.", ConsoleColor.DarkRed, 40);
        Thread.Sleep(600);
        WriteLineColored("It has known it for a long time.", ConsoleColor.DarkGray, 40);
        Thread.Sleep(900);
        Console.WriteLine();

        WriteLineColored("Press any key to fight...", ConsoleColor.White, 20);
        Console.ReadKey(true);
    }
    
}