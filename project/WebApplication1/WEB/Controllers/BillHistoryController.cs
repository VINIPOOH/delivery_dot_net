using BLL.impl;
using BLL.Intarfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WEB.Controllers
{
    public class BillHistoryController:Controller
    {
        private readonly IBillService _billService;

        public BillHistoryController(IBillService billService) {
            this._billService = billService;
        }


        [HttpGet]
        [Authorize]
        public IActionResult userStatistic() {
            ViewData.Add("BillDtoPage", _billService.GetBillHistoryByUserName(User.Identity.Name));
            return View();
        }
    }
}