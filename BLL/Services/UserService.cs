using BLL.DAL;
using BLL.Models;
using BLL.Services.Bases;

namespace BLL.Services
{
    // Way 1:
    //public interface IUserService
    //{
    //    public IQueryable<UserModel> Query();
    //    public Service Create(User user);
    //    public Service Update(User user);
    //    public Service Delete(int id);
    //}

    // Way 1:
    //public class UserService : Service, IUserService
    // Way 2:
    public class UserService : Service, IService<User, UserModel>
    {
        public UserService(Db db) : base(db)
        {
        }

        public Service Create(User entity)
        {
            throw new NotImplementedException();
        }

        public Service Delete(int id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// This query will return the active users.
        /// </summary>
        /// <returns></returns>
        public IQueryable<UserModel> Query()
        {
            return _db.Users.Where(u => u.IsActive).Select(u => new UserModel() { Record = u });
        }

        public Service Update(User entity)
        {
            throw new NotImplementedException();
        }
    }
}
