using Domain.Aggregates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Application.Services
{
    public abstract class BaseService
    {
        protected void AddAttachments(IAttachmentRepository attachmentRepository, Todo todo, List<string> fileNames)
        {
            foreach (string fileName in fileNames)
            {
                attachmentRepository.Add(new Attachment { Path = fileName, TodoId = todo.Id });
            }
        }

        protected void AddAttachments(IAttachmentRepository attachmentRepository, Diary diary, List<string> fileNames)
        {
            foreach (string fileName in fileNames)
            {
                attachmentRepository.Add(new Attachment { Path = fileName, DiaryId = diary.Id });
            }
        }

        protected List<string> SaveFiles(HttpFileCollectionBase Files)
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
