using HorseManager2022;
using HorseManager2022.Enums;
using HorseManager2022.Models;
using HorseManager2022.UI;
using HorseManager2022.UI.Components;
using HorseManager2022.UI.Dialogs;
using HorseManager2022.UI.Screens;

Console.OutputEncoding = System.Text.Encoding.UTF8;

// Initial setup
GameManager gameManager = new();
Audio.PlayTownSong();

// Create UI
Topbar topbar = new();
ScreenMenu initialScreen = new("Welcome to Horse Manager 2022");
ScreenMenu loadGameScreen = new("Load game", initialScreen);
ScreenCity cityScreen = new(topbar, loadGameScreen);
ScreenHouse vetScreen = new(topbar, cityScreen);
ScreenHouse shopScreen = new( topbar, cityScreen);
ScreenHouse shopBuyScreen = new(topbar, shopScreen);
ScreenHouse shopSellScreen = new(topbar, shopScreen);
ScreenHouse stableScreen = new(topbar, cityScreen);
ScreenHouse raceTrackScreen = new(topbar, cityScreen);
CalendarScreen calendarScreen = new(topbar, cityScreen);
InitialHorseSelectionScreen horseSelectionScreen = new(cityScreen, gameManager);
ScreenTable<Horse, Player> horsesStableScreen = new(topbar, "Horses in Stable", stableScreen, new string[] { "price"});
ScreenTable<Jockey, Player> joqueysStableScreen = new(topbar, "Hired Joqueys", stableScreen, new string[] { "price" });
ScreenShop<Horse, Shop> horsesBuyScreen = new(topbar, "[Shop] Select Horses to buy", shopBuyScreen, new string[] { "energy" }, true);
ScreenShop<Jockey, Shop> joqueysBuyScreen = new(topbar, "[Shop] Select Joqueys to hire", shopBuyScreen, new string[] { "energy" }, true);
ScreenShop<Horse, Player> horsesSellScreen = new(topbar, "[Shop] Select Horses to sell", shopSellScreen, new string[] { "energy" }, true);
ScreenShop<Jockey, Player> joqueysSellScreen = new(topbar, "[Shop] Select Joqueys to fire", shopSellScreen, new string[] { "energy" }, true);
ScreenTeams teamsStableScreen = new(topbar, "Teams", stableScreen);
ScreenTeams trainingScreen = new(topbar, "Select Team to go training", raceTrackScreen, isSelectable: true , isAddable: true, raceType: RaceType.Training);
ScreenTeams raceEventScreen = new(topbar, "Select Team to enter event", raceTrackScreen, isSelectable: true, isAddable: true, raceType: RaceType.Event);


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


initialScreen.AddOption("Credits", initialScreen, () => { UI.ShowCreditScreen(); });
cityScreen.AddOption("Vet", vetScreen);
vetScreen.AddOption("Details", vetScreen);
vetScreen.AddOption("Upgrade", vetScreen);
cityScreen.AddOption("Shop", shopScreen);
shopScreen.AddOption("Buy", shopBuyScreen);
shopBuyScreen.AddOption("Horses", horsesBuyScreen);
shopBuyScreen.AddOption("Jockeys", joqueysBuyScreen);
shopBuyScreen.AddOption("Back", shopScreen);
shopScreen.AddOption("Sell", shopSellScreen);
shopSellScreen.AddOption("Horses", horsesSellScreen);
shopSellScreen.AddOption("Jockeys", joqueysSellScreen);
shopSellScreen.AddOption("Back", shopScreen);
shopScreen.AddOption("LootBoxs", shopScreen);
cityScreen.AddOption("Stable", stableScreen);
stableScreen.AddOption("Horses", horsesStableScreen);
stableScreen.AddOption("Jockeys", joqueysStableScreen);
stableScreen.AddOption("Teams", teamsStableScreen);
cityScreen.AddOption("Racetrack", raceTrackScreen);
raceTrackScreen.AddOption("Training", trainingScreen);
raceTrackScreen.AddOption("Race Event", raceEventScreen);


/*
    [Topbar] --> Calendar [Option]
*/
topbar.AddOption("Calendar", calendarScreen, () => {
    calendarScreen.calendar = new Calendar(gameManager.currentDate, gameManager.GetList<Event, Player>());
});


/*
    [Topbar] --> Sleep [Option]
*/
topbar.AddOption("Sleep", cityScreen, () => {
    
    DialogConfirmation dialogConfirmation = new(
        x: 20, y: 10,
        title: "Sleep", 
        message: "Are you sure you want to sleep?",
        dialogType: DialogType.Question,
        previousScreen: initialScreen, 
        onConfirm: () => {
            
            gameManager.currentDate?.NextDay(gameManager);

        }, onCancel: () => {});

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
