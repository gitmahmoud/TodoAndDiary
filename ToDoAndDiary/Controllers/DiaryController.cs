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
    public class DiaryController : BaseController
    {

        // GET: Diary
        public ActionResult Index()
        {
            var diaries = serviceProvider._diaryService.GetDiaries();
            List<DiaryVm> lst_diaries = Mapper.Map<List<DiaryDTO>, List<DiaryVm>>(diaries);

            return View(lst_diaries);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Text")]DiaryVm diary)
        {

            if (ModelState.IsValid)
            {
                DiaryDTO diaryDto = Mapper.Map<DiaryVm, DiaryDTO>(diary);

                serviceProvider._diaryService.AddDiary(diaryDto);
                return RedirectToAction("Index");
            }

            return View(diary);
        }

    }
}