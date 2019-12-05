using Infrastructure.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Aggregates;

namespace Infrastructure.Data.Repositories
{
    public class AttachmentRepository : Repository<Attachment>, IAttachmentRepository
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="unitOfWork">Injected Unit of Work</param>
        public AttachmentRepository(IQueryableUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }
    }
}