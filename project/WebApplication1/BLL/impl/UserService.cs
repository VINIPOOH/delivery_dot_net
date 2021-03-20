using System.Collections.Generic;
using System.Linq;
using BLL.dto;
using BLL.exception;
using BLL.Intarfaces;
using DAL;
using DAL.Entity;

namespace BLL.impl
{
    public class UserService:IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public User FindByName(string email)
        {
            User byEmail = _userRepository.FindByName(email);
            if (byEmail==null)
            {
                throw new UsernameNotFoundException();
            }

            return byEmail;
        }

        public User ReplenishAccountBalance(string userName, long amountMoney)
        {
            User user = _userRepository.FindByName(userName);
            if (user == null)
            {
                throw new NoSuchUserException();
            }
            if (user.UserMoneyInCents + amountMoney <= 0) {
                throw new ToMuchMoneyException();
            }
            user.UserMoneyInCents= (user.UserMoneyInCents + amountMoney);
            _userRepository.Save();
            return user;
        }
    }
}