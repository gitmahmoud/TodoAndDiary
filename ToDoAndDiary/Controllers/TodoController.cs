using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Application.Services;
using Application.DTO;
using ToDoAndDiary.Model;

using AutoMapper;
using System.IO;

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

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Text,DueDate,DueTime")]TodoVm todo)
        {
            if (ModelState.IsValid)
            {
                TodoDTO todoDto = Mapper.Map<TodoVm, TodoDTO>(todo);

                serviceProvider._todoService.AddTodo(todoDto, Request.Files);
                
                return RedirectToAction("Index");
            }

            return View(todo);
        }

    }
}