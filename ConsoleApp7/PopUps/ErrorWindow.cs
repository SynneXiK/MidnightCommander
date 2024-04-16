using ConsoleApp7.Buttons;
using ConsoleApp7.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp7.PopUps
{
	public class ErrorWindow : AppWindows
	{
        private string errName;
		private int Width;

		public ErrorWindow(Application application)
		{
			this.Application = application;
		}

		public void GetError(string input)
		{
			if(input.Length % 2 == 1)
				errName = input + " ";

			this.errName = input;
			this.Width = Math.Max(errName.Length + 2, 18);
			this.Draw();
		}

		public override void Draw() //└┘┼─┴├┤┬┌┐│
		{
			Console.ForegroundColor = ConsoleColor.White;
			Console.BackgroundColor = ConsoleColor.Red;

			DrawShell();
			

			
			int top = 11;
			Console.SetCursorPosition(50, top);
			Console.Write($"│   Error Window".PadRight(Width) + " │");
			top++;
			Console.SetCursorPosition(50, top);
			Console.Write($"│{string.Empty.PadRight(Width)}│");
			top++;
			Console.SetCursorPosition(50, top);
			Console.Write($"│ Error:".PadRight(Width) + " │");
			top++;
			Console.SetCursorPosition(50, top);
			Console.Write($"│ {errName}".PadRight(Width) + " │");
			top++;


			Console.ForegroundColor = ConsoleColor.Black;
			Console.BackgroundColor = ConsoleColor.White;

			Console.SetCursorPosition(50 + ((Width + 1) / 2) - 6, top + 1);
			Console.Write("┌────────────┐");
			Console.SetCursorPosition(50 + ((Width + 1) / 2) - 6, top + 2);
			Console.Write("│     OK     │");
			Console.SetCursorPosition(50 + ((Width + 1) / 2) - 6, top + 3);
			Console.Write("└────────────┘");

			Console.ForegroundColor = ConsoleColor.White;
			Console.BackgroundColor = ConsoleColor.Red;
		}

		public void DrawShell()
		{
			
			int top = 10;
			Console.SetCursorPosition(50, top);
			Console.Write("┌".PadRight(Width + 1, '─')+"┐"); // Tabulka 20x40
			top++;


			for (int i = 0; i < 11; i++)
			{
				Console.SetCursorPosition(50, top);
				Console.Write($"│{string.Empty.PadRight(Width)}│");
				top++;
			}

			Console.SetCursorPosition(50, top);
			Console.Write("└".PadRight(Width + 1, '─')+ "┘");
		}
		public override void HandleKey(ConsoleKeyInfo info)
		{
			if(info.Key == ConsoleKey.Enter)
			{
				this.Application.RemoveWindow();

				if (this.Application.windows.Count > 1)
					this.Application.RemoveWindow();
			}
				
		}
		public override void Refresh()
		{
			
		}
	}
}
