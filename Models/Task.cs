using System.ComponentModel.DataAnnotations;
using System.Security.Permissions;

namespace toDoAPP.Models
{
    public class Task
    {
        [Key]
        public int TaskId { get; set; }
        public string TaskName { get; set; }
        public string Description {  get; set; }
        public DateTime LastDate { get; set; }
        public string Precedence { get; set; }
        public string Category { get; set; }
        public bool IsCompleted { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }


    }
}
