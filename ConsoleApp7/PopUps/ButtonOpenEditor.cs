using ConsoleApp7.PopUps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp7.Buttons
{
	public class ButtonOpenEditor : ComponentButton
	{
		public Application application { get; set; }

		public ErrorWindow ErrorWindow { get; set; }
		public ButtonOpenEditor(Application application, ErrorWindow ErrorWindow)
		{
			this.application = application;
			this.ErrorWindow = ErrorWindow;
		}

		public void Function(string path, string path2) // path2 jen aby to sedělo
		{

			if (File.Exists(path))
			{
				application.RemoveWindow();
				application.AddWindow(new TextEditor.Editor(path, this.application));
				Console.CursorVisible = true;
			}
			else
			{
				this.application.AddWindow(ErrorWindow);
				ErrorWindow.GetError("Not a file");
			}
				
		}
		public void HandleKey(ConsoleKeyInfo info, string path, string path2)
		{
			Function(path, "");
		}

        public void Draw(string path, string path2)
        {
            throw new NotImplementedException(); // není potřeba
        }

    }
}
