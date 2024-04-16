using ConsoleApp7.Buttons;
using ConsoleApp7.TextEditor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp7.EditorPopUps
{
	public class ReplaceButton : ComponentEditorButtons
	{
		public Application application { get; set; }

		public int Selected = 0;

		public string Value { get; set; } = "";

		public string OldValue { get; set; } = "";
		public string ValueRep { get; set; } = "";

		public string OldValueRep { get; set; } = "";

        public Keys keys { get; set; }

		public int foundTop { get; set; }
		public int foundLeft { get; set; }

		public List<int> TopSearch { get; set; } = new List<int>();
		public ReplaceButton(Application application, Keys keys) //  int fromTop, int fromLeft, Rows
        {
			Console.CursorVisible = false;
            this.keys = keys;
			this.application = application;
		}

		public void Function()
		{

		}
		public void ReplaceOne()
		{
			bool stop = false;
			if (keys.fromTop == foundTop)
				keys.fromTop++;

			for (int i = keys.fromTop; i < keys.Rows.Count - keys.fromTop; i++)
			{
				for (int j = 0; j < keys.Rows[i].Length; j++)
				{
					if (keys.Rows[i].Length - j - OldValue.Length >= 0 && stop != true)
					{
						if (keys.Rows[i].Substring(j, this.OldValue.Length > keys.Rows[i].Length - j ? 0 : this.OldValue.Length) == this.OldValue)
						{
							// Nebyl jsem si jistej jestli replace taky movuje na to kde jsem ale budiž
							keys.fromTop = i;
							keys.fromLeft = j;

							this.foundTop = i;
							this.foundLeft = j;

							stop = true;
							keys.Rows[i] = keys.Rows[i].Remove(j, OldValue.Length);
							keys.Rows[i] = keys.Rows[i].Insert(j, OldValueRep);
							keys.Modified = "M";
						}
					}

				}

			}

		}
		public void ReplaceAll()
		{
			for (int i = 0; i < keys.Rows.Count; i++)
			{
				for (int j = 0; j < keys.Rows[i].Length; j++)
				{
					if (keys.Rows[i].Substring(j, this.OldValue.Length > keys.Rows[i].Length - j ? 0 : this.OldValue.Length) == this.OldValue) // Házelo to jinak překrásný errorek ;)
					{
						keys.Rows[i] = keys.Rows[i].Remove(j, OldValue.Length);
						keys.Rows[i] = keys.Rows[i].Insert(j, OldValueRep);
						keys.Modified = "M";
					}
				}


			}
		}
		public void Draw() //└┘┼─┴├┤┬┌┐│
		{
			Console.ForegroundColor = ConsoleColor.White;
			Console.BackgroundColor = ConsoleColor.Gray;

			
			int top = 11;

			DrawShell();

			Console.SetCursorPosition(40, top);
			Console.Write($"│ Search Function".PadRight(46) + " │");
			top++;
			Console.SetCursorPosition(40, top);
			Console.Write($"│{string.Empty.PadRight(46)}│");
			top++;
			Console.SetCursorPosition(40, top);
			Console.Write($"│ Replace: ".PadRight(46) + " │");

			if (Selected == 0)
			{
				Console.ForegroundColor = ConsoleColor.Black;
				Console.BackgroundColor = ConsoleColor.White;
			}
			Console.SetCursorPosition(51, top);
			if (Value.Length <= 0)
			{
				Console.ForegroundColor = ConsoleColor.Yellow;

				if (OldValue.Length >= 18) // zase změna čísla
					Console.Write($"{OldValue.Substring(Value.Length - 18, 18)}".PadRight(18, '_')); // Aby si to nehralo s tim řádkem
				else
					Console.Write($"{OldValue}".PadRight(18, '_'));
			}
			else
			{
				if (Value.Length >= 18) // zase změna čísla
					Console.Write($"{Value.Substring(Value.Length - 18, 18)}".PadRight(18, '_')); // Aby si to nehralo s tim řádkem
				else
					Console.Write($"{Value}".PadRight(18, '_'));
			}
			Console.ForegroundColor = ConsoleColor.White;
			Console.BackgroundColor = ConsoleColor.Gray;

			top = top + 2;
			Console.SetCursorPosition(40, top);
			Console.Write($"│    With: │");
			if (Selected == 1)
			{
				Console.ForegroundColor = ConsoleColor.Black;
				Console.BackgroundColor = ConsoleColor.White;
			}


			Console.SetCursorPosition(51, top);
			if (ValueRep.Length <= 0)
			{
				Console.ForegroundColor = ConsoleColor.Yellow;

				if (OldValueRep.Length >= 18) // zase změna čísla
					Console.Write($"{OldValueRep.Substring(ValueRep.Length - 18, 18)}".PadRight(18, '_')); // Aby si to nehralo s tim řádkem
				else
					Console.Write($"{OldValueRep}".PadRight(18, '_'));
			}
			else
			{
				if (ValueRep.Length >= 18) // zase změna čísla
					Console.Write($"{ValueRep.Substring(ValueRep.Length - 18, 18)}".PadRight(18, '_')); // Aby si to nehralo s tim řádkem
				else
					Console.Write($"{ValueRep}".PadRight(18, '_'));
			}

			Console.ForegroundColor = ConsoleColor.White;
			Console.BackgroundColor = ConsoleColor.Gray;

			if (Selected == 2)
			{
				Console.ForegroundColor = ConsoleColor.Black;
				Console.BackgroundColor = ConsoleColor.White;
			}
			Console.SetCursorPosition(41, top + 6);
			Console.Write("┌─────────────┐");
			Console.SetCursorPosition(41, top + 7);
			Console.Write("│   Replace   │");
			Console.SetCursorPosition(41, top + 8);
			Console.Write("└─────────────┘");
			Console.SetCursorPosition(41, top);


			Console.ForegroundColor = ConsoleColor.White;
			Console.BackgroundColor = ConsoleColor.Gray;

			if (Selected == 3)
			{
				Console.ForegroundColor = ConsoleColor.Black;
				Console.BackgroundColor = ConsoleColor.White;
			}
			Console.SetCursorPosition(57, top + 6);
			Console.Write("┌─────────────┐");
			Console.SetCursorPosition(57, top + 7);
			Console.Write("│ Replace All │");
			Console.SetCursorPosition(57, top + 8);
			Console.Write("└─────────────┘");
			Console.ForegroundColor = ConsoleColor.White;
			Console.BackgroundColor = ConsoleColor.Gray;
			if (Selected == 4)
			{
				Console.ForegroundColor = ConsoleColor.Black;
				Console.BackgroundColor = ConsoleColor.White;
			}
			Console.SetCursorPosition(73, top + 6);
			Console.Write("┌────────────┐");
			Console.SetCursorPosition(73, top + 7);
			Console.Write("│   Cancel   │");
			Console.SetCursorPosition(73, top + 8);
			Console.Write("└────────────┘");
			Console.ForegroundColor = ConsoleColor.White;
			Console.BackgroundColor = ConsoleColor.DarkBlue;
		}


		public void HandleKey(ConsoleKeyInfo info)
		{
			if (info.Key == ConsoleKey.Tab)
			{
				if (Selected >= 4)
					Selected = 0;
				else
					Selected++;
			}
			if (info.Key == ConsoleKey.Enter)
			{
				OldValue = Value.Length <= 0 ? OldValue : Value;
				OldValueRep = ValueRep.Length <= 0 ? OldValueRep : ValueRep;

				switch (Selected)
				{
					case 2:
						ReplaceOne();

						break;

					case 3:
						ReplaceAll();
						break;

				}
				Console.CursorVisible = true;
				application.RemoveWindow();
				Value = "";
				Selected = 0;

			}
			string? input = info.Key.ToString();
			if (char.IsLetterOrDigit(info.KeyChar) || char.IsAscii(info.KeyChar) && input.Length < 2 || info.Key == ConsoleKey.Spacebar)
			{
				if (Selected == 0)
					Value += info.KeyChar;
				else if (Selected == 1)
					ValueRep += info.KeyChar;
			}


			if (info.Key == ConsoleKey.Backspace)
			{
				if (Selected == 0)
				{
					if (Value.Length > 0)
						Value = Value.Remove(Value.Length - 1);
					else
						OldValue = "";
				}

				else if (Selected == 1)
				{
					if (ValueRep.Length > 0)
						ValueRep = ValueRep.Remove(ValueRep.Length - 1);
					else
						OldValueRep = "";
				}
			}

		}
		public void DrawShell()
		{
			
			int top = 10;
			Console.SetCursorPosition(40, top);
			Console.Write("┌───────────────────────────────────────────────┐"); // Tabulka 20x40
			top++;


			for (int i = 0; i < 14; i++)
			{
				Console.SetCursorPosition(40, top);
				Console.Write($"│{string.Empty.PadRight(47)}│");
				top++;
			}

			Console.SetCursorPosition(40, top);
			Console.Write("└───────────────────────────────────────────────┘");
			//└┘┼─┴├┤┬┌┐│ 11 a 13
			top = 12;
			Console.SetCursorPosition(50, top);
			Console.Write("┌──────────────────┐");
			top++;
			Console.Write("│                  │"); // 11
			top++;
			Console.SetCursorPosition(50, top);
			Console.Write("├──────────────────┤");
			top++;
			Console.SetCursorPosition(50, top);
			Console.Write("│                  │"); // 13
			top++;
			Console.SetCursorPosition(50, top);
			Console.Write("└──────────────────┘");
		}
	}
}
