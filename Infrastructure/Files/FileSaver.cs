using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Domain.Interfaces;

namespace Infrastructure.Files
{
    public class FileSaver : IFileSaver
    {
        public List<string> SaveFile(HttpFileCollectionBase Files)
        {
            List<string> fileNames = new List<string>();

            foreach (string upload in Files)
            {
                if (Files[upload] == null) continue;

                string filename = Guid.NewGuid().ToString() + ".jpg";
                string path = AppDomain.CurrentDomain.BaseDirectory + "uploads/";

                Files[upload].SaveAs(System.IO.Path.Combine(path, filename));

                fileNames.Add(filename);
            }

            return fileNames;
        }
    }
}
