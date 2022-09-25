using Faker.Generator.Context;
using Faker.Generator.Interface;

namespace Faker.Generator.GeneratorLeafs;

public class DateTimeGenerator: IValueGenerator
{
    public object Generate(Type typeToGenerate, GeneratorContext context)
    {
        var start = new DateTime(1970, 1, 1);
        var range = (DateTime.Today - start).Days;
        return start.AddDays(context.Random.Next(range));
    }

    public bool CanGenerate(Type type)
    {
        return type == typeof(DateTime);
    }
}