namespace HotelAutomationApp.Shared.Common.Abstractions;

/// <summary>
/// Represents entity relation as parent/children
/// </summary>
/// <typeparam name="T">Type of entities</typeparam>
public interface IRecursiveTree<T>
    where T : class
{
    public T? Parent { get; set; }
    public ICollection<T> Children { get; set; }
    public bool HasParent { get; }
}