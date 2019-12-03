using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Application.Services;
using Application.DTO;
using ToDoAndDiary.Model;

using AutoMapper;

namespace ToDoAndDiary.Controllers
{
    public class TodoController : BaseController
    {

        // GET: Todo
        public ActionResult Index()
        {
            var todos = serviceProvider._todoService.GetTodos();
            List<TodoVm> lst_todos = Mapper.Map<List<TodoDTO>, List<TodoVm>>(todos);

            return View(lst_todos);
        }
        
    }
}