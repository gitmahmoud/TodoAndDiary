using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTO;
using AutoMapper;
using Domain.Aggregates;
using System.Web;

namespace Application.Services
{
    public class TodoService : BaseService, ITodoService
    {
        private readonly ITodoRepository _todoRepository;
        private readonly IAttachmentRepository _attachmentRepository;

        public TodoService(ITodoRepository todoRepository, IAttachmentRepository attachmentRepository)
        {
            _todoRepository = todoRepository;
            _attachmentRepository = attachmentRepository;
        }

        public void AddTodo(TodoDTO todoDto, HttpFileCollectionBase Files)
        {
            Todo todo = Mapper.Map<TodoDTO, Todo>(todoDto);
            _todoRepository.Add(todo);

            List<string> fileNames = SaveFiles(Files);
            AddAttachments(_attachmentRepository, todo, fileNames);

            _todoRepository.UnitOfWork.Commit();
        }


        public TodoDTO GetTodo(int id)
        {
            throw new NotImplementedException();
        }

        public List<TodoDTO> GetTodos()
        {            
            var todos = _todoRepository.Get(x=> x.IsDeleted != true, y=> y.OrderByDescending(x=> x.DueDate) );
            
            return Mapper.Map<IEnumerable<Todo>, List<TodoDTO>>(todos);            
        }

        public void UpdateTodo(TodoDTO todoDto)
        {
            throw new NotImplementedException();
        }
    }
}
