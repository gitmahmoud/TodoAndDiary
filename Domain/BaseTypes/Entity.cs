using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.BaseTypes
{
    public abstract class Entity
    {
        private DateTime _CreationDate;

        [DatabaseGenerated(DatabaseGeneratedOption.Identity), Key()]
        public int Id { get; set; }

        public DateTime CreationDate
        {
            get
            { 
                return this._CreationDate;
            }
            private set
            {
                this._CreationDate = DateTime.Now;
            }
        }
        public DateTime? LastEditDate { get; set; }
        public DateTime? DeleteDate { get; set; }
        public bool? IsDeleted { get; set; }
    }

}
