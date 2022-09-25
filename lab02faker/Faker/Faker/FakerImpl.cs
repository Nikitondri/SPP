using Faker.Generator;
using Faker.Generator.Context;
using Faker.Generator.Interface;

namespace Faker.Faker;

public class FakerImpl : IFaker
{

    private IValueGenerator _generator;
    private GeneratorContext _context;

    public FakerImpl()
    {
        _generator = new GeneratorComposite();
        _context = new GeneratorContext(new Random((int)DateTime.Now.Ticks), this);
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
        throw new Exception();
    }
}