using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Services.DiaryServiceAgg;
using Application.Services.TodoServiceAgg;

namespace Application.Services
{

    public class ServiceProviderManager
    {
        public IDiaryService _diaryService;
        public ITodoService _todoService;

        public ServiceProviderManager(IDiaryService diaryService, ITodoService todoService)
        {
            _todoService = todoService;
            _diaryService = diaryService;
        }
    }
}
