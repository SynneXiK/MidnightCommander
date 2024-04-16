using ConsoleApp7.PopUps;
using ConsoleApp7.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp7.Buttons
{
	internal class ButtonMove : ComponentButton
	{
		public int Selected = 0;
		public Application application { get; set; }
		public ErrorWindow ErrorWindow { get; set; }
		public ButtonMove(Application application, ErrorWindow ErrorWindow)
		{
			this.application = application;
			this.ErrorWindow = ErrorWindow;
		}

		public void Function(string path, string path2)
		{
			if (Directory.Exists(path))
				Directory.Move(path, path2 + path.Substring(path.LastIndexOf(@"\"), path.Length - path.LastIndexOf(@"\")));

			//else if (File.Exists(path))
			//	File.Move(path, path2 + path.Substring(path.LastIndexOf(@"\"), path.Length - path.LastIndexOf(@"\")));

			// Funguje na stejném disku, ale jenom to

			//if (Directory.Exists(path)) 
			//{
			//	Directory.CreateDirectory(path2 + path.Substring(path.LastIndexOf(@"\"), path.Length - path.LastIndexOf(@"\")));
			//	DirectoryInfo directoryInfo = new DirectoryInfo(path);
			//	foreach (FileInfo item in directoryInfo.GetFiles())
			//	{
			//		File.Copy(path + @"\" + item, path2 + path.Substring(path.LastIndexOf(@"\"), path.Length - path.LastIndexOf(@"\"))+ @"\" + item);
			//	}
			//	File.Copy(path, path2 + path.Substring(path.LastIndexOf(@"\"), path.Length - path.LastIndexOf(@"\")));
			//	Directory.Delete(path);
			//}

			else if (File.Exists(path))
			{
				File.Copy(path, path2 + path.Substring(path.LastIndexOf(@"\"), path.Length - path.LastIndexOf(@"\")));
				File.Delete(path);
			}
			
			// POKUS O MEZI DISKY ALE NECHTĚL JSEM RISKOVAT, je tam delete


			//else if (File.Exists(path))
			//	File.Move(path, path2 + path.Substring(path.LastIndexOf(@"\"), path.Length - path.LastIndexOf(@"\")));
			//UnauthorizedAccessException

		}

		public void Draw(string path, string path2) //└┘┼─┴├┤┬┌┐│
		{
			Console.ForegroundColor = ConsoleColor.White;
			Console.BackgroundColor = ConsoleColor.Red;
			path = ButtonHelper.TextLength(path);
			path2 = ButtonHelper.TextLength(path2);

			UIService.DrawButtonShell();

			
			int top = 11;
			Console.SetCursorPosition(50, top);
			Console.Write($"│   MOVE Function".PadRight(18) + " │");
			top++;
			Console.SetCursorPosition(50, top);
			Console.Write($"│{string.Empty.PadRight(18)}│");
			top++;
			Console.SetCursorPosition(50, top);
			Console.Write($"│ Move:".PadRight(18) + " │");
			top++;
			Console.SetCursorPosition(50, top);
			Console.Write($"│ {path}".PadRight(18) + " │");
			top++;
			Console.SetCursorPosition(50, top);
			Console.Write($"│ To:".PadRight(18) + " │");
			top++;
			Console.SetCursorPosition(50, top);
			Console.Write($"│ {path2}".PadRight(18) + " │");
			top++;

			if (Selected == 0)
			{
				Console.ForegroundColor = ConsoleColor.Black;
				Console.BackgroundColor = ConsoleColor.White;
			}
			Console.SetCursorPosition(51, top+1);
			Console.Write("┌──────┐");
			Console.SetCursorPosition(51, top+2);
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
				}

				application.RemoveWindow();
				Selected = 0;
			}
		}
	}
}
