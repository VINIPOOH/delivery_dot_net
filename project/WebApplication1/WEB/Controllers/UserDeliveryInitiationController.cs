using System;
using BLL.dto;
using BLL.exception;
using BLL.Intarfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WEB.Controllers
{
    public class UserDeliveryInitiationController : Controller
    {
        private static string REDIRECT_USER_USER_DELIVERY_INITIATION = "redirect:/user/user-delivery-initiation";

        private readonly IBillService _billService;
        private readonly ILocalityService _localityService;

        public UserDeliveryInitiationController(IBillService billService, ILocalityService localityService)
        {
            this._billService = billService;
            this._localityService = localityService;
        }

        [HttpGet]
        [Authorize]
        public IActionResult userDeliveryInitiation()
        {
            ViewData.Add("localities", _localityService.GetLocalities());
            return View();
        }

        [HttpPost]
        [Authorize]
        public IActionResult userDeliveryInitiationPost(DeliveryOrderCreateModel deliveryOrderCreateDto)
        {
            ViewData.Add("localities", _localityService.GetLocalities());
            try
            {
                _billService.InitializeBill(deliveryOrderCreateDto, User.Identity.Name);
            }
            catch (NoSuchWayException e)
            {
                ViewData.Add("noSuchWayException", true);
            }
            catch (UnsupportableWeightFactorException e)
            {
                ViewData.Add("unsupportableWeightFactorException", true);
            }
            catch (NoSuchUserException e)
            {
                ViewData.Add("noSuchUserException", true);
            }
            return View("userDeliveryInitiation");
        }
    }
}