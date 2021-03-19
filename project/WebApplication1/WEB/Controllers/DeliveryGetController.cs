using System;
using System.Security.Claims;
using BLL.impl;
using BLL.Intarfaces;
using DAL.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace WEB.Controllers
{
    public class DeliveryGetController : Controller
    {
        private readonly IDeliveryService _deliveryService;

        public DeliveryGetController(IDeliveryService deliveryService)
        {
            _deliveryService = deliveryService;
        }

        [HttpGet]
        [Authorize]
        public IActionResult userNotGottenDelivers()
        {
            ViewData.Add("DeliveryInfoToGetDtoList", _deliveryService.getDeliveryInfoToGet(User.Identity.Name));
            return View();
        }


        [HttpPost]
        [Authorize]
        public IActionResult userConfirmDeliveryPay(long deliveryId)
        {
            _deliveryService.confirmGettingDelivery(User.Identity.Name, deliveryId);
            ViewData.Add("DeliveryInfoToGetDtoList", _deliveryService.getDeliveryInfoToGet(User.Identity.Name));
            return View("userNotGottenDelivers");
        }
    }
}