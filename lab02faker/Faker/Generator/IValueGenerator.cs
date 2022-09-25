using Faker.Generator.Context;

namespace Faker.Generator;

public interface IValueGenerator
{
    object Generate(Type typeToGenerate, GeneratorContext context);

    bool CanGenerate(Type type);
}