using System.Linq;


// **GLOBÁLNE PREMENNÉ**
int enemiesDefeated = 0;

// **POMOCNÉ METÓDY PRE ZOBRAZENIE TEXTU S EFEKTOM PÍSANIA**
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

static void DisplayTitleScreen()
{
    Console.Clear();
    WriteLineColored(@"
    ╔══════════════════════════════════════╗
    ║          CONSOLE RPG ADVENTURE       ║
    ║              ~ Welcome ~             ║
    ╚══════════════════════════════════════╝
    ", ConsoleColor.Cyan, 20);
}

static void ShowLoadingBar(string message)
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

    static string GetColorInput(string prompt, ConsoleColor promotColor = ConsoleColor.Green) 
{
    WriteColored(prompt, promotColor);
    return Console.ReadLine() ?? "";
}

static void DisplayDamage(int damage, bool isCritical = false)
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

//SAVE OR LOAD GAME OPTION
DisplayTitleScreen();

if (GameStateManager.SaveFileExists())
{
    WriteLineWithDelay("A previous adventure was found.", 30);
    WriteLineWithDelay("[1] Continue your journey", 30);
    WriteLineWithDelay("[2] Start a new adventure", 30);
    if (int.TryParse(GetColorInput("What will you choose? ", ConsoleColor.Green), out int choice) && choice == 1)
    {
        GameStateManager.LoadGame();
        WriteLineWithDelay("Your adventure continues...", 30);
    }
    else
    {
        WriteLineWithDelay("A new adventure begins...", 30);
        ShowIntroStory();
    }
}
else 
{ 
    ShowIntroStory(); 
}


    // **HLAVNÁ METÓDA - ZAČIATOK HRY**

static void ShowIntroStory()
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

static void SetPlayerClassAndName()
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

// Spustenie hlavného dobrodružstva

// **POMOCNÉ METÓDY PRE POUŽÍVATEĽSKÝ VSTUP**
bool IsYes(string? input)
{
    string normalized = (input ?? string.Empty).Trim().ToLowerInvariant();
    return normalized is "yes" or "ano";
}

// **NEPRIATEĽ A BOSS LOGIKA**
// Metóda RandomEnemy() - výber náhodného nepriateľa z lokácie
// Metóda GetBossForLocation() - získanie boss-a pre konkrétnu lokáciu

// **ZOBRAZENIE INFORMÁCIÍ**
// Metóda PrintPlayerStats() - výpis aktuálnych štatistík hráča

// **HLAVNÝ HERNÝ CYKLUS**
// Metóda RunAdventure() - hlavný cyklus dobrodružstva
// - Stretnutie s nepriateľom
// - Voľba boja
// - Spustenie boja
// - Spracovanie výsledku boja
// - Pokračovanie alebo koniec

// **BOJOVÝ SYSTÉM**
// Metóda RunBattle() - hlavná logika boja medzi hráčom a nepriateľom
// - Cyklus boja kým jeden nepadne
// - Striedavé útoky
// - Kontrola zdravia

// Metóda TryPlayerAttack() - vykonanie útoku hráča
// - Voľba útoku alebo úteku
// - Výpočet damage
// - Kritický zásah
// - Ubratie zdravia nepriateľovi

// Metóda ResolveEnemyAttack() - vykonanie útoku nepriateľa
// - Výpočet damage nepriateľa
// - Kritický zásah nepriateľa
// - Ubratie zdravia hráčovi
// - Kontrola prehry

// **SYSTÉM ODMIEN A LEVELOVANIA**
// Metóda HandleEnemyDefeat() - spracovanie prehry nepriateľa
// - Pridanie XP a zlata
// - Drop itemov
// - Level up kontrola
// - Boss fight logika (po 3 nepriateľoch)
// - Boss boj a výsledky

// **MENU A VOĽBY PO BOJI**
// Metóda HandleAdventureChoice() - menu volieb po boji
// - Pokračovanie dobrodružstva
// - Obchod
// - Zobrazenie štatistík
// - Inventár
// - Equipovanie itemov
// - Použitie lektvarov
// - Pokračovanie v prieskume
// - Ukončenie hry
// - Debug možnosti
// - Uloženie a ukončenie

// **INVENTÁR A EQUIPMENT SYSTÉM**
// Metóda TryEquipItemFromInventory() - equipovanie itemov z inventára
// - Zobrazenie inventára
// - Výber itemu na equipnutie
// - Použitie EquipmentManager-a

// Metóda TryUseConsumableItem() - použitie consumable itemov
// - Zobrazenie consumable itemov
// - Výber lektvaru na použitie
// - Healing logika

// **ENUMERÁTOR PRE VÝSLEDKY BOJA**
// Enum BattleResult - možné výsledky boja
// - EnemyDefeated
// - PlayerDefeated  
// - PlayerRanAway
