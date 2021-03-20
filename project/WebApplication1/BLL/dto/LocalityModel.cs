using System;

namespace BLL.dto
{
    public class LocalityModel
    {
        public long Id{ get; set; }
        public string Name{ get; set; }

        public LocalityModel(long id, string name)
        {
            Id = id;
            Name = name;
        }

        protected bool Equals(LocalityModel other)
        {
            return Id == other.Id && Name == other.Name;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((LocalityModel) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (Id.GetHashCode() * 397) ^ (Name != null ? Name.GetHashCode() : 0);
            }
        }
    }
}