using ConsoleApp7.PopUps;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp7.Buttons
{
	public interface ComponentButton
	{
		public ErrorWindow ErrorWindow { get; set; }
		public Application application { get; set; }
		
		public void Draw(string path, string path2)
		{

		}
		public void Function(string path, string path2)
		{

		}
		public void HandleKey(ConsoleKeyInfo info, string path, string path2)
		{

		}
	}
}
