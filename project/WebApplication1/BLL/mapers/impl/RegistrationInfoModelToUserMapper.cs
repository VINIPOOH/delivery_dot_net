using BLL.dto;
using DAL.Entity;

namespace BLL.mapers.impl
{
    public class RegistrationInfoModelToUserMapper
    {
        public static User map(RegistrationInfoModel registrationInfoModel)
        {
            User user = new User();
            user.Email = registrationInfoModel.Username;
            user.Password = registrationInfoModel.Password;
            return user;
        }
    }
}