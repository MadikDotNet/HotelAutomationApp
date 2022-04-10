using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelAutomationApp.Domain.Common
{
    public class BaseEntity : IEquatable<BaseEntity>
    {
        public BaseEntity()
        {
        }
        
        public BaseEntity(string id) =>
            Id = id;

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }

        public bool Equals(BaseEntity? other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Id == other.Id;
        }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((BaseEntity) obj);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}