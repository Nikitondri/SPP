using System.Collections;
using Faker.Generator.Context;
using Faker.Generator.Interface;

namespace Faker.Generator.GeneratorLeafs;

public class ListGenerator : IValueGenerator
{
    public object Generate(Type typeToGenerate, GeneratorContext context)
    {
        var collection = (IList)Activator.CreateInstance(typeToGenerate)!;
        var createMethod = context.Faker
            .GetType()
            .GetMethod("Create")?
            .MakeGenericMethod(typeToGenerate.GetGenericArguments()[0]);
        for (var i = 0; i < context.Random.Next(1, 10); i++)
        {
            collection.Add(createMethod?.Invoke(context.Faker, new object?[] {}));
        }
        return collection;
    }
    
    public bool CanGenerate(Type type)
    {
        return type.IsGenericType && type.GetGenericTypeDefinition() == typeof(List<>);
    }
}