using System.Reflection;

namespace HotelAutomationApp.Shared.Common.Abstractions
{
    public abstract class Enumeration<T> where T : Enumeration<T>
    {
        protected Enumeration(int key, string name) =>
            (Key, Name) = (key, name);

        public int Key { get; }
        public string Name { get; }

        private static readonly Lazy<List<T>> _values = new(GetValues, true);
        
        private static List<T> GetValues()
        {
            return typeof(T)
                .GetProperties(BindingFlags.Public | BindingFlags.Static)
                .Where(p => p.PropertyType == typeof(T))
                .Select(p => (T)p.GetValue(null)!)
                .ToList();
        }

        protected virtual IEnumerable<object> GetAtomicValues()
        {
            yield return Key;
            yield return Name;
        }

        private static List<T> Values => _values.Value;

        public static T Get(int key) => Values.First(q => q.Key == key);

        public static T Get(string? name) => Values.First(q => q.Name == name);

        public static IEnumerable<int> Keys => Values.Select(value => value.Key);

        public static IEnumerable<string> Names => Values.Select(value => value.Name);

        public static bool TryGet(int key, out T? type)
        {
            type = Values.FirstOrDefault(q => q.Key == key);

            return type != null;
        }
        
        public static bool TryGet(string name, out T? type)
        {
            type = Values.FirstOrDefault(q => q.Name == name);

            return type != null;
        }
    }
}