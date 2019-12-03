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

    }
}