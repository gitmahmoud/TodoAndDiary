using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTO;
using AutoMapper;
using Domain.Aggregates;


namespace Application.Services
{
    public class DiaryService : IDiaryService
    {
        private readonly IDiaryRepository _diaryRepository;

        public DiaryService(IDiaryRepository diaryRepository)
        {
            _diaryRepository = diaryRepository;
        }


        public void AddDiary(DiaryDTO diaryDto)
        {
            throw new NotImplementedException();
        }

        public List<DiaryDTO> GetDiaries()
        {
            var diaries = _diaryRepository.GetAll();

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
