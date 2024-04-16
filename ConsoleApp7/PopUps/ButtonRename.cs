using ConsoleApp7.PopUps;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp7.Buttons
{
	internal class ButtonRename : ComponentButton
	{


		public int Selected = 0;
		public Application application { get; set; }

		public ErrorWindow ErrorWindow { get; set; }
		public ButtonRename(Application application, ErrorWindow ErrorWindow)
		{
			this.application = application;
			this.ErrorWindow = ErrorWindow;
		}

		public string Value { get; set; } = "";

		public void Function(string path, string path2)
		{
            DirectoryInfo newdir = new DirectoryInfo(path);
            path2 = newdir.Parent.FullName;

            if (Directory.Exists(path) && !Directory.Exists(path2 + Value))
			{
				Directory.Move(path, path2 + Value);
				application.RemoveWindow();
			}
				

			else if (File.Exists(path) && !File.Exists(path2 + Value))
			{
				File.Move(path, path2 + Value);
				application.RemoveWindow();
			}
				
			else
			{
				application.RemoveWindow();
				application.windows.Last().Refresh();
                application.AddWindow(ErrorWindow);
                ErrorWindow.GetError("File already exists");
			}

		}

		public void Draw(string path, string path2) //└┘┼─┴├┤┬┌┐│
		{
			Console.ForegroundColor = ConsoleColor.White;
			Console.BackgroundColor = ConsoleColor.Red;
			path = TextLength(path);

			DrawShell();

			string empty = "";
			int top = 11;
			Console.SetCursorPosition(50, top);
			Console.Write($"│ RENAME Function".PadRight(18) + " │");
			top++;
			Console.SetCursorPosition(50, top);
			Console.Write($"│{empty.PadRight(18)}│");
			top++;
			Console.SetCursorPosition(50, top);
			Console.Write($"│ Rename:".PadRight(18) + " │");
			top++;
			if (Selected == 0)
			{
				Console.ForegroundColor = ConsoleColor.Black;
				Console.BackgroundColor = ConsoleColor.White;
			}
			Console.SetCursorPosition(50, top);
			if (Value.Length >= 18)
				Console.Write($"│ {Value.Substring(Value.Length - 18, 16)}".PadRight(18, '_') + " │"); // Aby si to nehralo s tim řádkem
			else
			Console.Write($"│ {Value}".PadRight(18, '_') + " │"); // textbox

			top++;
			Console.ForegroundColor = ConsoleColor.White;
			Console.BackgroundColor = ConsoleColor.Red;

			if (Selected == 1)
			{
				Console.ForegroundColor = ConsoleColor.Black;
				Console.BackgroundColor = ConsoleColor.White;
			}
			Console.SetCursorPosition(51, top + 1);
			Console.Write("┌──────┐");
			Console.SetCursorPosition(51, top + 2);
			Console.Write("│  OK  │");
			Console.SetCursorPosition(51, top + 3);
			Console.Write("└──────┘");
			Console.ForegroundColor = ConsoleColor.White;
			Console.BackgroundColor = ConsoleColor.Red;
			if (Selected == 2)
			{
				Console.ForegroundColor = ConsoleColor.Black;
				Console.BackgroundColor = ConsoleColor.White;
			}
			Console.SetCursorPosition(61, top + 1);
			Console.Write("┌──────┐");
			Console.SetCursorPosition(61, top + 2);
			Console.Write("│Cancel│");
			Console.SetCursorPosition(61, top + 3);
			Console.Write("└──────┘");
			Console.ForegroundColor = ConsoleColor.White;
			Console.BackgroundColor = ConsoleColor.DarkBlue;
		}
		public void HandleKey(ConsoleKeyInfo info, string path, string path2)
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
				if (Selected == 1 || Selected == 0)
					Function(path, path2);

				else if(Selected == 2)
					application.RemoveWindow();


				Value = "";
				Selected = 0;
			}

			if (Char.IsLetterOrDigit(info.KeyChar) && Selected == 0)
				Value += info.KeyChar;

			if (info.Key == ConsoleKey.Backspace && Selected == 0 && Value.Length > 0)
				Value = Value.Remove(Value.Length - 1);

		}
		public string TextLength(string input)
		{
			if (input.Length >= 18)
				input = @"..\" + input.Substring(input.Length - 13, 13);

			return input;
		}
		public void DrawShell()
		{
			string empty = "";
			int top = 10;
			Console.SetCursorPosition(50, top);
			Console.Write("┌──────────────────┐"); // Tabulka 20x40
			top++;


			for (int i = 0; i < 11; i++)
			{
				Console.SetCursorPosition(50, top);
				Console.Write($"│{empty.PadRight(18)}│");
				top++;
			}

			Console.SetCursorPosition(50, top);
			Console.Write("└──────────────────┘");
		}


	}
}
