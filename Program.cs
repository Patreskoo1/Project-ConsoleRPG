using System.Linq;


// **GLOBÁLNE PREMENNÉ**
// int enemiesDeatedInCurrentLocation = 0;

SaveOrLoadGame();
ShowIntroStory();
Player player = SetPlayerClassAndName();



//SAVE OR LOAD GAME OPTION
void SaveOrLoadGame()
{
    StoryText.DisplayTitleScreen();

    if (GameStateManager.SaveFileExists())
    {
        StoryText.WriteLineWithDelay("A previous adventure was found.", 30);
        StoryText.WriteLineWithDelay("[1] Continue your journey", 30);
        StoryText.WriteLineWithDelay("[2] Start a new adventure", 30);
        if (int.TryParse(StoryText.GetColorInput("What will you choose? ", ConsoleColor.Green), out int choice) && choice == 1)
        {
            GameStateManager.LoadGame();
            StoryText.WriteLineWithDelay("Your adventure continues...", 30);
        }
        else
        {
            StoryText.WriteLineWithDelay("A new adventure begins...", 30);
            ShowIntroStory();
        }
    }
    else
    {
        ShowIntroStory();
    }
}


    // **HLAVNÁ METÓDA - ZAČIATOK HRY**

void ShowIntroStory()
{
    StoryText.ShowTavernIntro();
}

Player SetPlayerClassAndName()
{
    Player player = StoryText.ShowClassSelection();
    return player;  
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
