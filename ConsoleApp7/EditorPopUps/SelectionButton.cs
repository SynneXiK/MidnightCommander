using ConsoleApp7.Buttons;
using ConsoleApp7.Components;
using ConsoleApp7.TextEditor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp7.EditorPopUps
{
	public class SelectionButton : ComponentEditorButtons
	{
		public Application application { get; set; }
		public Keys keys;
		public string Value { get; set; }
		private bool first = true;

		public SelectionButton(Application application, Keys keys) // int fromLeft, int fromTop, int offsetHeight, int offsetWidth, List<string> rows, 
        {
			this.application = application;
			this.keys = keys;
		}

		public void Draw()
		{
			keys.Selecting = "B";

			application.windows[application.windows.Count - 2].Draw();
		}
		public void Function()
		{

		}
		public void HandleKey(ConsoleKeyInfo info)
		{
			if (first)
				GetStartVals();

			if (info.Key == ConsoleKey.F3)
			{
				if (keys.fromLeft == keys.startLeft && keys.fromTop == keys.startTop)
				{
					application.RemoveWindow();
					keys.Selecting = "-";
					this.first = true;
				}
				else
				{
					application.RemoveWindow(); 
				}

			}
			else
				keys.HandleKey(info);

			keys.endLeft = keys.fromLeft;
			keys.endTop = keys.fromTop;
			
		}
		public void GetStartVals()
		{
			keys.startLeft = keys.fromLeft;
			keys.startTop = keys.fromTop;
			this.first = false;
		}
		
	}
}
