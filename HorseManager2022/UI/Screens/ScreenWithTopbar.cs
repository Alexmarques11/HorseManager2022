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
                if (this.selectedPosition == this.options.Count)
                {
                    return Option.GetBackOption(this.previousScreen);
                }
                else
                    return this.options[this.selectedPosition];
            }
            else
            {
                if (this.selectedPosition == this.topbar.options.Count)
                {
                    return Option.GetBackOption(this.previousScreen);
                }
                else
                    return this.topbar.options[this.selectedPosition];

            }
        }

    }
}
