using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Identity.Client;
using Serilog;
using System.Security.Claims;
using toDoAPP.Repository;
using Task = toDoAPP.Models.Task;
namespace toDoAPP.Controllers
{
    public class TaskController : Controller
    {
        private readonly TaskRepository _repo;

        public TaskController(TaskRepository taskRepository)
        {
            _repo = taskRepository;
        }
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Index()
        {
            int currentUser = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            if (currentUser == null)
            {
                return NotFound();
            }
            var tasks = _repo.GetByUser(currentUser).ToList();
            return View(tasks);
        }
        [HttpGet]
		[Authorize]
		public ActionResult AddTask()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddTask(Task task)
        {
            int currentUser = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            
            if (task.LastDate.Date > DateTime.Now.Date || task.LastDate.Date == DateTime.Now.Date)
            {
                task.UserId = currentUser;
                _repo.Insert(task);
            }
            Log.Information(" ADDED TASK -> {@task} ", task);
            return RedirectToAction("Index", "Task");

        }
        [HttpGet]
		[Authorize]
		public ActionResult UpdateTask(int id)
        {
            Task task = _repo.GetById(id);
            return View(task);
        }
        [HttpPost]
        public ActionResult UpdateTask(Task task)
        {
            int currentUser = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            task.UserId = currentUser;
            var existingTask = _repo.GetById(task.TaskId); 
            if (existingTask != null)
            {
                existingTask.UserId = task.UserId;
                existingTask.TaskName = task.TaskName;
                existingTask.Description = task.Description;
                existingTask.LastDate = task.LastDate;
                existingTask.Precedence = task.Precedence;
                existingTask.Category = task.Category;
                existingTask.IsCompleted = task.IsCompleted;
                _repo.Update(existingTask);
                Log.Information("UPDATED TASK -> {@existingTask} ", existingTask);
                return RedirectToAction("Index", "Task");
            }
            else
            {
                return NotFound();
            }
            
        }
        public IActionResult DeleteTask(int id)
        {
            var deletedTask = _repo.GetById(id);
            _repo.Delete(id);
            Log.Information("DELETED TASK -> {@deletedTask} ", deletedTask );
            return RedirectToAction("Index", "Task");
        }
    }
}