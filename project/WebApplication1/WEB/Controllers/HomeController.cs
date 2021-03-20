using System;
using BLL.dto;
using BLL.exception;
using BLL.impl;
using BLL.Intarfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WEB.Controllers
{
    public class HomeController : Controller
    {
        private IDeliveryService deliveryService;
        private ILocalityService localityService;

        public HomeController(IDeliveryService deliveryService, ILocalityService localityService)
        {
            this.deliveryService = deliveryService;
            this.localityService = localityService;
        }

        [HttpGet]
        public IActionResult Home()
        {
            ViewData.Add("localities", localityService.GetLocalities());
            return View("Home",new DeliveryInfoRequestModel());
        }


        [HttpPost]
        [Authorize]
        public IActionResult HomeCount(DeliveryInfoRequestModel deliveryInfoRequestDto)
        {
            try
            {
                ViewData.Add("localities", localityService.GetLocalities());
                ViewData.Add("priceAndTimeOnDeliveryModel",
                    deliveryService.GetDeliveryCostAndTimeDto(deliveryInfoRequestDto));
            }
            catch (NoSuchWayException e)
            {
                ViewData.Add("noSuchWayException", true);
            }
            catch (UnsupportableWeightFactorException e)
            {
                ViewData.Add("unsupportableWeightFactorException", true);
            }
            return View("Home");
        }
    }
}