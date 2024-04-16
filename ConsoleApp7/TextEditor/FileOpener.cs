using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp7.TextEditor
{
	public class FileOpener
	{
		string path;

		public FileOpener(string path)
		{
			this.path = path;
		}

		public List<string> IfData()
		{
			StreamReader sr = new StreamReader(path);
			List<string> files = new List<string>();

			while (!sr.EndOfStream)
				files.Add(sr.ReadLine());

			sr.Close();
			return files;
		}
		public List<string> GetFiles()
		{
            List<string> files = new List<string>();

            if (new FileInfo(path).Length == 0)
			{
				foreach (string item in this.IfEmpty())
				{
					files.Add(item);
				}
			}

			else
			{
                foreach (string item in this.IfData())
                {
                    files.Add(item);
                }
            }


			return files;
		}

		public List<string> IfEmpty()
		{
            List<string> files = new List<string>();

			files.Add("");

            return files;



        }

	}
}
