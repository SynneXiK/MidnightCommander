using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp7.Helpers
{
    public static class ButtonHelper
    {
        public static void DrawShell()
        {
            int top = 10;
            Console.SetCursorPosition(50, top);
            Console.Write("┌──────────────────┐"); // Tabulka 20x40
            top++;


            for (int i = 0; i < 11; i++)
            {
                Console.SetCursorPosition(50, top);
                Console.Write($"│{string.Empty.PadRight(18)}│");
                top++;
            }

            Console.SetCursorPosition(50, top);
            Console.Write("└──────────────────┘");
        }
        public static string TextLength(string input)
        {
            if (input.Length >= 18)
                input = @"..\" + input.Substring(input.Length - 13, 13);

            return input;
        }
    }
}
