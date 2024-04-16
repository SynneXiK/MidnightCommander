using ConsoleApp7.Components;
using ConsoleApp7.PopUps;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp7.Buttons
{
	public class Button : AppWindows
	{
        private List<ComponentButton> Buttons;
		public int ButtonSelected;
		private string path;
		private string path2;
		public ErrorWindow ErrorWindow;
        public Button(Table Chosen, Table Other, Application application)
		{
			this.path = Path.Combine(Chosen.path, Chosen.ChosenDirectory!);
			this.path2 = Path.Combine(Other.path, Other.ChosenDirectory!);	
			this.Application = application;
			this.ErrorWindow = new ErrorWindow(this.Application);
			this.Buttons = new List<ComponentButton> { new ButtonFiller(), new ButtonFiller(), new ButtonFiller(), new ButtonFiller(), new ButtonOpenEditor(this.Application, this.ErrorWindow), new ButtonCopy(this.Application, this.ErrorWindow), new ButtonRename(this.Application, this.ErrorWindow), new ButtonCreate(this.Application, this.ErrorWindow), new ButtonDelete(this.Application, this.ErrorWindow), new ButtonFiller(), new ButtonExit(this.Application, this.ErrorWindow) };

		}
		public override void Draw()
		{
			this.Buttons[ButtonSelected].Draw(path, path2);
		}

		public override void HandleKey(ConsoleKeyInfo info)
		{
			if (path.StartsWith(@"\"))
				path = path.Remove(0, 1);

            if (path2.StartsWith(@"\"))
                path2 = path2.Remove(0, 1);

			

			string? input = info.Key.ToString();

			if (path.Length <= 3 && input != "F10")
			{
				this.Application.AddWindow(ErrorWindow);
				ErrorWindow.GetError("Can't use buttons in disc view");
			}
			if (input.Contains('F') && input.Length == 2 || input == "F10")
			{
				this.ButtonSelected = Convert.ToInt32(input.Remove(0, 1));
				this.Buttons[ButtonSelected].HandleKey(info, path, path2);
			}
			else
			this.Buttons[ButtonSelected].HandleKey(info, path, path2);

			// F4 - Edit F5 - Copy, F6 - Rename, F7 - Makedir, F8 - Delete, F9 - Move
		}
		public override void Refresh()
		{

		}
	}
}
