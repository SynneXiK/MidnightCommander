using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp7.TextEditor
{
	public class Editor : Components.AppWindows
	{
		FileOpener FileOpener;
		Keys keys;
		List<string> Rows = new List<string> { };
		string path;

		int fromTop = 0;
		int fromLeft = 0;

		string Modified = "-";

		int Count = Console.WindowHeight - 1;
		int offsetWidth = 0;
		int offsetHeight = 0;

		List<int> TopSearch = new List<int>() { -1};
		public string Value;

		public int startLeft;
		public int startTop;

		public Editor(string path, Application application)
		{
			this.path = path;
			FileOpener = new FileOpener(path);
			Rows = FileOpener.GetFiles(); 
			this.keys = new Keys(Rows, path, application, this.Modified);

			Console.Clear();
		}

		public override void Draw()
		{
			Console.CursorVisible = false;
			

			int top = 1;
			for (int i = offsetHeight; i < offsetHeight + Math.Min(Count, Rows.Count); i++)
			{
				Console.SetCursorPosition(0, top);
				if (keys.TopSearch.Contains(i))
					Console.BackgroundColor = ConsoleColor.DarkGreen;

				if (keys.Selecting == "B")
					DrawMarked(i);

				//aaaaabbbaaaaa
				else if (i == keys.FoundTop)
					DrawFound(i);
				else
				{
					//keys.Rows[i].Substring(j, this.OldValue.Length > keys.keys.Rows[i].Length - j ? 0 : this.OldValue.Length) == this.OldValue
					if ((i >= Rows.Count ? -1 : keys.Rows[i].Length) >= keys.offsetWidth) // připsat sem tu blbost s tou 0 
						Console.Write(keys.Rows[i].Substring(offsetWidth, Math.Min(keys.Rows[i].Length - offsetWidth, 119)).PadRight(120, ' ')); // nvm jestli tam má bejt 119 či 120
					else
						Console.Write(" ");

				}
				
				Console.BackgroundColor = ConsoleColor.DarkBlue;
				top++;
			}
			if (Console.WindowHeight > Rows.Count) // protože to bylo nějak divně rozbitý a psalo to na další řádek i když tam dělám substringy or smth idk
			{
				for (int i = 1; i < Console.WindowHeight - Rows.Count; i++)
				{
					Console.SetCursorPosition(0, Rows.Count + i);
					Console.Write(" ".PadRight(120, ' '));
				}
			}
			Console.CursorVisible = true;
			DrawTop();
			DrawBottom();
			Console.SetCursorPosition(keys.fromLeft, keys.fromTop + 1);
		}

		public override void Refresh()
		{
			this.Draw();
		}
		public void DrawMarked(int i)
		{
			if (keys.endTop == i && keys.startLeft != -1 || keys.fromTop == i && keys.startLeft != -1)
			{
				Console.Write(keys.Rows[i].Substring(0, keys.startLeft));
				Console.ForegroundColor = ConsoleColor.Yellow;
				Console.BackgroundColor = ConsoleColor.DarkGreen;
				Console.Write(keys.Rows[i].Substring(keys.startLeft, keys.endLeft));
				Console.ForegroundColor = ConsoleColor.White;
				Console.BackgroundColor = ConsoleColor.DarkBlue;
				Console.Write(keys.Rows[i].Substring(keys.endLeft, keys.Rows[i].Length - keys.endLeft));

			}
			
			if (i > keys.endTop && i < keys.startTop && keys.startLeft != -1 || i < keys.endTop && i > keys.startTop && keys.startLeft != -1) // když je mezi
			{
				
					Console.ForegroundColor = ConsoleColor.Yellow;
					Console.BackgroundColor = ConsoleColor.DarkGreen;
					Console.Write(keys.Rows[i]);
				

			}
			Console.ForegroundColor = ConsoleColor.White;
			Console.BackgroundColor = ConsoleColor.DarkBlue;
		}
		public void DrawFound(int i)
		{
			Console.Write(keys.Rows[i].Substring(0, keys.FoundLeft));
			Console.ForegroundColor = ConsoleColor.Yellow;
			Console.BackgroundColor = ConsoleColor.DarkGreen;
			Console.Write(keys.Rows[i].Substring(keys.FoundLeft, keys.Value.Length));
			Console.ForegroundColor = ConsoleColor.White;
			Console.BackgroundColor = ConsoleColor.DarkBlue;
			Console.Write(keys.Rows[i].Substring(keys.FoundLeft + keys.Value.Length, keys.Rows[i].Length - keys.FoundLeft - keys.Value.Length));
		}
		public void DrawBottom()
		{
			List<string> Functions = new List<string>() { "Help", "Save", "Mark", "Replac", "Copy", "Move", "Search", "Delete", "PullDn", "Quit" };
			Console.SetCursorPosition(0, Console.WindowHeight - 1);
			for (int i = 1; i < 11; i++)
			{
				Console.ForegroundColor = ConsoleColor.White;
				Console.BackgroundColor = ConsoleColor.Black;
				Console.Write($" {i}");
				Console.BackgroundColor = ConsoleColor.Cyan;
				Console.ForegroundColor = ConsoleColor.Black;
				Console.Write($"{Functions[i - 1]}     ");
			}
			Console.BackgroundColor = ConsoleColor.DarkBlue;
			Console.ForegroundColor = ConsoleColor.White;
		}
		public override void HandleKey(ConsoleKeyInfo info)
		{
			keys.HandleKey(info);

			this.Rows = keys.Rows;
			this.fromLeft = keys.fromLeft;
			this.fromTop = keys.fromTop;
			this.Modified = keys.Modified;
			this.offsetHeight = keys.offsetHeight;
			this.offsetWidth = keys.offsetWidth;
			this.startLeft = keys.startLeft;
			this.startTop = keys.startTop;
			//this.FoundLeft = keys.foundLeft;
			//this.FoundTop = keys.foundTop;
			//this.TopSearch = keys.buttons.TopSearch;
			//this.Value = keys.buttons.Value;

		}

		public void DrawTop() // Nezustava to konstantně nahoře, asi se to přepisuje prvním záznamem, nedovolit aby se psal na první pozici i guess
		{
			
			char chosenchar = Rows[fromTop].Length < 1 || Rows[fromTop].Length == fromLeft ? ' ' : Convert.ToChar(Rows[fromTop].Substring(fromLeft, 1));
			Console.SetCursorPosition(0 , 0);
			Console.Write(this.path);
			Console.Write("".PadRight(4));
			Console.Write($"[ {keys.Selecting}{keys.Modified}-- ]");
			Console.Write("".PadRight(4));
			Console.Write(keys.fromLeft + keys.offsetWidth);
			Console.Write("L:[   1+" + (keys.fromTop + keys.offsetWidth) +"/" + $" {keys.Rows.Count}");
			Console.Write("*(0 / 9427b)*"); // sam nevi co to je xd
			Console.Write($"   " + $"{Convert.ToInt32(chosenchar)}".PadRight(6));
			Console.Write("  " + HexConverter(Convert.ToInt32(chosenchar)).PadRight(48, ' '));  
			

		}

		public string HexConverter(int value)
		{
			StringBuilder sb = new StringBuilder();
			sb.AppendFormat("0x{0:X2} ", value);
			return sb.ToString();
		}


	}
}
