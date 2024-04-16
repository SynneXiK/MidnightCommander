using ConsoleApp7.PopUps;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp7.Buttons
{
	internal class ButtonDelete : ComponentButton
	{
		public int Selected = 0;
		public Application application { get; set; }

		public ErrorWindow ErrorWindow { get; set; }
		public ButtonDelete(Application application, ErrorWindow ErrorWindow)
		{
			this.application = application;
			this.ErrorWindow = ErrorWindow;
		}

		public void Function(string path, string path2)
		{
			try
			{
                if (Directory.Exists(path))
            		Directory.Delete(path, true);

                else if (File.Exists(path))
					File.Delete(path);
            }
            catch (Exception)
			{
				ErrorWindow.GetError("File or Directory doesnt exist");
				throw;
			}
			//if (Directory.Exists(path))
			//	Directory.Delete(path, true);

			//else if (File.Exists(path))
			//	File.Delete(path);
			// JÁ TOHLE RISKOVAT FAKT NEBUDU

		}

		public void Draw(string path, string path2) //└┘┼─┴├┤┬┌┐│
		{
			Console.ForegroundColor = ConsoleColor.White;
			Console.BackgroundColor = ConsoleColor.Red;
			path = TextLength(path);
			path2 = TextLength(path2);

			DrawShell();

			string empty = "";
			int top = 11;
			Console.SetCursorPosition(50, top);
			Console.Write($"│ DELETE Function".PadRight(18) + " │");
			top++;
			Console.SetCursorPosition(50, top);
			Console.Write($"│{empty.PadRight(18)}│");
			top++;
			Console.SetCursorPosition(50, top);
			Console.Write($"│ Delete:".PadRight(18) + " │");
			top++;
			Console.SetCursorPosition(50, top);
			Console.Write($"│ {path}".PadRight(18) + " │");
			top++;


			if (Selected == 0)
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
			if (Selected == 1)
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
				if (Selected >= 1)
					Selected = 0;
				else
					Selected++;
			}
			if (info.Key == ConsoleKey.Enter)
			{
				if (Selected == 0)
				{
					Function(path, path2);
					application.RemoveWindow();
				}

				else
					application.RemoveWindow();

				Selected = 0;
			}
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
