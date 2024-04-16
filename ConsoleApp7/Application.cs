using ConsoleApp7.Buttons;
using ConsoleApp7.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp7
{
	public class Application
	{
        public List<AppWindows> windows = new List<AppWindows> {new DataWindow()};
		public bool Running = true;

		public Application()
		{
			Console.CursorVisible = false;
			Console.Title = "Midnight Commander";
			Console.ForegroundColor = ConsoleColor.White;
			Console.BackgroundColor = ConsoleColor.DarkBlue;
		}
		public void Draw()
		{
			windows.Last().Draw();
			windows.Last().Application = this;
		}

		public void HandleKey(ConsoleKeyInfo info)
		{
			windows.Last().HandleKey(info);
			windows.Last().Refresh();
		}
		public void AddWindow(AppWindows newWindow)
		{
			windows.Add(newWindow);
		}
		public void RemoveWindow()
		{
			windows.Remove(windows.Last());
			Console.Clear();
		}
		public void Run()
		{
			while(Running == true)
			{
				Draw();

				ConsoleKeyInfo info = Console.ReadKey();
				HandleKey(info);
			}
		}
	}
}
