using Faker.Generator.Context;

namespace Faker.Generator.GeneratorLeafs;

public class DoubleGenerator : IValueGenerator
{
    public object Generate(Type typeToGenerate, GeneratorContext context)
    {
        return context.Random.NextDouble();
    }

    public bool CanGenerate(Type type)
    {
        return type == typeof(double);
    }
}