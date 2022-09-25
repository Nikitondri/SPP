using Faker.Exception;
using Faker.Faker.Creator;
using Faker.Generator;
using Faker.Generator.Context;

namespace Faker.Faker.impl;

public class FakerImpl : IFaker
{

    private readonly IValueGenerator _generator;
    private readonly GeneratorContext _context;
    private readonly IObjectCreator _creator;

    public FakerImpl()
    {
        _generator = new GeneratorComposite();
        _context = new GeneratorContext(new Random((int)DateTime.Now.Ticks), this);
        _creator = new ObjectCreatorImpl(this);
    }

    public T Create<T>()
    {
        return (T)GetValue(typeof(T));
    }

    private object GetValue(Type type)
    {
        if (_generator.CanGenerate(type))
        {
            return _generator.Generate(type, _context);
        }
        return _creator.Create(type);
        // throw new CanNotGenerateException();
    }
}