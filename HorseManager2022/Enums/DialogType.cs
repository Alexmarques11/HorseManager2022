using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorseManager2022.Enums
{
    public enum DialogType
    {
        Information = 0,
        Success = 1,
        Error = 2,
        Warning = 3,
        Question = 4
    }

    static class DialogTypeExtensions
    {
        public static ConsoleColor GetColor(this DialogType dialogType)
        {
            return dialogType switch
            {
                DialogType.Error => ConsoleColor.Red,
                DialogType.Warning => ConsoleColor.Yellow,
                DialogType.Question => ConsoleColor.Cyan,
                DialogType.Success => ConsoleColor.Green,
                DialogType.Information => ConsoleColor.Blue,
                _ => ConsoleColor.White
            };
        }

        public static string GetIcon(this DialogType dialogType)
        {
            return dialogType switch
            {
                DialogType.Error => "X",
                DialogType.Warning => "!",
                DialogType.Question => "?",
                DialogType.Success => "V",
                DialogType.Information => "i",
                _ => "i"
            };
        }
    }
}
