using Faker.Exception;
using Faker.Generator.Context;

namespace Faker.Generator;

public class GeneratorComposite : IValueGenerator
{
    private readonly List<IValueGenerator> _generators;

    public GeneratorComposite()
    {
        _generators = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(a => a.GetTypes())
            .Where(t => t.GetInterfaces().Contains(typeof(IValueGenerator)) && t.IsClass && t != GetType())
            .Select(t => (IValueGenerator)Activator.CreateInstance(t)!).ToList();
    }

    public object Generate(Type typeToGenerate, GeneratorContext context)
    {
        foreach (var generator in _generators.Where(generator => generator.CanGenerate(typeToGenerate)))
        {
            return generator.Generate(typeToGenerate, context);
        }
        throw new CanNotGenerateException();
    }

    public bool CanGenerate(Type type)
    {
        return _generators.Any(generator => generator.CanGenerate(type));
    }
}