using System.ComponentModel.DataAnnotations;

namespace BLL.dto
{
    public class RegistrationInfoModel
    {
        [EmailAddress]
        [Required]
        public string Username{ get; set; }
        [Required]
        public string Password{ get; set; }
        [Required]
        public string PasswordRepeat{ get; set; }
    }
}