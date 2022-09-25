using Faker.Generator.Context;

namespace Faker.Generator.GeneratorLeafs;

public class BooleanGenerator : IValueGenerator
{
    public object Generate(Type typeToGenerate, GeneratorContext context)
    {
        return context.Random.Next(0, 1) == 0;
    }

    public bool CanGenerate(Type type)
    {
        return type == typeof(bool);
    }
}