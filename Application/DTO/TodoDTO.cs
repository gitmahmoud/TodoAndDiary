using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO
{
    public class TodoDTO
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime CreationDate { get; set; }

    }
}
