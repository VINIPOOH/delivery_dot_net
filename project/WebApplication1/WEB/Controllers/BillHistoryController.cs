using BLL.impl;
using BLL.Intarfaces;
using Microsoft.AspNetCore.Mvc;

namespace WEB.Controllers
{
    public class BillHistoryController:Controller
    {
        private IBillService billService;

        public BillHistoryController(IBillService billService) {
            this.billService = billService;
        }


        [HttpGet]
        public IActionResult userStatistic() {
            ViewData.Add("BillDtoPage", billService.GetBillHistoryByUserName(User.Identity.Name));
            return View();
        }
    }
}