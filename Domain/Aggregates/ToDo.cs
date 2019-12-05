using Domain.BaseTypes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Aggregates
{
    public class Todo : Entity
    {
        public Todo() : base()
        {

        }
        public string Text { get; set; }

        public DateTime DueDate { get; set; }

        [NotMapped]
        public bool Expired { get { return this.DueDate < DateTime.Now; } }

        public ICollection<Attachment> Attachments { get; set; }
    }
}
