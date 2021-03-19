using System;
using ComputerNet.DAL.Interfaces;
using DAL.Entity;
using Utils;

namespace DAL
{
    public interface IUserRepository: IGenericRepository<User>
    {
        User findByEmail(String email);

        User findByName(string userName);

        User findByIdAndUserMoneyInCentsGreaterThanEqual(string userName, long userMoneyInCents);
    }
}