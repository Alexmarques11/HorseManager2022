using HorseManager2022.Enums;
using HorseManager2022.Models;
using HorseManager2022.UI.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorseManager2022.UI
{
    internal abstract class ScreenWithTopbar : Screen
    {
        // Properties
        public MenuMode menuMode;
        protected Topbar topbar;

        public bool isModeDown => menuMode == MenuMode.Down;
        public bool isModeUp => menuMode == MenuMode.Up;

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
                base.selectedPosition = value;
            }
        }


        // Constructor
        public ScreenWithTopbar(Topbar topbar, Screen? previousScreen = null)
            : base(previousScreen)
        {
            this.topbar = topbar;
            menuMode = MenuMode.Down;
        }


        public override Screen? Show(GameManager? gameManager)
        {
            base.Show(gameManager);
            menuMode = MenuMode.Down;
            return null;
        }


        // Methods for each selection direction (up, down, left, right)
        override public void SelectLeft()
        {
            if (this.selectedPosition > 0)
                this.selectedPosition--;
            else
            {
                if (menuMode == MenuMode.Down)
                    this.selectedPosition = this.options.Count - 1;
                else
                    this.selectedPosition = topbar.options.Count;
            }
        }
        
        
        override public void SelectRight()
        {
            if (menuMode == MenuMode.Down && this.selectedPosition < this.options.Count - 1
                        || menuMode == MenuMode.Up && this.selectedPosition < this.topbar.options.Count)
                this.selectedPosition++;
            else
                this.selectedPosition = 0;
        }

        
        override public void SelectUp()
        {
            menuMode = (menuMode == MenuMode.Up) ? MenuMode.Down : MenuMode.Up;
            this.selectedPosition = 0;
        }
        
        
        override public void SelectDown() => SelectUp();

        
        override public Option? SelectEnter()
        {
            if (menuMode == MenuMode.Down)
            {
                if (selectedPosition == options.Count)
                {
                    return Option.GetBackOption(previousScreen);
                }
                else
                    return options[selectedPosition];
            }
            else
            {
                if (selectedPosition == topbar.options.Count)
                {
                    return Option.GetBackOption(previousScreen);
                }
                else {

                    if (topbar.options[selectedPosition].text == "Sleep") 
                    { 
                        topbar.options[selectedPosition].nextScreen = this;
                    }
                    else if (topbar.options[selectedPosition].text == "Calendar")
                    {
                        if (topbar.options[selectedPosition].nextScreen != null)
                            topbar.options[selectedPosition].nextScreen!.previousScreen = this;
                    }
                    
                    return topbar.options[selectedPosition];
                }

            }
        }

    }
}
