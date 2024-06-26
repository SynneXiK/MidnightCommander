﻿using ConsoleApp7.Helpers;
using ConsoleApp7.PopUps;
using ConsoleApp7.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp7.Buttons
{
    internal class ButtonCopy : ComponentButton
	{
		public int Selected = 0;
		public Application application { get; set; }

		public ErrorWindow ErrorWindow { get; set; }
		public ButtonCopy(Application application, ErrorWindow ErrorWindow)
		{
			this.application = application;
			this.ErrorWindow = ErrorWindow;
		}
		public void Function(string path, string path2)
		{
            // Bude tady to stejný jako v Move, až to ale bude fungovat mezi disky
            
				try
				{
					if (File.Exists(path))
						File.Copy(path, path2 + path.Substring(path.LastIndexOf(@"\"), path.Length - path.LastIndexOf(@"\")));
					if (Directory.Exists(path))
						Directory.CreateDirectory(path2 + path.Substring(path.LastIndexOf(@"\"), path.Length - path.LastIndexOf(@"\")));
					
					application.RemoveWindow();
				}
				catch (UnauthorizedAccessException)
				{
					application.RemoveWindow();
					application.windows.Last().Refresh();
					application.AddWindow(ErrorWindow);
					ErrorWindow.GetError("Access denied!");
				}
				catch (IOException)
				{
					application.RemoveWindow();
					application.windows.Last().Refresh();
					application.AddWindow(ErrorWindow);
					ErrorWindow.GetError("File or Directory already exists!");
				}
				//            DirectoryInfo dir = new DirectoryInfo(path);
				//            while (dir.GetDirectories() != null || dir.GetFiles() != null)
				//{
				//                DirectoryInfo newdir = new DirectoryInfo(path);
				//                path = newdir.Parent.FullName;
				//                foreach (DirectoryInfo item in dir.GetDirectories())
				//                {
				//		Directory.CreateDirectory(path  + @"\" + item.FullName);
				//                }
				//                foreach (FileInfo item in dir.GetFiles())
				//                {
				//		File.Copy(path, path + @"\" + item.FullName);
				//	}
				//            }
			

            try
			{
                if (File.Exists(path))
                    File.Copy(path, path2 + path.Substring(path.LastIndexOf(@"\"), path.Length - path.LastIndexOf(@"\")));

                application.RemoveWindow();
            }
			catch (UnauthorizedAccessException)
			{
				application.RemoveWindow();
                application.windows.Last().Refresh();
				application.AddWindow(ErrorWindow);
				ErrorWindow.GetError("Access denied!");
			}
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
			for (int i = 11; i < 17; i++) // top
			{

			}
			Console.SetCursorPosition(50, top);
			Console.Write($"│   COPY Function".PadRight(18) + " │");
			top++;
			Console.SetCursorPosition(50, top);
			Console.Write($"│{string.Empty.PadRight(18)}│");
			top++;
			Console.SetCursorPosition(50, top);
			Console.Write($"│ Copy:".PadRight(18) + " │");
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
			ColorHelper.DefaultBackground();
		}
		public void HandleKey(ConsoleKeyInfo info, string path, string path2)
		{
			if (info.Key == ConsoleKey.Tab)
			{
				Selected = Selected >= 1 ? 0 : Selected++;
			}
			if (info.Key == ConsoleKey.Enter)
			{
				if (Selected == 0)
				{
					Function(path, path2);
				}

				else
					application.RemoveWindow();

				Selected = 0;
			}
		}
	}
}
