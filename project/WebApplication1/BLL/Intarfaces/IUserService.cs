using System;
using System.Collections.Generic;
using BLL.dto;
using DAL.Entity;

namespace BLL.Intarfaces
{
    public interface IUserService
    {
        User FindByName(String email);

        User ReplenishAccountBalance(string userName, long amountMoney);
    }
}