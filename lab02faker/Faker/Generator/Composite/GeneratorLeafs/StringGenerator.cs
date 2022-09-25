using System.Text;
using Faker.Generator.Context;

namespace Faker.Generator.GeneratorLeafs;

public class StringGenerator  : IValueGenerator
{
    public object Generate(Type typeToGenerate, GeneratorContext context)
    {
        var result = new StringBuilder();
        for (var i = 0; i < context.Random.Next(1, 50); i++)
        {
            var ch = (char)('a' + context.Random.Next(0, 26));
            result.Append(ch);
        }

        return result.ToString();
    }

    public bool CanGenerate(Type type)
    {
        return type == typeof(string);
    }
}