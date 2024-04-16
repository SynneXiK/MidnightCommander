using ConsoleApp7.Buttons;
using ConsoleApp7.TextEditor;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp7.EditorPopUps
{
    internal class ButtonExitEditor : ComponentEditorButtons
	{


		public int Selected = 0;
        public Application application { get; set; }
        public Keys keys;


        string path;

		public string Value { get; set; }

		public ButtonExitEditor(Application Application, string path, Keys keys)
        {
            application = Application;
            this.path = path;
            this.keys = keys;

        }

        public void Function()
        {
            switch (Selected)
            {
                case 0: // Save
                    FileFinisher Finisher = new FileFinisher(keys.Rows, path);
                    Finisher.ExportFiles();
                    keys.Modified = "-";
                    application.RemoveWindow();
                    Console.CursorVisible = false;
                    break;

                case 1: // Don't Save
                    application.RemoveWindow(); // aby to vyplo oba dva
                    keys.Modified = "-";
                    Console.CursorVisible = false;
                    break;

            }
            application.RemoveWindow(); // Dělá i cancel
            Selected = 0;
        }

        public void Draw() //└┘┼─┴├┤┬┌┐│
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Red;

            DrawShell();

            string empty = "";
            int top = 11;
            Console.SetCursorPosition(40, top);
            Console.Write($"│ EXIT".PadRight(46) + " │");
            top++;
            Console.SetCursorPosition(40, top);
            Console.Write($"│{empty.PadRight(46)}│");
            top++;
            Console.SetCursorPosition(40, top);
            Console.Write($"│ Do you want to save the file: ".PadRight(46) + " │");
            top++;
            if (Selected == 0)
            {
                Console.ForegroundColor = ConsoleColor.Black;
                Console.BackgroundColor = ConsoleColor.White;
            }
            Console.SetCursorPosition(41, top + 1);
            Console.Write("┌────────────┐");
            Console.SetCursorPosition(41, top + 2);
            Console.Write("│    Save    │");
            Console.SetCursorPosition(41, top + 3);
            Console.Write("└────────────┘");
            Console.SetCursorPosition(41, top);


            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Red;

            if (Selected == 1)
            {
                Console.ForegroundColor = ConsoleColor.Black;
                Console.BackgroundColor = ConsoleColor.White;
            }
            Console.SetCursorPosition(57, top + 1);
            Console.Write("┌────────────┐");
            Console.SetCursorPosition(57, top + 2);
            Console.Write("│ Don't Save │");
            Console.SetCursorPosition(57, top + 3);
            Console.Write("└────────────┘");
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Red;
            if (Selected == 2)
            {
                Console.ForegroundColor = ConsoleColor.Black;
                Console.BackgroundColor = ConsoleColor.White;
            }
            Console.SetCursorPosition(73, top + 1);
            Console.Write("┌────────────┐");
            Console.SetCursorPosition(73, top + 2);
            Console.Write("│   Cancel   │");
            Console.SetCursorPosition(73, top + 3);
            Console.Write("└────────────┘");
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.DarkBlue;
        }
        public void HandleKey(ConsoleKeyInfo info)
        {
            if (info.Key == ConsoleKey.Tab)
            {
                if (Selected >= 2)
                    Selected = 0;
                else
                    Selected++;
            }
            if (info.Key == ConsoleKey.Enter)
            {
                Function();

                Selected = 0;
            }
        }
        public void Refresh()
        {
            Draw();
        }

        public void DrawShell()
        {
            string empty = "";
            int top = 10;
            Console.SetCursorPosition(40, top);
            Console.Write("┌──────────────────────────────────────────────┐"); // Tabulka 20x40
            top++;


            for (int i = 0; i < 8; i++)
            {
                Console.SetCursorPosition(40, top);
                Console.Write($"│{empty.PadRight(46)}│");
                top++;
            }

            Console.SetCursorPosition(40, top);
            Console.Write("└──────────────────────────────────────────────┘");
        }
    }
}
