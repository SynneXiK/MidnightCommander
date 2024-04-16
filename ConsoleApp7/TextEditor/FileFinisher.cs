using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp7.TextEditor
{
	public class FileFinisher
	{
		List<string> rows;
		string path;
		StreamWriter sw;
		public FileFinisher(List<string> Rows,string path)
		{
			this.rows = Rows;
			this.path = path;
			sw = new StreamWriter(path);
		}

		public void ExportFiles()
		{
			foreach (string line in rows)
			{
				sw.WriteLine(line);
			}
			sw.Close();
		}
	



	}
}
