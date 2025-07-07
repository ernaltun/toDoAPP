using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using toDoAPP.Models;

namespace toDoAPP.Models
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
        public DbSet<Task> Tasks { get; set; }  
        public DbSet<User> Users { get; set; }
    }
}
