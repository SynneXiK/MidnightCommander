using ConsoleApp7.PopUps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp7.Buttons
{
	internal class ButtonCreate : ComponentButton
	{
		public int Selected = 0;
		public Application application { get; set; }

		public ErrorWindow ErrorWindow { get; set; }
		public ButtonCreate(Application application, ErrorWindow ErrorWindow)
		{
			this.application = application;
			this.ErrorWindow = ErrorWindow;
		}

		public string Value { get; set; } = "";

		public void Function(string path, string path2)
		{
            DirectoryInfo newdir = new DirectoryInfo(path);
            path = newdir.Parent.FullName;
            //path = path.Remove(path.LastIndexOf(@"\"), path.Length - path.LastIndexOf(@"\"));

			if (File.Exists(path + @"\" + Value) || Directory.Exists(path + @"\" + Value))
			{
                application.AddWindow(ErrorWindow);
                ErrorWindow.GetError("File or Directory already exists");
				return;
			}
			else if (Selected == 1)
			{
				File.Create(path + @"\" + Value);
				application.RemoveWindow();
			}	
				

			else if (Selected == 2)
			{
				Directory.CreateDirectory(path + @"\" + Value);
				application.RemoveWindow();
			}
				
			
				

			
		}

		public void Draw(string path, string path2) //└┘┼─┴├┤┬┌┐│
		{
			Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Red;
            path = TextLength(path);

			string empty = "";
			int top = 11;
			
			DrawShell();

			Console.SetCursorPosition(40, top);
			Console.Write($"│ CREATE Function".PadRight(46) + " │");
			top++;
			Console.SetCursorPosition(40, top);
			Console.Write($"│{empty.PadRight(46)}│");
			top++;
			Console.SetCursorPosition(40, top);
			Console.Write($"│ CREATE:".PadRight(46) + " │");

			if (Selected == 0)
			{
				Console.ForegroundColor = ConsoleColor.Black;
				Console.BackgroundColor = ConsoleColor.White;
			}
			Console.SetCursorPosition(50, top);
			if (Value.Length >= 18)
				Console.Write($"{Value.Substring(Value.Length - 18, 18)}".PadRight(18, '_')); // Aby si to nehralo s tim řádkem
			else
				Console.Write($"{Value}".PadRight(18, '_'));

			Console.ForegroundColor = ConsoleColor.White;
			Console.BackgroundColor = ConsoleColor.Red;

			if (Selected == 1)
			{
				Console.ForegroundColor = ConsoleColor.Black;
				Console.BackgroundColor = ConsoleColor.White;
			}
			Console.SetCursorPosition(41, top + 3);
			Console.Write("┌────────────┐");
			Console.SetCursorPosition(41, top + 4);
			Console.Write("│ Make File  │");
			Console.SetCursorPosition(41, top + 5);
			Console.Write("└────────────┘");
			Console.SetCursorPosition(41, top);


			Console.ForegroundColor = ConsoleColor.White;
			Console.BackgroundColor = ConsoleColor.Red;

			if (Selected == 2)
			{
				Console.ForegroundColor = ConsoleColor.Black;
				Console.BackgroundColor = ConsoleColor.White;
			}
			Console.SetCursorPosition(57, top + 3);
			Console.Write("┌────────────┐");
			Console.SetCursorPosition(57, top + 4);
			Console.Write("│  Make Dir  │");
			Console.SetCursorPosition(57, top + 5);
			Console.Write("└────────────┘");
			Console.ForegroundColor = ConsoleColor.White;
			Console.BackgroundColor = ConsoleColor.Red;
			if (Selected == 3)
			{
				Console.ForegroundColor = ConsoleColor.Black;
				Console.BackgroundColor = ConsoleColor.White;
			}
			Console.SetCursorPosition(73, top + 3);
			Console.Write("┌────────────┐");
			Console.SetCursorPosition(73, top + 4);
			Console.Write("│   Cancel   │");
			Console.SetCursorPosition(73, top + 5);
			Console.Write("└────────────┘");
			Console.ForegroundColor = ConsoleColor.White;
			Console.BackgroundColor = ConsoleColor.DarkBlue;
		}
		
	
		public void HandleKey(ConsoleKeyInfo info, string path, string path2)
		{
			if (info.Key == ConsoleKey.Tab)
			{
				if (Selected >= 3)
					Selected = 0;
				else
					Selected++;
			}
			if (info.Key == ConsoleKey.Enter)
			{
				if (Selected == 1 || Selected == 2)
					Function(path, path2);

				else if(Selected == 3)
					application.RemoveWindow();
					

				Selected = 0;
				Value = "";
			}
			if (Char.IsLetterOrDigit(info.KeyChar) && Selected == 0 || info.KeyChar == '.' && Selected == 0)
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
