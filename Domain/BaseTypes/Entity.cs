using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.BaseTypes
{
    public abstract class Entity
    {
        [Key]
        public int Id { get; set; }

        public string Text { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime LastEditDate { get; set; }
        public DateTime DeletDate { get; set; }
        public bool IsDeleted { get; set; }
    }

}
