using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ToDoAndDiary.Model
{
    public class DiaryVm
    {
        public int Id { get; set; }

         [Display(Name = "Text")]
        public string Text { get; set; }

        [Display(Name = "Created At")]
        public DateTime CreationDate { get; set; }
    }
}