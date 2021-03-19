using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace DAL.Entity
{
    public class User : IdentityUser
    {
        public User(string userName, string password, long userMoneyInCents) : base(userName)
        {
            Password = password;
            UserMoneyInCents = userMoneyInCents;
        }

        public User()
        {
        }

        public List<Bill> Bills { get; set; }
        [InverseProperty("Addressee")]
        public List<Delivery> Deliveries{ get; set; }
        [Required]
        public string Password{ get; set; }
        [Required]
        public long UserMoneyInCents{ get; set; }

        protected bool Equals(User other)
        {
            return Password == other.Password && UserMoneyInCents == other.UserMoneyInCents;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((User) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((Password != null ? Password.GetHashCode() : 0) * 397) ^ UserMoneyInCents.GetHashCode();
            }
        }
    }
}