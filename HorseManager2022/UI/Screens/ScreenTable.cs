using HorseManager2022.Deprecated;
using HorseManager2022.Enums;
using HorseManager2022.UI.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorseManager2022.UI.Screens
{
    internal abstract class ScreenTable<T, U> : ScreenWithTopbar
    {
        // Properties
        protected Table<T, U> table;
        protected readonly bool isSelectable;
        private bool isAddable;

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

                table.selectedPosition = (isModeDown && (isSelectable || isAddable)) ? value : -1; // -1 means no selection

                base.selectedPosition = value;
            }
        }


        // Constructor
        public ScreenTable(Topbar topbar, string title, Screen? previousScreen = null, string[]? propertiesToExclude = null, bool isSelectable = false, bool isAddable = false)
            : base(topbar, previousScreen)
        {
            this.isSelectable = isSelectable;
            this.isAddable = isAddable;
            table = new Table<T, U>(title, propertiesToExclude ?? Array.Empty<string>(), isSelectable, isAddable);
        }


        // Methods
        virtual protected void SetTableOptions(GameManager? gameManager) { }
        virtual protected void SetAdditionalOptions(GameManager? gameManager) { }


        override public Screen? Show(GameManager? gameManager)
        {
            SetupOptions(gameManager);

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


        private void SetupOptions(GameManager? gameManager)
        {
            options.Clear();

            if (isSelectable)
                SetTableOptions(gameManager);

            if (isAddable)
                SetAdditionalOptions(gameManager);
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
                selectedPosition = options.Count - 1;
            }
        }


        override public Option? SelectEnter()
        {
            if (menuMode == MenuMode.Down)
            {
                if (!isSelectable && !isAddable)
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
