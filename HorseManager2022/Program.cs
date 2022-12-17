using HorseManager2022;
using HorseManager2022.UI;
using HorseManager2022.UI.Dialogs;
using HorseManager2022.UI.Screens;

Console.OutputEncoding = System.Text.Encoding.UTF8;

// Create UI
GameManager gameManager = new();
Topbar topbar = new();
ScreenMenu initialScreen = new("Welcome to Horse Manager 2022");
ScreenMenu loadGameScreen = new("Load game", initialScreen);
ScreenCity cityScreen = new(topbar, loadGameScreen);
ScreenHouse vetScreen = new(topbar, cityScreen);
ScreenHouse shopScreen = new( topbar, cityScreen);
ScreenHouse stableScreen = new(topbar, cityScreen);
ScreenHouse raceTrackScreen = new(topbar, cityScreen);
CalendarScreen calendarScreen = new(topbar, cityScreen);
HorseSelectionScreen horseSelectionScreen = new(cityScreen);

// ---------------- Initial Screen Options ---------------- \\

/*
    Initial [Screen] --> Load game [Option]
*/
initialScreen.AddOption("Load game", loadGameScreen, () => {
    UI.PopulateScreenWithSaveOptions(loadGameScreen, cityScreen, gameManager);
});


/*
    Initial [Screen] --> New game [Option]
*/
initialScreen.AddOption("New game", horseSelectionScreen, () => { 

    
    UI.ShowCreateNewSaveScreen((savename) => {

        gameManager.CreateNewSave(savename);
        UI.PopulateScreenWithSaveOptions(loadGameScreen, cityScreen, gameManager);

    });
    

});


/*
    Initial [Screen] --> Credits [Option]
*/
initialScreen.AddOption("Credits", initialScreen, () => { UI.ShowCreditScreen(); });


/*
    City [Screen] --> Vet [Option]
*/
cityScreen.AddOption("Vet", vetScreen, () => {

    Console.WriteLine("Vet");
    Console.ReadKey();
});

/*
    Vet [Screen] --> Details [Option]
*/

vetScreen.AddOption("Details", vetScreen, () => { 
    
    Console.WriteLine("Details");
    Console.ReadKey();

});

/*
    Vet [Screen] --> Upgrade [Option]
*/

vetScreen.AddOption("Upgrade", vetScreen, () => { 
    
    Console.WriteLine("Upgrade");
    Console.ReadKey();
    
});

/*
    City [Screen] --> Shop [Option]
*/
cityScreen.AddOption("Shop", shopScreen, () => {});

/*
    Shop [Screen] --> Buy [Option]
*/

shopScreen.AddOption("Buy", shopScreen, () => {

    Console.WriteLine("Buy");

    Console.ReadKey();
    

});

/*
    Shop [Screen] --> Sell [Option]
*/

shopScreen.AddOption("Sell", shopScreen, () => {

    Console.WriteLine("Sell");

    Console.ReadKey();
    

});

/*
    City [Screen] --> Stable [Option]
*/
cityScreen.AddOption("Stable", stableScreen, () => {});

/*
    Stable [Screen] --> Horses [Option]
*/

stableScreen.AddOption("Horses", stableScreen, () => {

    Console.WriteLine("Horses");

    Console.ReadKey();

});

/*
    Stable [Screen] --> Jockeys [Option]
*/

stableScreen.AddOption("Jockeys", stableScreen, () => {

    Console.WriteLine("Jockeys");

    Console.ReadKey();

});

/*
    City [Screen] --> Racetrack [Option]
*/
cityScreen.AddOption("Racetrack", raceTrackScreen, () => {});

/*
    Racetrack [Screen] --> Train [Option]
*/

raceTrackScreen.AddOption("Train", raceTrackScreen, () => {

    Console.WriteLine("Train");

    Console.ReadKey();

});

/*
    Racetrack [Screen] --> Race [Option]
*/


