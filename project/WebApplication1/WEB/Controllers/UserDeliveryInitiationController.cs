using System;
using BLL.dto;
using BLL.exception;
using BLL.Intarfaces;
using Microsoft.AspNetCore.Mvc;

namespace WEB.Controllers
{
    public class UserDeliveryInitiationController : Controller
    {
        private static string REDIRECT_USER_USER_DELIVERY_INITIATION = "redirect:/user/user-delivery-initiation";

        private IBillService billService;
        private ILocalityService localityService;

        public UserDeliveryInitiationController(IBillService billService, ILocalityService localityService)
        {
            this.billService = billService;
            this.localityService = localityService;
        }

        [HttpGet]
        public IActionResult userDeliveryInitiation()
        {
            ViewData.Add("localities", localityService.getLocalities());
            return View();
        }

        [HttpPost]
        public IActionResult userDeliveryInitiationPost(DeliveryOrderCreateModel deliveryOrderCreateDto)
        {
            ViewData.Add("localities", localityService.getLocalities());
            try
            {
                billService.initializeBill(deliveryOrderCreateDto, User.Identity.Name);
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