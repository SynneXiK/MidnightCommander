using ConsoleApp7.EditorPopUps;
using ConsoleApp7.PopUps;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp7.TextEditor
{
	public class Keys
	{
		Application application;
		public int fromTop = 0;
		public int fromLeft = 0;
		public int CurrentTop;
		public int CurrentLeft;

		public string Modified = "-";
		public string Selecting = "-";

		public List<string> Rows;
		string path;

		public EditorButtons buttons;
		public SelectionButton Mark;
		public Editor editor;

		public int offsetWidth = 0;
		public int offsetHeight = 0;

		public List<int> TopSearch = new List<int>();

		public int FoundTop = -1;
		public int FoundLeft = -1;

		public string Value;

		public int startLeft = -1;
		public int startTop = -1;

		public int endLeft = -1;
		public int endTop = -1;

		public Keys(List<string> Rows, string path, Application Application,string Modified)
		{
			this.Rows = Rows;
			this.path = path;
			this.application = Application;
			this.Modified = Modified;
            this.buttons = new EditorButtons(Application, this, path);
		}


		public void HandleKey(ConsoleKeyInfo info)
		{
			CurrentTop = offsetHeight + fromTop;
			CurrentLeft = offsetWidth + fromLeft;
			switch (info.Key)
			{
				case ConsoleKey.UpArrow:
					CheckMovementUp();
					break;

				case ConsoleKey.DownArrow:
					CheckMovementDown();
					break;

				case ConsoleKey.LeftArrow:
					CheckMovementLeft();
					break;

				case ConsoleKey.RightArrow:
					CheckMovementRight();
					break;

				case ConsoleKey.F10:
					if (Modified == "-")
					{
						this.application.RemoveWindow();
						Console.CursorVisible = false;
					}
					else
					{
						buttons.ButtonSelected = 10;
						application.AddWindow(buttons);
					}
					break;

				case ConsoleKey.F2:
					Save();
					break;

				case ConsoleKey.Backspace:
					CheckBackspace();
					break;

				case ConsoleKey.Delete:
					CheckDeleteKey();
					break;

				case ConsoleKey.PageUp:
					CheckPageUp();
					break;

				case ConsoleKey.PageDown:
					CheckPageDown();
					break;

				case ConsoleKey.Home:
					fromLeft = 0;
					offsetWidth = 0;
					break;

				case ConsoleKey.End: // Už jenom tohle fixnout a DrawTop v Editoru!! 
					fromLeft = Rows[fromTop].Length <= 119 ? Rows[fromTop].Length : 119 - Rows[fromTop].Length;
					offsetWidth = Rows[fromTop].Length <= 119 ? 0 : Rows[fromTop].Length - 119;
					break;

				case ConsoleKey.Enter:
					CheckEnterKey();
					break;


				default:
					string? input = info.Key.ToString();
					if(input.Length == 2 && input[0] == 'F')
					{
						buttons.ButtonSelected = Convert.ToInt32(input[1].ToString());
						application.AddWindow(buttons);
					}
					else if (Char.IsLetterOrDigit(info.KeyChar) || Char.IsAscii(info.KeyChar) && input.Length < 2 || info.Key == ConsoleKey.Spacebar)
					// Aby mi funkce jako F2 neproběhli kod a nedali left++
					{
						Rows[fromTop + offsetHeight] = Rows[fromTop + offsetHeight].Substring(0, fromLeft) + info.KeyChar + Rows[fromTop + offsetHeight].Substring(fromLeft, Rows[fromTop + offsetHeight].Length - fromLeft);
						this.Modified = "M";
						fromLeft++;
					}
					break;

			}
		}
        
        public void Save()
		{
			this.Modified = "-";
			FileFinisher Finisher = new FileFinisher(this.Rows, path);
			Finisher.ExportFiles();
		}


		public void CheckEnterKey()
		{
			Rows.Insert(CurrentTop + 1, Rows[CurrentTop].Substring(fromLeft, Rows[CurrentTop].Length - fromLeft));
			Rows[CurrentTop] = Rows[CurrentTop].Substring(0, fromLeft);

			fromLeft = 0;
			fromTop++;
			this.Modified = "M";
		}

		public void CheckMovementDown()
		{
			if (fromTop == Console.WindowHeight - 3 && offsetHeight <= Rows.Count)
			{
				offsetHeight++;
				this.Rows.Add("");
				return;
			}

			if (fromTop + 1 == Rows.Count)
				this.Rows.Add("");

			if (fromLeft >= Rows[CurrentTop + 1].Length)
				fromLeft = Rows[CurrentTop + 1].Length;

			if (offsetWidth >= Rows[CurrentTop + 1].Length && fromLeft != Rows.Count) // fromleft se nemůže rovnat rows count wtf
				offsetWidth = Rows[CurrentTop + 1].Length;


			fromTop++;

        }
		public void CheckMovementUp()
		{
			if (fromTop == 0 && offsetHeight > 0)
			{
				offsetHeight--;
				return;
			}
			if (fromTop <= 0)
				return;

			if (offsetWidth >= Rows[CurrentTop - 1].Length)
				offsetWidth = Rows[CurrentTop - 1].Length;

			if (fromLeft >= Rows[CurrentTop - 1].Length)
				fromLeft = Rows[CurrentTop - 1].Length;


			fromTop--;
			


		}

		public void CheckMovementLeft()
		{
			if (fromLeft == 0 && offsetWidth > 0)
			{
				offsetWidth--;
				return;
			}

			else if (fromLeft > 0)
				fromLeft--;
		}

		public void CheckMovementRight()
		{
			if(fromLeft == Console.WindowWidth - 1 && offsetWidth <= Rows[fromTop].Length)
			{
				offsetWidth++;
				return;
			}

			if (fromLeft >= Rows[fromTop + offsetHeight].Length)
				fromLeft = Rows[fromTop + offsetHeight].Length;
			else
			fromLeft++;

		}

		public void CheckBackspace()
		{
			if (fromLeft == 0)
			{
                fromLeft = Rows[CurrentTop - 1].Length;
                Rows[CurrentTop - 1] = Rows[CurrentTop - 1] + Rows[CurrentTop];
				for (int i = CurrentTop; i < Rows.Count - CurrentTop; i++)
				{
					Rows[i] = Rows[i + 1];
				}
				Rows.Remove(Rows.Last());
				fromTop--;
			}
			else
			{
				Rows[CurrentTop] = Rows[CurrentTop].Substring(0, fromLeft - 1) + Rows[CurrentTop].Substring(fromLeft, Rows[CurrentTop].Length - fromLeft);
				fromLeft--;
			}
			this.Modified = "M";
		}
		public void CheckDeleteKey()
		{
			if (fromTop == Rows.Count && fromLeft == Rows[CurrentTop].Length)
				return;
			if (fromLeft == Rows[CurrentTop].Length)
			{
				Rows[CurrentTop] = Rows[CurrentTop] + Rows[CurrentTop + 1];
				for (int i = CurrentTop + 1; i < Rows.Count - CurrentTop; i++)
				{
					if (i != Rows.Count - 1)
					Rows[i] = Rows[i + 1];
				}
				Rows.Remove(Rows.Last());
			}
			else
			Rows[CurrentTop] = Rows[CurrentTop].Substring(0, fromLeft) + Rows[CurrentTop].Substring(fromLeft + 1, Rows[CurrentTop].Length - fromLeft - 1);
			this.Modified = "M";
		}

		public void CheckPageUp()
		{
			if (fromTop - offsetHeight - Console.WindowHeight - 2 >= 0)
				offsetHeight -= Console.WindowHeight - 2;

			if (offsetHeight <= 0)
				offsetHeight = 0;
		}
		public void CheckPageDown()
		{
			if (fromTop + offsetHeight + Console.WindowHeight - 2<= Rows.Count)
				offsetHeight += Console.WindowHeight - 2;

			if (offsetHeight >= Rows.Count)
				offsetHeight = Rows.Count - Console.WindowHeight;

			
		}

	}
}
