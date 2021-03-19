using System;
using BLL.exception;
using BLL.Intarfaces;
using DAL.Entity;
using Microsoft.AspNetCore.Mvc;

namespace WEB.Controllers
{
    public class DeliveryPayController : Controller
    {
        private IBillService billService;

        public DeliveryPayController(IBillService billService)
        {
            this.billService = billService;
        }

        [HttpGet]
        public IActionResult userConfirmDelivers()
        {
            ViewData.Add("BillInfoToPayDtoList",
                billService.getBillsToPayByUserName(User.Identity.Name));
            return View();
        }

        [HttpPost]
        public IActionResult payForDelivery(long billId) {
            try
            {
                billService.payForDelivery(User.Identity.Name, billId);
            }
            catch (NotEnoughMoneyException e)
            {
                ViewData.Add("notEnoughMoneyException", true);
            }
            ViewData.Add("BillInfoToPayDtoList",
                billService.getBillsToPayByUserName(User.Identity.Name));
            return View("userConfirmDelivers");
        }
    }
}