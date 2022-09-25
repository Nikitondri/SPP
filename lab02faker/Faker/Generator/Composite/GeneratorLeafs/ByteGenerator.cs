using Faker.Generator.Context;

namespace Faker.Generator.GeneratorLeafs;

public class ByteGenerator : IValueGenerator
{
    public object Generate(Type typeToGenerate, GeneratorContext context)
    {
        return (byte)context.Random.Next(byte.MinValue, byte.MaxValue);
    }

    public bool CanGenerate(Type type)
    {
        return type == typeof(byte);
    }
}