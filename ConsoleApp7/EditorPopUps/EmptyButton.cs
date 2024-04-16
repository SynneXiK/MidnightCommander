using ConsoleApp7.Buttons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp7.EditorPopUps
{
	internal class EmptyButton : ComponentEditorButtons
	{
		public string Value { get; set; }
		public Application application { get; set; }
		public EmptyButton(Application application)
		{
			//application.RemoveWindow(); // tohle je empty button ale stale se přida když ho neodeberu
		}

		public void Draw()
		{

		}
		public void Function()
		{

		}



		public void HandleKey(ConsoleKeyInfo info)
		{
			
		}
	}
}
