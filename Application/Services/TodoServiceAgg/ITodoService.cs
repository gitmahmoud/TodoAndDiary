using Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Application.Services.TodoServiceAgg
{
    public interface ITodoService
    {

        /// <summary>
        /// Gets all ToDos 
        /// </summary>
        /// <returns>List of ToDos</returns>
        List<TodoDTO> GetTodos();

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
        void AddTodo(TodoDTO todoDto, HttpFileCollectionBase Files);

        /// <summary>
        /// Updates a todo information
        /// </summary>
        /// <param name="todoDto">Information to be updated</param>
        void UpdateTodo(TodoDTO todoDto);

    }
}
