using ConsoleApp7.Buttons;
using ConsoleApp7.Components;
using ConsoleApp7.TextEditor;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp7.EditorPopUps
{
    public class SearchPopup : ComponentEditorButtons
	{
		public Application application { get; set; }



		public int Selected = 0;

        public string Value { get; set; } = "";

        public string OldValue { get; set; } = "";

		public Keys keys { get; set; }


        public List<int> TopSearch { get; set; }
		public SearchPopup(Application application, Keys keys) //  int fromTop, int fromLeft, Rows
        {
            Console.CursorVisible = false;
            this.keys = keys;
            this.application = application;

	    }

        public void Function()
        {

        }
        public void SearchOne()
        {
            bool stop = false;
            if (this.keys.fromTop == keys.FoundTop)
                this.keys.fromTop++;
            
            for (int i = this.keys.fromTop; i < keys.Rows.Count - this.keys.fromTop; i++)
            {
                for (int j = 0; j < keys.Rows[i].Length; j++)
                {
                    if (keys.Rows[i].Length - j - OldValue.Length >= 0 && stop != true)
                    {
						if (keys.Rows[i].Substring(j, this.OldValue.Length) == this.OldValue) // bere to ale pouze na řádku
						{
							this.keys.fromTop = i;
							this.keys.fromLeft = j;
                            keys.FoundLeft = j;
                            keys.FoundTop = i;
                            keys.Value = this.OldValue;
                            stop = true;
                            
						}
					}
					
				}
				
			}
            
        }
        public void SearchAll()
        {
			keys.TopSearch.Clear();
			for (int i = 0; i < keys.Rows.Count; i++)
            {
                if (keys.Rows[i].Contains(OldValue) && OldValue != "")
                    keys.TopSearch.Add(i);
                
            }
        }
        public void Draw() //└┘┼─┴├┤┬┌┐│
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Gray;

            string empty = "";
            int top = 11;

            DrawShell();

            Console.SetCursorPosition(40, top);
            Console.Write($"│ Search Function".PadRight(46) + " │");
            top++;
            Console.SetCursorPosition(40, top);
            Console.Write($"│{empty.PadRight(46)}│");
            top++;
            Console.SetCursorPosition(40, top);
            Console.Write($"│ Search for: ".PadRight(46) + " │");

            if (Selected == 0)
            {
                Console.ForegroundColor = ConsoleColor.Black;
                Console.BackgroundColor = ConsoleColor.White;
            }
            Console.SetCursorPosition(55, top);
            if (Value.Length <= 0)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;

                if (OldValue.Length >= 18) // zase změna čísla
                    Console.Write($"{OldValue.Substring(OldValue.Length - 18, 18)}".PadRight(18, '_')); // Aby si to nehralo s tim řádkem
                else
                    Console.Write($"{OldValue}".PadRight(18, ' '));
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

            top++;
            Console.SetCursorPosition(40, top);
            if(keys.FoundTop == -1)
                Console.Write($"│ Last found at: [First search] ".PadRight(46) + " │");
            else
            Console.Write($"│ Last found at: Top{keys.FoundTop}, Left{keys.FoundLeft}: ".PadRight(46) + " │");

            

            if (Selected == 1)
            {
                Console.ForegroundColor = ConsoleColor.Black;
                Console.BackgroundColor = ConsoleColor.White;
            }
            Console.SetCursorPosition(41, top + 6);
            Console.Write("┌────────────┐");
            Console.SetCursorPosition(41, top + 7);
            Console.Write("│    Find    │");
            Console.SetCursorPosition(41, top + 8);
            Console.Write("└────────────┘");
            Console.SetCursorPosition(41, top);


            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Gray;

            if (Selected == 2)
            {
                Console.ForegroundColor = ConsoleColor.Black;
                Console.BackgroundColor = ConsoleColor.White;
            }
            Console.SetCursorPosition(57, top + 6);
            Console.Write("┌────────────┐");
            Console.SetCursorPosition(57, top + 7);
            Console.Write("│  Find All  │");
            Console.SetCursorPosition(57, top + 8);
            Console.Write("└────────────┘");
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Gray;
            if (Selected == 3)
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
                if (Selected >= 3)
                    Selected = 0;
                else
                    Selected++;
            }
            if (info.Key == ConsoleKey.Enter)
            {
				OldValue = Value.Length <= 0 ? OldValue : Value;
				
				switch (Selected)
                {
                    case 1:
                        SearchOne();
						break;

                    case 2:
                        SearchAll();
						break;

				}
                Console.CursorVisible = true;
                CheckforOffset();
				application.RemoveWindow();
				Value = "";
				Selected = 0;
				
			}
            string? input = info.Key.ToString();
            if (char.IsLetterOrDigit(info.KeyChar) || char.IsAscii(info.KeyChar) && input.Length < 2 || info.Key == ConsoleKey.Spacebar) 
                Value += info.KeyChar;

            if (info.Key == ConsoleKey.Backspace && Selected == 0)
            {
                if(Value.Length > 0)
				Value = Value.Remove(Value.Length - 1);
                else
                OldValue = "";
			}

		}
        public void DrawShell()
        {
            string empty = "";
            int top = 10;
            Console.SetCursorPosition(40, top);
            Console.Write("┌──────────────────────────────────────────────┐"); // Tabulka 20x40
            top++;


            for (int i = 0; i < 12; i++)
            {
                Console.SetCursorPosition(40, top);
                Console.Write($"│{empty.PadRight(46)}│");
                top++;
            }

            Console.SetCursorPosition(40, top);
            Console.Write("└──────────────────────────────────────────────┘");
        }
        public void Refresh()
        {

        }
        public void CheckforOffset()
        {
            if(keys.fromTop >= 27)
            {
                keys.offsetHeight = keys.fromTop - 26;
                keys.fromTop = 26;
            }
            if(keys.fromLeft > Console.WindowWidth)
            {
                keys.offsetWidth = keys.offsetWidth - Console.WindowWidth;
                keys.fromLeft = Console.WindowWidth;
            }
        }
    }
}






