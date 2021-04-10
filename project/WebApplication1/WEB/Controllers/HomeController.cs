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
        private readonly IDeliveryService _deliveryService;
        private readonly ILocalityService _localityService;

        public HomeController(IDeliveryService deliveryService, ILocalityService localityService)
        {
            this._deliveryService = deliveryService;
            this._localityService = localityService;
        }

        [HttpGet]
        public IActionResult Home()
        {
            ViewData.Add("localities", _localityService.GetLocalities());
            return View("Home",new DeliveryInfoRequestModel());
        }


        [HttpPost]
        public IActionResult HomeCount(DeliveryInfoRequestModel deliveryInfoRequestDto)
        {
            try
            {
                ViewData.Add("localities", _localityService.GetLocalities());
                ViewData.Add("priceAndTimeOnDeliveryModel",
                    _deliveryService.GetDeliveryCostAndTimeDto(deliveryInfoRequestDto));
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