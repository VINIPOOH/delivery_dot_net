using BLL.exception;
using BLL.impl;
using BLL.Intarfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WEB.Controllers
{
    public class ProfileController : Controller
    {
        private readonly IUserService _userService;

        public ProfileController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [Authorize]
        public IActionResult userProfile()
        {
            ViewData.Add("userMoneyInCents",_userService.FindByName(User.Identity.Name).UserMoneyInCents);
            return View();
        }

        [HttpPost]
        [Authorize]
        public IActionResult userProfileReplenish(long money)
        {
            if (money <= 0) {
                ViewData.Add("incorrectMoney", true);
                return View("userProfile");
            }
            try
            {
                _userService.ReplenishAccountBalance(User.Identity.Name, money);
                ViewData.Add("userMoneyInCents",_userService.FindByName(User.Identity.Name).UserMoneyInCents);
            }
            catch (ToMuchMoneyException e)
            {
                ViewData.Add("incorrectMoney", true);
            }
            return View("userProfile");
        }
    }
}