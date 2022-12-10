using System.Collections.Concurrent;

namespace App.repository.cache;

public class StringFormatterCache
{
    private readonly ConcurrentDictionary<string, Func<object, string>> _cache = new();

    public bool IsExistElement(string key)
    {
        return _cache.ContainsKey(key);
    }

    public void AddElement(string key, Func<object, string> value)
    {
        _cache.TryAdd(key, value);
    }

    public Func<object, string> GetElement(string key)
    {
        return _cache[key];
    }
}