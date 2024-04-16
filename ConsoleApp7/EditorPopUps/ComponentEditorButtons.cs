using ConsoleApp7.TextEditor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp7.EditorPopUps
{
	public interface ComponentEditorButtons
	{
		//public int foundTop { get; set; }
		//public int foundLeft { get; set; }
		public string Value { get; set; }

		//public List<int> TopSearch { get; set; }

		//public List<string> Rows { get; set; }

		Application application { get; set; }
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
