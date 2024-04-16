using ConsoleApp7.Buttons;
using ConsoleApp7.TextEditor;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp7.EditorPopUps
{
	public class EditorButtons : Components.AppWindows
	{
		public List<ComponentEditorButtons> Buttons;
		public int ButtonSelected;

		public Application application;
		public string path;

		public Keys keys { get; set; }

		public EditorButtons(Application application, Keys keys, string path) //  List<string> rows, int Top, int Left, string Modified, string path, int offsetHeight, int offsetWidth,
        {
			this.path = path;
            this.keys = keys;
			this.application = application;
			Buttons = new List<ComponentEditorButtons> { new EmptyButton(application), new EmptyButton(application), new EmptyButton(application), new SelectionButton(application , keys), new ReplaceButton(application, keys), new CopyButton(application), new MoveButton(application), new SearchPopup(application, keys), new DeleteButton(application), new EmptyButton(application), new ButtonExitEditor(application, path, keys) };
		}
		public override void Draw()
		{
			this.Buttons[ButtonSelected].Draw();
		}
		public override void HandleKey(ConsoleKeyInfo info) 
		{ 
			this.Buttons[ButtonSelected].HandleKey(info);
		}
		public override void Refresh()
		{
			Draw();
		}
	}
}
