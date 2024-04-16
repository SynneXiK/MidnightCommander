using ConsoleApp7.Services;
using ConsoleApp7.Buttons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp7.PopUps;

namespace ConsoleApp7.Components
{
    public class Table : AppWindows
    {
        public string? CurrentParent { get; set; }

        public Application application;

        private FilesService files = new FilesService();

        private ErrorWindow errorWindow;

		private List<Data> list = new List<Data>();

        public bool IsActive;

		private int offset = 0;

        public string path { get; set; } = "";

        public string? ChosenDirectory { get; set; }

        public int Left { get; set; }
		public int Selected { get; set; } = 0;

        public int Count { get; set; } = 25;

        public Table(int left)
        {
            this.errorWindow = new ErrorWindow(this.application); // aplikace nebude null, vždy jí předávám
			this.Left = left;
			list = files.GetData(this.path);
		}
        public override void HandleKey(ConsoleKeyInfo info)
        {
			ChosenDirectory = list[Selected].Name;
            switch(info.Key)
            {
                case ConsoleKey.UpArrow:
                    if (Selected <= 0)
                        return;

                    Selected--;

                    if (Selected == offset - 1)
                        offset--;

                    ChosenDirectory = list[Selected].Name;
                    return;

                case ConsoleKey.DownArrow:
                    if (Selected >= list.Count - 1)
                        return;

                    Selected++;

                    if (Selected == offset + Math.Min(Count, this.list.Count))
                        offset++;

                    ChosenDirectory = list[Selected].Name;

                    return;
                case ConsoleKey.End:
                    Selected = list.Count - 1;

                    if (offset <= list.Count - 1 && Selected >= Console.WindowHeight - 3)
                        offset = list.Count - Count;
                    return;
                case ConsoleKey.Home:
                    Selected = 0;
                    offset = 0;
                    return;
                case ConsoleKey.Enter:
                    Selected = 0;
                    if (ChosenDirectory == "..")
                    {
                        if (path.Length <= 3) // disk
                        {
                            path = "";
                        }
                        else
                        {
                            DirectoryInfo newdir = new DirectoryInfo(path);
                            path = newdir.Parent.FullName;
                        }

                        list = files.GetData(this.path);
                        return;

                    }
                    if (path.Length < 3)
                    {

                        try
                        {
                            Directory.GetDirectories(path + ChosenDirectory);
                            Directory.GetFiles(path + ChosenDirectory);

                        }
                        catch (UnauthorizedAccessException)
                        {
                            application.AddWindow(errorWindow);
                            errorWindow.GetError("Access denied!");
                            return;
                        }
                        catch (Exception)
                        {
                            application.AddWindow(errorWindow);
                            errorWindow.GetError("File or Folder doesnt exist!");
                            return;
                        }


                        path += ChosenDirectory;
                    }
                    else
                    {

                        try
                        {

                            Directory.GetDirectories(Path.Combine(path, ChosenDirectory));
                            Directory.GetFiles(Path.Combine(path, ChosenDirectory));

                        }
                        catch (UnauthorizedAccessException)
                        {
                            application.AddWindow(errorWindow);
                            errorWindow.GetError("Access denied!");
                            return;
                        }
                        catch (Exception)
                        {
                            application.AddWindow(errorWindow);
                            errorWindow.GetError("Unexpected error!");
                            return;
                        }

                        Path.Combine(path, ChosenDirectory);


                    }
                    list = files.GetData(this.path);
                    return;


            }

		}

		public override void Refresh()
		{
			list = files.GetData(this.path);
		}

		public override void Draw() 
        {
			DrawOutline();

            int Top = 1;
            for (int i = offset; i < offset + Math.Min(Count, this.list.Count); i++)
            {
                if (i == Selected)
                {
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.BackgroundColor = ConsoleColor.Cyan;
				}
                Console.SetCursorPosition(this.Left, Top);
                DrawLine(list[i].Name, list[i].Size, list[i].UpdateTime);
                Top++;
                Console.ForegroundColor = ConsoleColor.White;
                Console.BackgroundColor = ConsoleColor.DarkBlue;
            }
            for (int i = list.Count + 1; i < this.Count + 1; i++) // Přepisování
            {
                Console.SetCursorPosition(this.Left, i);
                Console.Write($"{string.Empty.PadRight(37)}│{string.Empty.PadRight(6)}│{string.Empty.PadRight(13)}│");

			}
			DrawBottom();
			Console.SetCursorPosition(this.Left, this.Count + 2);
            Console.Write(path.PadRight(56));

        }
        public void DrawLine(string Name, long? Size, DateTime UpdateTime)
        {
            if (Name.Length >= 37)
            {
                Name = Name.Substring(0, 34)+"..";
            }
            Console.Write($"{Name}{string.Empty.PadRight(37 - Name.Length)}");
            Console.Write($"│{SizeConverter(Size)}{string.Empty.PadRight(6-SizeConverter(Size).Length)}│"); // -1 kvůli mezeře na začátku
            Console.Write($"{UpdateTime.Date.ToString("dd MMM HH:mm").PadRight(13)}");  
        }
		private string SizeConverter(long? input)
		{
			string[] Sizes = { "b", "kb", "Mb", "Gb", "Tb" };
			int i = 0;
            if (input == null)
            {
                return "Folder";
            }
            if (input < 0)
            {
                input *= -1;
            }
			while (input > 1024)
			{
				input = input / 1024;
				i++;
			}
            if (Sizes[i] == Sizes[0] && input == null)
                return "";

            else
            return $"{input}{Sizes[i]}";
		}
		public void DrawOutline() // Upravit na pouze jednu tabulku
		{
            //└┘┼─┴├┤┬┌┐│
             
            Console.SetCursorPosition(this.Left-1, 0);
			string none = "";
			Console.WriteLine("┌─────────────────────────────────────┬──────┬─────────[X]─┐");
			for (int i = 0; i < Count; i++) // 25 jako random číslo ngl
			{
                Console.SetCursorPosition(this.Left - 1, i+1);
				Console.WriteLine($"│{none.PadRight(37)}│{none.PadRight(6)}│{none.PadRight(13)}│");
			}
            Console.SetCursorPosition(this.Left - 1, Count + 1);
			Console.WriteLine("├─────────────────────────────────────┴──────┴─────────────┤");
			Console.SetCursorPosition(this.Left - 1, Count + 2);
			// Mezera pro path
			Console.WriteLine($"│{none.PadRight(58)}│");
			Console.SetCursorPosition(this.Left - 1, Count + 3);
			Console.WriteLine("└──────────────────────────────────────────────────────────┘");
		}
        public void DrawBottom()
        {
            if (this.Left > 60)
                return;

            List<string> Functions = new List<string>() {"Help", "Menu", "View", "Edit", "Copy", "RenMove", "Mkdir", "Delete", "PullDn", "Quit "};
			Console.SetCursorPosition(0, Console.WindowHeight-1);
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
        



    }
}
