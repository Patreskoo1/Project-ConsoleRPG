using System;
using System.Threading;

public static class StoryText
{
    static void WriteWithDelay(string text, int delayMs = 30)
    {
        foreach (char c in text)
        {
            Console.Write(c);
            Thread.Sleep(delayMs);
        }
    }

    static void WriteLineWithDelay(string text, int delayMs = 30)
    {
        WriteWithDelay(text + Environment.NewLine, delayMs);
        Console.WriteLine();
    }

    static void WriteColored(string text, ConsoleColor color, int delayMs = 30)
    {
        Console.ForegroundColor = color;
        WriteWithDelay(text, delayMs);
        Console.ResetColor();
    }

    static void WriteLineColored(string text, ConsoleColor color, int delayMs = 30)
    {
        WriteColored(text + Environment.NewLine, color, delayMs);
        Console.WriteLine();
    }

    public static void ShowTavernIntro()
    {
        Console.Clear();

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

    public static void ShowClassSelection()
    { 
        Console.Clear();

        WriteLineColored("The barkeeper eyes you from across the room.", ConsoleColor.DarkGray, 35);
        Thread.Sleep(400);
        WriteLineColored("\"You've got that look,\" he mutters. \"Like someone who's not done yet.\"", ConsoleColor.DarkGray, 30);
        Thread.Sleep(600);
        Console.WriteLine();

        WriteLineColored("He slides a drink across the bar.", ConsoleColor.Gray, 35);
        Thread.Sleep(400);
        WriteLineColored("\"So. What do they call you?\"", ConsoleColor.Gray, 35);
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

            PlayerClass selectedClass = playerClass switch
            {
                "SOLDIER" => PlayerClass.Soldier,
                "ROGUE" => PlayerClass.Rogue,
                "SCHOLAR" => PlayerClass.Scholar,
                "ARCHER" => PlayerClass.Archer,
                _ => PlayerClass.Soldier
            };

        Player.SetPlayerClass(selectedClass);

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
}