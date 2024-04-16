using ConsoleApp7.PopUps;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace ConsoleApp7.Services
{
    public class FilesService
    {

        public List<Data> GetData(string path)
        {
            List<Data> data = new List<Data>();
            if (path == "")
                data = this.ReadDisc();

            else
                data = this.ReadFolders(path);

            return data;
        }

        public List<Data> ReadDisc()
        {
            List<Data> result = new List<Data>();


            //foreach (DriveInfo drive in DriveInfo.GetDrives())
            //{
            //    result.Add(new Data()
            //    {
            //        Name = drive.Name,
            //        Size = drive.TotalSize - (drive.TotalSize - drive.TotalFreeSpace)
            //    });
            //}

            //Odkomentovat, doma jsem měl něco s Diskem D a zakomentovat spodek
            result.Add(new Data() { Name = @"C:\", Size = 1234569, UpdateTime = DateTime.Now });
            result.Add(new Data() { Name = @"F:\", Size = 1234569, UpdateTime = DateTime.Now });
            result.Add(new Data() { Name = @"G:\", Size = 1234569, UpdateTime = DateTime.Now });
            result.Add(new Data() { Name = @"X:\", Size = 1234569, UpdateTime = DateTime.Now });
            result.Add(new Data() { Name = @"Z:\", Size = 1234569, UpdateTime = DateTime.Now });

            return result;
        }

        public List<Data> ReadFolders(string path)
        {
           
            List<Data> result = new List<Data>();
            result.Add(new Data()
            {
                Name = "..",
                // disk
            });
            DirectoryInfo dir = new DirectoryInfo(path);

           
				foreach (DirectoryInfo item in dir.GetDirectories())
				{
					result.Add(new Data()
					{
						Name = item.Name,
						UpdateTime = item.LastWriteTime,
						Path = item.FullName
					});
				}
				foreach (FileInfo item in dir.GetFiles())
				{
					result.Add(new Data()
					{
						Name = item.Name,
						Size = item.Length,
						UpdateTime = item.LastWriteTime
					});
				}
            
            
            


            return result;
        }

        

    }
}
