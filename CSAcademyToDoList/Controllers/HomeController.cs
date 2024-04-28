using CSAcademyToDoList.Data;
using CSAcademyToDoList.Models;
using CSAcademyToDoList.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CSAcademyToDoList.Controllers
{
    public class HomeController : Controller
    {
        private TodoDbContext _todoDbContext;

        public HomeController(TodoDbContext todoDbContext)
        {
            _todoDbContext = todoDbContext;
        }

		[HttpGet]
		[ActionName("Index")]
		public IActionResult Index()
        {
            var todoListViewModel = GetAllTodos();
            return View(todoListViewModel);
        }

        TodoViewModel GetAllTodos()
        {
            var todos = _todoDbContext.TodoItems.ToList();

            //List<TodoItem> todoList = new ();

            return new TodoViewModel
            {
                TodoList = todos
            };
        }

        [HttpPost]
        [ActionName("Add")]
        public IActionResult Insert(TodoViewModel todoItem)
        {
            var todo = new TodoItem
            {
                Name = todoItem.Name,
                Id = todoItem.Id
            };

            _todoDbContext.TodoItems.Add(todo);
            _todoDbContext.SaveChanges();

            return RedirectToAction("Index");
        }

		[HttpPost]
		[ActionName("Delete")]
		public JsonResult Delete(int id, string name)
		{
            var todoItem = _todoDbContext.TodoItems.Find(id);

            if (todoItem != null)
            {
                _todoDbContext.TodoItems.Remove(todoItem);
                _todoDbContext.SaveChanges();
				return Json(new { success = true });
			}
            else
            {
				return Json(new { success = false, message = $"Todo item '{name}' was not found." });
			}
		}

        [HttpGet]
        public JsonResult PopulateForm(int id, string name)
        {
            var todo = GetById(id);
            return Json(todo);
        }

        TodoItem GetById(int id)
        {
            var todo = _todoDbContext.TodoItems.Find(id);
            return todo;
        }

		[HttpPost]
		[ActionName("Update")]
		public IActionResult Update(TodoViewModel todoItem)
		{
			var todo = new TodoItem
			{
				Id = todoItem.Id,
				Name = todoItem.Name
			};

            var existingTodo = _todoDbContext.TodoItems.Find(todoItem.Id);

            if (existingTodo != null)
            {
                existingTodo.Name = todo.Name;
                _todoDbContext.SaveChanges();
            }

			return RedirectToAction("Index");
		}
	}
}