using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Final_Battle
{
    internal class PlayerInputSystem
    {
        public static int GetValidNumber(int min, int max, string OutSideOfRangeText = "")
        {
            int selection = -1;
            bool CorrectInput = false;
            while (!CorrectInput)
            {
                string? Input = Console.ReadLine();

                CorrectInput = int.TryParse(Input, out selection);

                if (selection < min || selection > max)
                    CorrectInput = false;
                if (!CorrectInput)
                    Console.WriteLine(OutSideOfRangeText);
            }

            return selection;
        }
    }
}
