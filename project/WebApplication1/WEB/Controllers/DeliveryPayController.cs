using System;
using BLL.exception;
using BLL.Intarfaces;
using DAL.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WEB.Controllers
{
    public class DeliveryPayController : Controller
    {
        private readonly IBillService _billService;

        public DeliveryPayController(IBillService billService)
        {
            this._billService = billService;
        }

        [HttpGet]
        [Authorize]
        public IActionResult userConfirmDelivers()
        {
            ViewData.Add("BillInfoToPayDtoList",
                _billService.GetBillsToPayByUserName(User.Identity.Name));
            return View();
        }

        [HttpPost]
        [Authorize]
        public IActionResult PayForDelivery(long billId) {
            try
            {
                _billService.PayForDelivery(User.Identity.Name, billId);
            }
            catch (NotEnoughMoneyException e)
            {
                ViewData.Add("notEnoughMoneyException", true);
            }
            ViewData.Add("BillInfoToPayDtoList",
                _billService.GetBillsToPayByUserName(User.Identity.Name));
            return View("userConfirmDelivers");
        }
    }
}