using toDoAPP.Models;

namespace toDoAPP.Interface
{
    public interface IUserInterface
    {
        IQueryable<User> Users { get; }
        void CreateUser(User User);
        User GetUserById(int id);
        User GetUserByUserName(string userName);
        void Update(User user);
    }
}

