using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelAutomationApp.Domain.Common
{
    public class BaseEntity
    {
        public BaseEntity()
        {
        }
        
        public BaseEntity(string id) =>
            Id = id;

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
    }
}