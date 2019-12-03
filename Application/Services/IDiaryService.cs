using Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public interface IDiaryService
    {

        /// <summary>
        /// Gets all diaries 
        /// </summary>
        /// <returns>List of Diaries</returns>
        List<DiaryDTO> GetDiaries();

        /// <summary>
        /// Retrieve a diary by its ID
        /// </summary>
        /// <param name="id">The ID of the diary to be retrieved</param>
        /// <returns>diary DTO object</returns>
        DiaryDTO GetDiary(int id);

        /// <summary>
        /// Adds a diary information
        /// </summary>
        /// <param name="diaryDto">Information to be added</param>
        void AddDiary(DiaryDTO diaryDto);

        /// <summary>
        /// Updates a diary information
        /// </summary>
        /// <param name="diaryDto">Information to be updated</param>
        void UpdateDiary(DiaryDTO diaryDto);
    }
}
