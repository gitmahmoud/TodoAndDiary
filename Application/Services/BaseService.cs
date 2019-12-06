using Domain.Aggregates.TodoAgg;
using Domain.Aggregates.AttachmentsAgg;
using Domain.Aggregates.DiaryAgg;
using Domain.Interfaces;
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
        protected IFileSaver _fileSaver;        

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
            List<string> fileNames = _fileSaver.SaveFile(Files);

            return fileNames;
        }
    }
}
