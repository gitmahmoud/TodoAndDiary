using Infrastructure.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Aggregates.DiaryAgg;

namespace Infrastructure.Data.Repositories
{
    public class DiaryRepository : Repository<Diary>, IDiaryRepository
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="unitOfWork">Injected Unit of Work</param>
        public DiaryRepository(IQueryableUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }
    }
}
