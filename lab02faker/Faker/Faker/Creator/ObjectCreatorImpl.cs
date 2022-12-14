using System.Reflection;
using Faker.Exception;

namespace Faker.Faker.Creator;

public class ObjectCreatorImpl : IObjectCreator
{
    private readonly IFaker _faker;

    public ObjectCreatorImpl(IFaker faker)
    {
        _faker = faker;
    }

    public object Create(Type type)
    {
        var constructors = FindConstructorWithMaxParams(type);
        var result = CreateByConstructor(constructors, type);
        FillFieldsByProperties(type, ref result);
        return result;
    }

    private static IEnumerable<ConstructorInfo> FindConstructorWithMaxParams(Type type)
    {
        try
        {
            return type
                .GetConstructors()
                .OrderByDescending(info => info.GetParameters().Length);
        }
        catch (System.Exception)
        {
            throw new CreateObjectException();
        }
    }

    private object CreateByConstructor(IEnumerable<ConstructorInfo> constructors, Type type)
    {
        foreach (var constructorInfo in constructors)
        {
            try
            {
                var paramValues = new object[constructorInfo.GetParameters().Length];
                var parameters = constructorInfo.GetParameters();

                for (var i = 0; i < paramValues.Length; i++)
                {
                    paramValues[i] = InvokeCreateFaker(parameters[i].ParameterType)!;
                }
                return constructorInfo.Invoke(paramValues);
            }
            catch (System.Exception)
            {
                // ignored
            }
        }

        try
        {
            return Activator.CreateInstance(type)!;
        }
        catch (System.Exception)
        {
            // ignored
        }
        throw new CreateObjectException();
    }

    private void FillFieldsByProperties(Type type, ref object result)
    {
        var fields = type.GetFields();
        var properties = type.GetProperties();
        foreach (var field in fields)
        {
            field.SetValue(result, InvokeCreateFaker(field.FieldType));
        }

        foreach (var prop in properties)
        {
            if (prop.CanWrite)
            {
                prop.SetValue(result, InvokeCreateFaker(prop.PropertyType));
            }
        }
    }

    private object? InvokeCreateFaker(Type type)
    {
        return _faker
            .GetType()
            .GetMethod("Create")?
            .MakeGenericMethod(type)
            .Invoke(_faker, new object[] { });
    }
}