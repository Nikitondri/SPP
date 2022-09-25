using Faker.Generator.Context;
using Faker.Generator.Interface;

namespace Faker.Generator.GeneratorLeafs;

public class ShortGenerator : IValueGenerator
{
    public object Generate(Type typeToGenerate, GeneratorContext context)
    {
        return (short)context.Random.Next(short.MinValue, short.MaxValue);
    }

    public bool CanGenerate(Type type)
    {
        return type == typeof(short);
    }
}