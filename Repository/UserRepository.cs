using Microsoft.EntityFrameworkCore;
using toDoAPP.Interface;
using toDoAPP.Models;

namespace toDoAPP.Repository
{
    public class UserRepository : IUserInterface
    {
        private DataContext _context;
        public UserRepository(DataContext contex)
        {        
            _context = contex; 
        }
        public IQueryable<User> Users => _context.Users;

        public void CreateUser(User User)
        {
            _context.Users.Add(User);
            _context.SaveChanges();
        }

        public User GetUserById(int id)
        {
            var user = _context.Users.Find(id);
            return user;
        }

        public User GetUserByUserName(string userName)
        {
            var user = _context.Users.Find(userName);
            return user;
        }

        public void Update(User user)
        {
            _context.Update(user);
            _context.SaveChanges();
        }
    }
}
