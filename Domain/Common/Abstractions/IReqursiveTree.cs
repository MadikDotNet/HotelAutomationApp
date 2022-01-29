namespace HotelAutomationApp.Domain.Common.Abstractions;

/// <summary>
/// Represents entity relation as parent/children
/// </summary>
/// <typeparam name="T">Type of entities</typeparam>
public interface IRecursiveTree<T>
{
    public string ParentId { get; set; }
    public T Parent { get; set; }
    public ICollection<T> Children { get; set; }
    public bool HasParent => !string.IsNullOrEmpty(ParentId);
}