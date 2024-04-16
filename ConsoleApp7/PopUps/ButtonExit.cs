using ConsoleApp7.Buttons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp7.PopUps
{
	public class ButtonExit : ComponentButton
	{
		public Application application { get; set; }

		public ErrorWindow ErrorWindow { get; set; }
        public ButtonExit(Application application, ErrorWindow ErrorWindow)
        {
            this.application = application;
            this.ErrorWindow = ErrorWindow;
        }
        public void Draw(string path, string path2)
		{

		}
		public void Function(string path, string path2)
		{
			
		}

		public void HandleKey(ConsoleKeyInfo info, string path, string path2)
		{
			application.Running = false;
		}
	}
}
