using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorseManager2022.UI
{
    internal class Option
    {
        // Properties
        public string text { get; set; }
        public Screen? nextScreen { get; set; }
        public Action? onEnter { get; set; }

        // Constructor
        public Option(string text, Screen? nextScreen, Action? onEnter)
        {
            this.text = text;
            this.onEnter = onEnter;
            this.nextScreen = nextScreen;
        }

        static public Option GetBackOption(Screen? previousScreen, bool isInitialScreen = false)
        {
            string optionName = isInitialScreen ? "Exit" : "Back";
            return new Option(optionName, previousScreen, () => { });
        }
    }
}
