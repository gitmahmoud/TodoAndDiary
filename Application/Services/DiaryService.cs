using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Application.DTO;
using AutoMapper;
using Domain.Aggregates;
using Domain.Interfaces;

namespace Application.Services
{
    public class DiaryService : BaseService, IDiaryService
    {
        private readonly IDiaryRepository _diaryRepository;
        private readonly IAttachmentRepository _attachmentRepository;

        public DiaryService(IDiaryRepository diaryRepository, IAttachmentRepository attachmentRepository, IFileSaver fileSaver)
        {
            _diaryRepository = diaryRepository;
            _attachmentRepository = attachmentRepository;
            _fileSaver = fileSaver;
        }


        public void AddDiary(DiaryDTO diaryDto, HttpFileCollectionBase Files)
        {
            Diary diary = Mapper.Map<DiaryDTO, Diary>(diaryDto);
            _diaryRepository.Add(diary);

            List<string> fileNames = SaveFiles(Files);
            AddAttachments(_attachmentRepository, diary, fileNames);

            _diaryRepository.UnitOfWork.Commit();
        }

        public List<DiaryDTO> GetDiaries()
        {
            var diaries = _diaryRepository.Get(x=> x.IsDeleted != true, x=> x.OrderByDescending(y=> y.CreationDate));

            return Mapper.Map<IEnumerable<Diary>, List<DiaryDTO>>(diaries);
        }

        public DiaryDTO GetDiary(int id)
        {
            throw new NotImplementedException();
        }

        public void UpdateDiary(DiaryDTO diaryDto)
        {
            throw new NotImplementedException();
        }
    }
}
