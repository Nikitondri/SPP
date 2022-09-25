using Faker.Generator.Context;
using Faker.Generator.Interface;

namespace Faker.Generator.GeneratorLeafs;

public class IntGenerator : IValueGenerator
{
    public object Generate(Type typeToGenerate, GeneratorContext context)
    {
        return context.Random.Next(int.MinValue, int.MaxValue);
    }

    public bool CanGenerate(Type type)
    {
        return type == typeof(int);
    }
}