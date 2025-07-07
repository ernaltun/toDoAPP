using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using toDoAPP.Interface;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;
using toDoAPP.Models;
using Task = toDoAPP.Models.Task;
namespace toDoAPP.Repository
{
    public class TaskRepository : ITaskInterface
    {
        private readonly DataContext _context;

        public TaskRepository(DataContext dataContext)
        {
            _context = dataContext;
        }
        public void Delete(int id)
        {
            var task = _context.Tasks.Find(id);
            if (task != null)
            {
                _context.Tasks.Remove(task);
                _context.SaveChanges();
            }
        }

        public IEnumerable<Models.Task> GetAll()
        {
            var tasks = _context.Tasks;
            return tasks;
        }

        public Task GetById(int id)
        {
            var task = _context.Tasks.Find(id);
            return task; 
        }

        public void Insert(Task task)
        {
            _context.Tasks.Add(task);
            _context.SaveChanges();
        }


		public void Save()
        {
            _context.SaveChanges();
        }

        public void Update(Task task)
        {
            _context.Update(task);
            _context.SaveChanges();
        }

        public List<Task> GetByUser (int userId)
        {
            var tasks = _context.Tasks.Include(p=> p.User).Where(p=>p.UserId==userId).ToList();
            return tasks;
        }
    }
}
