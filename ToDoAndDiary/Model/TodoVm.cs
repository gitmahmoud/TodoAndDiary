using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ToDoAndDiary.Model
{
    public class TodoVm
    {
        private DateTime _DueDate;
        private DateTime _DueTime;
        public int Id { get; set; }

        
        [Display(Name = "Text"), Required, DataType(DataType.MultilineText)]
        public string Text { get; set; }

        
        [Display(Name = "Due Date"), Required, DataType(DataType.Date)]
        public DateTime DueDate
        {
            get { return _DueDate; }

            set { this._DueDate = value; }
        }

        
        [Display(Name = "Due Time"), Required, DataType(DataType.Time)]
        public DateTime DueTime
        {
            get { return _DueTime; }

            set {
                this._DueTime = value;
                this._DueDate = this._DueDate.AddHours(value.Hour).AddMinutes(value.Minute); 
            }
        }


        [Display(Name = "Created At")]
        public DateTime CreationDate { get; set; }
    }
}