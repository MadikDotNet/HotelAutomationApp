using System.ComponentModel.DataAnnotations.Schema;
using HotelAutomationApp.Domain.Common.Abstractions;
using HotelAutomationApp.Domain.Models.ValueObjects;

namespace HotelAutomationApp.Domain.Common
{
    public class BaseDictionary<TDictionary> : BaseEntity, IRecursiveTree<TDictionary>
    {
        public BaseDictionary(
            string name,
            string code,
            string description,
            string parentId,
            ICollection<TDictionary> children)
        {
            Name = name;
            Code = code;
            Description = description;
            ParentId = parentId;
            Children = children;
        }
        
        public BaseDictionary()
        {
            
        }
        
        public Name Name { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        [ForeignKey(nameof(Parent))]
        public string? ParentId { get; set; }
        public TDictionary Parent { get; set; }
        public ICollection<TDictionary> Children { get; set; }
    }
}