using System;
using DAL.Entity;

namespace integration_test
{
    public class EntitySetuper
    {
        public static int MAX_WRIGHT_ON_SETUPED_TARIF_WEIGHT_FACTOR = 100;
        public static int BILL_COST = 10;
        public static Delivery SetupDeliveryAndBill(MockDbContext _context, bool isDeliveryRecived, bool isDeliveryPayed)
        {
            Way way = SetupWayWithTarif(_context);
            User addressee = SetupAdresee(_context);
            User addresser = SetupAdreser(_context);
            
            Delivery delivery = new Delivery();
            delivery.Addressee = addressee;
            delivery.AddresseeUserId = addressee.Id;
            delivery.Way = way;
            delivery.WayId = way.WayId;
            delivery.IsPackageReceived = isDeliveryRecived;
            delivery.Weight = 11;
            _context.Add(delivery);
            _context.SaveChanges();
            Bill bill =new Bill();
            bill.DeliveryId = delivery.DeliveryId;
            bill.UserId = addresser.Id;
            bill.Delivery = delivery;
            bill.User = addresser;
            bill.IsDeliveryPaid = isDeliveryPayed;
            bill.CostInCents = BILL_COST;
            bill.DateOfPay = DateTime.Today;
            _context.Add(bill);
            _context.SaveChanges();

            return delivery;
        }

        public static User SetupAdresee(MockDbContext _context)
        {
            User addressee = new User();
            addressee.UserName = "ivan_v123@ukr.net";
            addressee.Email = "ivan_v123@ukr.net";
            addressee.Password = "password";
            addressee.UserMoneyInCents = 9000000000;
            _context.Add(addressee);
            _context.SaveChanges();
            return addressee;
        }
        
        public static User SetupAdreser(MockDbContext _context)
        {
            User addresser = new User();
            addresser.UserName = "ivan@ukr.net";
            addresser.Email = "ivan@ukr.net";
            addresser.Password = "password";
            addresser.UserMoneyInCents = 8000000000;
            _context.Add(addresser);
            _context.SaveChanges();
            return addresser;
        }

        public static Way SetupWayWithTarif(MockDbContext _context)
        {
            Locality send = new Locality();
            send.NameEn = "localitySend";
            _context.Add(send);
            _context.SaveChanges();
            Locality gett = new Locality();
            gett.NameEn = "localityGet";
            _context.Add(gett);
            _context.SaveChanges();
            Way way = new Way(send.LocalityId, send, gett.LocalityId, gett, 10,2,10);
            _context.Add(way);
            _context.SaveChanges();
            TariffWeightFactor tariffWeightFactor = new TariffWeightFactor();
            tariffWeightFactor.MaxWeightRange = MAX_WRIGHT_ON_SETUPED_TARIF_WEIGHT_FACTOR;
            tariffWeightFactor.MinWeightRange = 0;
            tariffWeightFactor.OverPayOnKilometer = 10;
            _context.Add(tariffWeightFactor);
            _context.SaveChanges();
            WayToTariffWeightFactor wayToTariffWeightFactor = new WayToTariffWeightFactor();
            wayToTariffWeightFactor.WayId = way.WayId;
            wayToTariffWeightFactor.Way = way;
            wayToTariffWeightFactor.TariffWeightFactorId = tariffWeightFactor.TariffWeightFactorId;
            wayToTariffWeightFactor.TariffWeightFactor = tariffWeightFactor;
            _context.Add(wayToTariffWeightFactor);
            _context.SaveChanges();
            return way;
        } 
    }
}