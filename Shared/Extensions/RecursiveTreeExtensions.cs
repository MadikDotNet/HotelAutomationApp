using HotelAutomationApp.Shared.Common.Abstractions;

namespace HotelAutomationApp.Shared.Extensions;

public static class RecursiveTreeExtensions
{
    public static IEnumerable<T> AsRecursiveTree<T, TKey>(
            this IEnumerable<T> source,
            Func<T, TKey> parentKey,
            Func<T, TKey> childKey)
            where T : class, IRecursiveTree<T>
            where TKey : IEquatable<TKey>?
        {
            var parents = source.Where(q => !q.HasParent);
            var other = source.ExcludeSameElements(parents, parentKey).ToList();

            return parents.Select(q => q.MultiplyNodeChildren(
                null,
                other,
                parentKey,
                childKey));
        }

        private static T MultiplyNodeChildren<T, TKey>(
            this T node,
            T parent,
            List<T> possibleChildren,
            Func<T, TKey> parentKey,
            Func<T, TKey> childKey)
            where T : class, IRecursiveTree<T>
            where TKey : IEquatable<TKey>
        {
            if (!possibleChildren.Any())
            {
                return node;
            }

            node.Children = possibleChildren
                .ExcludeAfterFilter(item => childKey(item).Equals(parentKey(node)), out var remainElements)
                .Select(item => item.MultiplyNodeChildren(node, remainElements, parentKey, childKey)).ToList();
            
            return node;
        }

        /// <summary>
        /// Depth-first search
        /// </summary>
        /// <param name="source"></param>
        /// <param name="predicate"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T? ExtractNode<T>(
            this IEnumerable<T> source,
            Predicate<T> predicate)
            where T : class, IRecursiveTree<T>
        {
            foreach (var item in source)
            {
                if (predicate.Invoke(item))
                {
                    return item;
                }

                if (item.Children.Any() && item.Children.ExtractNode(predicate) is { } value)
                {
                    return value;
                }
            }

            return null;
        }
}