using Faker.Generator.Context;

namespace Faker.Generator.GeneratorLeafs;

public class FloatGenerator : IValueGenerator
{
    public object Generate(Type typeToGenerate, GeneratorContext context)
    {
        return context.Random.NextSingle();
    }

    public bool CanGenerate(Type type)
    {
        return type == typeof(float);
    }
}