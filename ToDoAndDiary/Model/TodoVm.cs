using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ToDoAndDiary.Model
{
    public class TodoVm
    {
        public int Id { get; set; }
        public string Text { get; set; }

        [Display(Name = "Due Date")]
        public DateTime DueDate { get; set; }

        [Display(Name = "Created At")]
        public DateTime CreationDate { get; set; }
    }
}