using System.Linq;
using ComputerNet.DAL.Repositories;
using DAL.Entity;
using WebApplication1.Dal;

namespace DAL.impl
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(MyDbContext context) : base(context)
        {
        }

        public User FindByEmail(string email)
        {
            return base.Get(user => user.Email.Equals(email)).FirstOrDefault();
        }
        
        public User FindByName(string userName)
        {
            return base.Get(user => user.UserName.Equals(userName)).FirstOrDefault();
        }

        public User FindByIdAndUserMoneyInCentsGreaterThanEqual(string userName, long userMoneyInCents)
        {
            return base.Get(user => user.UserName.Equals(userName) && user.UserMoneyInCents>=userMoneyInCents)
                .FirstOrDefault();
        }
    }
}