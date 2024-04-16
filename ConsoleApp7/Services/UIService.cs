using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp7.Services
{
    public static class UIService
    {
        public static void DrawButtonShell()
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
    }
}
