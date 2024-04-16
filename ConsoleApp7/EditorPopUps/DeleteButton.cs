using ConsoleApp7.Buttons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp7.EditorPopUps
{
	public class DeleteButton : ComponentEditorButtons
	{
		public string Value { get; set; }
		public Application application { get; set; }
		public DeleteButton(Application application)
		{
			this.application = application;
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
