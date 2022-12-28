using HorseManager2022.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace HorseManager2022.UI
{
    internal abstract class Screen : SelectableObject
    {
        // Properties
        protected Screen? previousScreen { get; set; }
        
        protected bool isInitialScreen
        {
            get
            {
                return previousScreen == null;
            }
        }
        
        
        // Constructor
        public Screen(Screen? previousScreen = null)
        {
            options = new List<Option>();
            this.previousScreen = previousScreen;
        }

        
        // Methods
        abstract public Screen? Show(GameManager? gameManager);

    }
}
