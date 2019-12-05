using Domain.Aggregates;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{

    public class InfrastructureProviderManager
    {
        public IDiaryRepository _diaryRepository;
        public IAttachmentRepository _attachmentRepository;
        public IFileSaver _fileSaver;
        public ITodoRepository _todoRepository;

        public InfrastructureProviderManager(IDiaryRepository diaryRepository, IAttachmentRepository attachmentRepository,
            IFileSaver fileSaver, ITodoRepository todoRepository)
        {
            _diaryRepository = diaryRepository;
            _attachmentRepository = attachmentRepository;
            _fileSaver = fileSaver;
            _todoRepository = todoRepository;
        }
    }
}