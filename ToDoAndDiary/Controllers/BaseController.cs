using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Application.Services;
using Microsoft.Practices.Unity;

namespace ToDoAndDiary.Controllers
{
    public class BaseController : Controller
    {
        protected ServiceProviderManager serviceProvider { get; private set; }

        public BaseController()
        {
            serviceProvider = IoCContainer.Instance.Resolve<ServiceProviderManager>();
        }
    }
}