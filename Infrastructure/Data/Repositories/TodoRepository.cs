using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Aggregates;
using Infrastructure.Data.Interfaces;

namespace Infrastructure.Data.Repositories
{
    public class TodoRepository : Repository<Todo>, ITodoRepository
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="unitOfWork">Injected Unit of Work</param>
        public TodoRepository(IQueryableUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }
    }
}
