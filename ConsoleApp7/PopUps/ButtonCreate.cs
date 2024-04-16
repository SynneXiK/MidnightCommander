using ConsoleApp7.PopUps;
using ConsoleApp7.Services;
using ConsoleApp7.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp7.Buttons
{
	public class ButtonCreate : ComponentButton
	{
		public int Selected = 0;
		public Application application { get; set; }

		public ErrorWindow ErrorWindow { get; set; }
		public ButtonCreate(Application application, ErrorWindow ErrorWindow)
		{
			this.application = application;
			this.ErrorWindow = ErrorWindow;
		}

		private string Value = "";

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
            path = ButtonHelper.TextLength(path);

			int top = 11;

			UIService.DrawButtonShell();

			Console.SetCursorPosition(40, top);
			Console.Write($"│ CREATE Function".PadRight(46) + " │");
			top++;
			Console.SetCursorPosition(40, top);
			Console.Write($"│{string.Empty.PadRight(46)}│");
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
				Selected = Selected >= 3 ? 0 : Selected++;
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
			if ((Char.IsLetterOrDigit(info.KeyChar) || info.KeyChar == '.') && Selected == 0)
				Value += info.KeyChar;

			if (info.Key == ConsoleKey.Backspace && Selected == 0 && Value.Length > 0)
				Value = Value.Remove(Value.Length - 1);

			
		}
	}	
}
