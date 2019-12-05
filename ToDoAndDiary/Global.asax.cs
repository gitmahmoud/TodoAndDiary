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
using Infrastructure.Data;
using AutoMapper;
using Application.DTO;
using ToDoAndDiary.Model;

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
            container.RegisterType<IUnitOfWork, MainUnitOfWork>(new ContainerControlledLifetimeManager());
                        container.RegisterType<IQueryableUnitOfWork, MainUnitOfWork>();

            container.RegisterType<ITodoService, TodoService>();
            container.RegisterType<IDiaryService, DiaryService>();

            container.RegisterType<IDiaryRepository, DiaryRepository>();
            container.RegisterType<ITodoRepository, TodoRepository>();
            container.RegisterType<IAttachmentRepository, AttachmentRepository>();

            Mapper.CreateMap<Diary, DiaryDTO>();
            Mapper.CreateMap<DiaryDTO, DiaryVm>();

            Mapper.CreateMap<DiaryVm, DiaryDTO>();
            Mapper.CreateMap<DiaryDTO, Diary>();

            Mapper.CreateMap<Todo, TodoDTO>();
            Mapper.CreateMap<TodoDTO, TodoVm>();

            Mapper.CreateMap<TodoVm, TodoDTO>();
            Mapper.CreateMap<TodoDTO, Todo>();            
        }
    }
}
