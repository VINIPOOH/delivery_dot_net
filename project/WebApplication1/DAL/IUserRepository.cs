using System;
using ComputerNet.DAL.Interfaces;
using DAL.Entity;
using Utils;

namespace DAL
{
    public interface IUserRepository: IGenericRepository<User>
    {
        User FindByEmail(string email);

        User FindByName(string userName);

        User FindByIdAndUserMoneyInCentsGreaterThanEqual(string userName, long userMoneyInCents);
    }
}