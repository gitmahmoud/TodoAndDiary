using Domain.Aggregates.AttachmentsAgg;
using Domain.BaseTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Aggregates.DiaryAgg
{
    public class Diary : Entity
    {
        public Diary() : base()
        {

        }
        public string Text { get; set; }
        public ICollection<Attachment> Attachments { get; set; }

    }
}
