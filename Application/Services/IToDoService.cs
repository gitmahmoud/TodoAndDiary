using Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public interface IToDoService
    {

        /// <summary>
        /// Gets all ToDos 
        /// </summary>
        /// <returns>List of ToDos</returns>
        List<TodoDTO> GetTodos(int pageIndex, int pageCount, string orderBy, bool ascending);

        /// <summary>
        /// Retrieve a todo by its ID
        /// </summary>
        /// <param name="id">The ID of the todo to be retrieved</param>
        /// <returns>todo DTO object</returns>
        TodoDTO GetTodo(int id);

        /// <summary>
        /// Adds a todo information
        /// </summary>
        /// <param name="todoDto">Information to be added</param>
        void AddTodo(TodoDTO todoDto);

        /// <summary>
        /// Updates a todo information
        /// </summary>
        /// <param name="todoDto">Information to be updated</param>
        void UpdateTodo(TodoDTO todoDto);

    }
}
