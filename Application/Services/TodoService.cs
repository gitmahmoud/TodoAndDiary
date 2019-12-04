using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTO;
using AutoMapper;
using Domain.Aggregates;

namespace Application.Services
{
    public class TodoService : ITodoService
    {
        private readonly ITodoRepository _todoRepository;

        public TodoService(ITodoRepository todoRepository)
        {
            _todoRepository = todoRepository;
        }


        public void AddTodo(TodoDTO todoDto)
        {
            Todo todo = Mapper.Map<TodoDTO, Todo>(todoDto);
            _todoRepository.Add(todo);
            _todoRepository.UnitOfWork.Commit();
        }

        public TodoDTO GetTodo(int id)
        {
            throw new NotImplementedException();
        }

        public List<TodoDTO> GetTodos()
        {
            var todos = _todoRepository.GetAll();
            
            return Mapper.Map<IEnumerable<Todo>, List<TodoDTO>>(todos);            
        }

        public void UpdateTodo(TodoDTO todoDto)
        {
            throw new NotImplementedException();
        }
    }
}
