using Domain.BaseTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Aggregates
{
    public class Attachment : Entity
    {
        public string Path { get; set; }

        public int? DiaryId { get; set; }
        public Diary Diary { get; set; }

        public int? TodoId { get; set; }
        public Todo Todo { get; set; }
    }
}
