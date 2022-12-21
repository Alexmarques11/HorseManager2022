using HorseManager2022.Enums;
using HorseManager2022.Interfaces;
using HorseManager2022.Models;
using HorseManager2022.UI.Components;
using HorseManager2022.UI.Dialogs;
using System;

namespace HorseManager2022.UI.Screens
{
    internal class ScreenTable<T, U> : ScreenWithTopbar where T : ISelectable
    {
        // Constants
        private const int DIALOG_POS_X = 20;
        private const int DIALOG_POS_Y = 15;

        // Properties
        private readonly bool isSelectable;
        private Table<T, U> table;
        
        public override int selectedPosition
        {
            get
            {
                return base.selectedPosition;
            }
            set
            {
                if (isModeDown && value > options.Count)
                    value = options.Count;

                if (isModeUp && value > topbar.options.Count)
                    value = topbar.options.Count;

                topbar.selectedPosition = value;

                table.selectedPosition = (isModeDown && isSelectable) ? value : -1; // -1 means no selection

                base.selectedPosition = value;
            }
        }


        // Constructor
        public ScreenTable(Topbar topbar, string title, Screen? previousScreen = null, string[]? propertiesToExclude = null, bool isSelectable = false)
            : base(topbar, previousScreen)
        {
            this.isSelectable = isSelectable;
            table = new Table<T, U>(title, propertiesToExclude ?? Array.Empty<string>(), isSelectable);
            
        }


        // Methods
        override public Screen? Show(GameManager? gameManager)
        {
            if (isSelectable)
                SetTableOptions(gameManager);

            // Wait for option
            Option? selectedOption = WaitForOption(() =>
            {
                Console.Clear();

                topbar.Show(this, gameManager);

                table.Show(gameManager);

            });

            selectedOption?.onEnter?.Invoke();
            return selectedOption?.nextScreen;
        }


        private void SetTableOptions(GameManager? gameManager)
        {
            options.Clear();
            foreach (T item in table.GetTableItems(gameManager))
            {
                Action onEnter = GetOptionOnEnter(item, gameManager);
                options.Add(new Option(nextScreen: this, onEnter: onEnter));
            }
        }


        private Action GetOptionOnEnter(T item, GameManager? gameManager)
        {
            return () => {

                // Get dialog data
                string itemType = item.GetType().Name.ToLower();
                string action = (typeof(U) == typeof(Shop)) ? "buy" : "sell";

                // Build Dialog
                DialogConfirmation dialogConfirmation = new(
                    x: DIALOG_POS_X, y: DIALOG_POS_Y,
                    title: $"{action} {itemType}",
                    message: $"Are you sure you want to {action} {item.name} for {item.price:C} ?",
                    dialogType: DialogType.Question,
                    previousScreen: this,
                    onConfirm: () => {
                        
                        // Get dialog data
                        bool response = gameManager?.Exchange<T, U>(item) ?? false;
                        string message = response ? $"{item.name} was successfully {action}ed!" : $"You don't have enough money to {action} {item.name}!";
                        DialogType dialogType = response ? DialogType.Success : DialogType.Error;

                        // Build Dialog
                        DialogMessage dialogWarning = new(
                            x: DIALOG_POS_X, y: DIALOG_POS_Y,
                            message: message,
                            dialogType: dialogType,
                            previousScreen: this
                        );
                        dialogWarning.Show();

                    }, onCancel: () => { });

                dialogConfirmation.Show();
            };
        }


        // Methods for each selection direction (up, down, left, right)
        override public void SelectLeft()
        {
            if (menuMode == MenuMode.Up && selectedPosition > 0)
                selectedPosition--;
            else if (menuMode == MenuMode.Up)
                selectedPosition = topbar.options.Count;
        }


        override public void SelectRight()
        {
            if (menuMode == MenuMode.Up && selectedPosition < topbar.options.Count)
                selectedPosition++;
            else if (menuMode == MenuMode.Up)
                selectedPosition = 0;
        }
        

        override public void SelectDown()
        {
            if (menuMode == MenuMode.Down)
            {
                if (selectedPosition < options.Count - 1)
                    selectedPosition++;
                else
                {
                    menuMode = MenuMode.Up;
                    selectedPosition = 0;
                }
            }
            else
            {
                menuMode = MenuMode.Down;
                selectedPosition = 0;
            }
        }

        
        override public void SelectUp()
        {
            if (menuMode == MenuMode.Down)
            {
                if (selectedPosition > 0)
                    selectedPosition--;
                else
                {
                    menuMode = MenuMode.Up;
                    selectedPosition = 0;
                }
            }
            else
            {
                menuMode = MenuMode.Down;
                selectedPosition = 0;
            }
        }


        override public Option? SelectEnter()
        {
            if (menuMode == MenuMode.Down) 
            {
                if (!isSelectable) 
                    return Option.GetBackOption(previousScreen);
                else
                {
                    return (options.Count > 0) ? options[selectedPosition] : Option.GetBackOption(previousScreen);
                }
            }
            else
            {
                if (selectedPosition == topbar.options.Count)
                {
                    return Option.GetBackOption(previousScreen);
                }
                else
                    return topbar.options[selectedPosition];
            }
        }

    }
}
