using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HorseManager2022.UI;

namespace HorseManager2022.Interfaces
{
    interface ISelectable
    {
        // Properties
        List<Option> options { get; set; }
        int selectedPosition { get; set; }

        // Methods
        void AddOption(string text, Screen? nextScreen, Action onEnter);
        void ClearOptions();
        Option WaitForOption(Action onWait);

        // Methods for each selection direction (up, down, left, right)
        void SelectLeft();
        void SelectRight();
        void SelectUp();
        void SelectDown();
        Option? SelectEnter();
        Option? SelectOption();
    }
}
