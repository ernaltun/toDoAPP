using toDoAPP.Models;
using Task = toDoAPP.Models.Task;

namespace toDoAPP.Interface
{
	public interface ITaskInterface
	{
		IEnumerable<Task> GetAll();
		Task GetById(int id);
		void Insert(Task task);
		void Update(Task task);
        List<Task> GetByUser(int userId);
        void Delete(int id);
		void Save();
	} 
}
