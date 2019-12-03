using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Microsoft.Practices.Unity;

using Domain.Interfaces;
using Infrastructure.Data.UnitOfWork;
using Application.Services;
using Domain.Aggregates;
using Infrastructure.Data.Repositories;
using Infrastructure.Data.Interfaces;

namespace ToDoAndDiary
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            var container = IoCContainer.Instance;
            
            container.RegisterType<IQueryableUnitOfWork, MainUnitOfWork>(new PerRequestLifetimeManager());

            container.RegisterType<ITodoService, TodoService>();
            container.RegisterType<IDiaryService, DiaryService>();

            container.RegisterType<IDiaryRepository, DiaryRepository>();
            container.RegisterType<ITodoRepository, TodoRepository>();


        }
    }
}
