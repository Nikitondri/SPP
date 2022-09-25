namespace Faker.Faker.Checker;

public class DepthDependencyCheckerImpl : IDepthDependencyChecker
{
    private const int MaxDepth = 5;

    private readonly Dictionary<Type, int> _types;

    public DepthDependencyCheckerImpl()
    {
        _types = new Dictionary<Type, int>();
    }

    public void Add(Type type)
    {
        if (_types.ContainsKey(type))
        {
            _types[type] += 1;
        }
        else
        {
            _types.Add(type, 1);
        }
    }

    public void Delete(Type type)
    {
        if (_types.ContainsKey(type) && _types[type] > 1)
        {
            _types[type] -= 1;
        }
        else
        {
            _types.Remove(type);
        }
    }

    public bool IsMaxDepth()
    {
        return _types.Values.Any(typeCount => typeCount > MaxDepth);
    }
}