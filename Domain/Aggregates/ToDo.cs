using Domain.BaseTypes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Aggregates
{
    public class Todo : Entity
    {
        public string Text { get; set; }

        public DateTime DueDate { get; set; }

        public ICollection<Attachment> Attachments { get; set; }
    }
}
