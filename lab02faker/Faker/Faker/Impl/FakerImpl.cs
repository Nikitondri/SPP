using Faker.Faker.Checker;
using Faker.Faker.Creator;
using Faker.Generator;
using Faker.Generator.Composite;
using Faker.Generator.Context;

namespace Faker.Faker.impl;

public class FakerImpl : IFaker
{

    private readonly IValueGenerator _generator;
    private readonly GeneratorContext _context;
    private readonly IObjectCreator _creator;
    private readonly IDepthDependencyChecker _checker;

    public FakerImpl()
    {
        _generator = new GeneratorComposite();
        _context = new GeneratorContext(new Random((int)DateTime.Now.Ticks), this);
        _creator = new ObjectCreatorImpl(this);
        _checker = new DepthDependencyCheckerImpl();
    }

    public T Create<T>()
    {
        var type = typeof(T);
        object result;
        _checker.Add(type);
        if (!_checker.IsMaxDepth())
        {
            result = (T)GetValue(typeof(T))!;
        }
        else
        {
            result = null!;
        }
        _checker.Delete(type);
        return (T)result;
    }

    private object GetValue(Type type)
    {
        return _generator.CanGenerate(type) ? _generator.Generate(type, _context) : _creator.Create(type);
    }
}