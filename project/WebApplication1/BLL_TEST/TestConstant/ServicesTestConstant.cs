using System.Collections.Generic;
using BLL.dto;
using DAL.Entity;
using Microsoft.EntityFrameworkCore.Metadata;

namespace BLL_TEST.TestConstant
{
    public class ServicesTestConstant
    {
        private static string USER_ID = "userId";
        private static long BILL_ID = 1L;


        private static long DELIVERY_ID = 1L;

        public static string getUserId()
        {
            return USER_ID;
        }

        public static long getBillId()
        {
            return BILL_ID;
        }

        public static long getDeliveryId()
        {
            return DELIVERY_ID;
        }

        public static List<TariffWeightFactor> getTariffWeightFactors()
        {
            return new List<TariffWeightFactor> {getTariffWeightFactor()};
        }

        public static List<Delivery> getDeliveres()
        {
            return new List<Delivery>() {getDelivery()};
        }

        public static DeliveryInfoToGetDto getDeliveryInfoToGetDto()
        {
            return new DeliveryInfoToGetDto(getAddreser().Email, 1L,
                "localitySendName", "localityGetName");
        }

        public static List<BillModel> getBillDtos()
        {
            return new List<BillModel> {getBillDto()};
        }

        public static List<Bill> getBills()
        {
            return new List<Bill> {getBill()};
        }

        public static User getAdversee()
        {
            User user = new User("userName", "password", 0L);
            user.Id = USER_ID;
            return user;
        }

        public static List<User> getUsers()
        {
            return new List<User> {getAddreser()};
        }

        public static User getAddreser()
        {
            User user = new User("userName", "password", 300L);
            user.Id = USER_ID;
            user.Email = "email";
            return user;
        }

        public static BillInfoToPayModel getBillInfoToPayDto()
        {
            Bill bill = getBill();
            return new BillInfoToPayModel(bill.BillId, bill.CostInCents, bill.DeliveryId, bill.Delivery.Weight,
                bill.Delivery.Addressee.Email);
        }

        public static WayToTariffWeightFactor GetWayToTariffWeightFactor()
        {
            return new WayToTariffWeightFactor(1, 1, getTariffWeightFactor());
        }

        public static List<WayToTariffWeightFactor> GetWayToTariffWeightFactors()
        {
            return new List<WayToTariffWeightFactor>() {GetWayToTariffWeightFactor()};
        }

        public static TariffWeightFactor getTariffWeightFactor()
        {
            return new TariffWeightFactor(1l, 0, 100, 1);
        }

        public static Locality getLocalitySend()
        {
            return new Locality(1, "Send");
        }

        public static Locality getLocalityGet()
        {
            return new Locality(2, "Get");
        }

        public static List<Locality> getLocalities()
        {
            return new List<Locality> {getLocalityGet()};
        }

        public static Way getWay()
        {
            return new Way(getLocalitySend().LocalityId, getLocalitySend(),
                getLocalityGet().LocalityId, getLocalityGet(),
                1L, 1, 1, 1, GetWayToTariffWeightFactors());
        }

        public static Delivery getDelivery()
        {
            return new Delivery(1L, getWay().WayId, getWay(), getAdversee().Id, getAddreser(), false, 1);
        }

        public static Bill getBill()
        {
            return new Bill(getDelivery().DeliveryId, getDelivery(), 1L, true,
                1, getAddreser().Id, getAddreser());
        }

        public static BillModel getBillDto()
        {
            return new BillModel(1L, 1L, true, 1);
        }

        public static DeliveryOrderCreateModel getDeliveryOrderCreateDto()
        {
            return new DeliveryOrderCreateModel(1, 2, 1, "email");
        }
    }
}