raceTrackScreen.AddOption("Race", raceTrackScreen, () => {

    const int HORSES = 4;

    // Race start loop
    int x = 1;
    bool isRaceStarted = false;
    do
    {
        int y = 6;

        Console.Clear();

        // LeaderBoard
        Console.WriteLine("+-------------------------------------------------------------------------------+");
        Console.WriteLine("|                                                                               |");
        Console.WriteLine("|                     [Corrida Comum] (10Km)  > Prémio 100€ <                   |");
        Console.WriteLine("|                                                                               |");
        Console.WriteLine("+-------------------------------------------------------------------------------+");
        Console.WriteLine();
        Console.WriteLine();
        
        // Racetrack Start
        Console.WriteLine("================================================================================");

        // Racetrack
        for (int i = 0; i < HORSES; i++)
        {
            // Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("      ~        ~      ~                            ~                       ~    ");
            Console.WriteLine("  ~        ~                   ~          ~               ~      ~              ");
            Console.WriteLine("                  ~     ~           ~            ~                    ~         ");
            Console.WriteLine("         ~                    ~            ~            ~      ~             ~  ");
            Console.WriteLine("   ~            ~      ~             ~              ~                  ~        ");
            // Console.WriteLine("           ~                   ~              ~               ~                 ");
            Console.WriteLine("================================================================================");
        }

        // Draw Horses
        Random random = new();
        for (int i = 0; i < HORSES; i++)
        {
            Console.SetCursorPosition(x + 8, y);
            Console.Write(",,");
            Console.SetCursorPosition(x + 7, y + 1);
            Console.Write("/(-\\");
            Console.SetCursorPosition(x + 2, y + 2);
            Console.Write(",---' /`-'");
            Console.SetCursorPosition(x + 1, y + 3);
            Console.Write("/");
            Console.SetCursorPosition(x + 3, y + 3);
            Console.Write("( )__))");
            Console.SetCursorPosition(x, y + 4);
            Console.Write("/");
            Console.SetCursorPosition(x + 3, y + 4);
            Console.Write("//");
            Console.SetCursorPosition(x + 8, y + 4);
            Console.Write("\\\\");
            Console.SetCursorPosition(x + 3, y + 5);
            Console.Write("``");
            Console.SetCursorPosition(x + 9, y + 5);
            Console.Write("``");
            y += 6;
        }
        x += 3;
        Thread.Sleep(120);
        
        // Race Start / Countdown / Music
        if (!isRaceStarted) 
        {
            isRaceStarted = true;
            Audio.PlayRaceSong();
            DialogCounter dialogCounter = new(20, 8);
            dialogCounter.Show();
        }

    } while (x < 72);

    RaceLeaderboard leaderboard = new(85, 7);
    leaderboard.Show();
    Audio.PlayRaceEndSong();
    Console.ReadKey();

});


/*
    [Topbar] --> Calendar [Option]
*/
topbar.AddOption("Calendar", calendarScreen, () => {
    calendarScreen.calendar = new Calendar(gameManager.currentDate, gameManager.gameData.events);
});


/*
    [Topbar] --> Sleep [Option]
*/
topbar.AddOption("Sleep", cityScreen, () => {
    
    DialogConfirmation dialogConfirmation = new(
        20, // x
        10, // y
        "Sleep", 
        "Are you sure you want to sleep?", 
        initialScreen, 
        () => {
            // On Confirm

            gameManager.currentDate?.NextDay(gameManager);

        }, () => {
            // On Cancel
        });

    dialogConfirmation.Show();

});


// ---------------- Game Loop ---------------- \\
Screen? activeScreen, nextScreen;
activeScreen = initialScreen.Show(gameManager);


while (activeScreen != null)
{
    nextScreen = activeScreen.Show(gameManager);
    activeScreen = nextScreen;
}


Console.Clear();
Console.WriteLine("Thanks for playing!");
