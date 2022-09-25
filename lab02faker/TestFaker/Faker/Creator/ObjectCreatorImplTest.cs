using Faker.Exception;
using Faker.Faker.Creator;
using Faker.Faker.impl;
using NUnit.Framework;
using TestFaker.Faker.types;

namespace TestFaker.Faker.Creator;

[TestFixture]
public class ObjectCreatorImplTest
{
    
    private readonly IObjectCreator _creator = new ObjectCreatorImpl(new FakerImpl());

    [Test]
    public void CreateUser()
    {
        var user = (User)_creator.Create(typeof(User));
        Assert.True(user.weight is > float.MinValue and < float.MaxValue);
        Assert.True(user.Age is > int.MinValue and < int.MaxValue);
        Assert.True(user.Name.Length is > 0 and < 50 && !user.Name.Contains(" "));
    }
    
    [Test]
    public void CreateCat()
    {
        var cat = (Cat)_creator.Create(typeof(Cat));
        Assert.True(cat.getAge() is > int.MinValue and < int.MaxValue);
        Assert.True(cat.Name.Length is > 0 and < 50 && !cat.Name.Contains(" "));
        Assert.True(cat.type.Length is > 0 and < 50 && !cat.type.Contains(" "));
    }

    [Test]
    public void CreateUserAndCat()
    {
        var userAndCat = (UserAndCat)_creator.Create(typeof(UserAndCat));
        //user
        Assert.True(userAndCat.User.weight is > float.MinValue and < float.MaxValue);
        Assert.True(userAndCat.User.Age is > int.MinValue and < int.MaxValue);
        Assert.True(userAndCat.User.Name.Length is > 0 and < 50 && !userAndCat.User.Name.Contains(" "));
        //cat
        Assert.True(userAndCat.cat.getAge() is > int.MinValue and < int.MaxValue);
        Assert.True(userAndCat.cat.Name.Length is > 0 and < 50 && !userAndCat.cat.Name.Contains(" "));
        Assert.True(userAndCat.cat.type.Length is > 0 and < 50 && !userAndCat.cat.type.Contains(" "));
        
    }

    [Test]
    public void CreateConstructorWithException()
    {
        var obj = (ConstructorWithException)_creator.Create(typeof(ConstructorWithException));
        Assert.True(obj.Field1.Length is > 0 and < 50 && !obj.Field1.Contains(" "));
        Assert.True(obj.Field2.Length is > 0 and < 50 && !obj.Field2.Contains(" "));
        Assert.True(obj.Field3.Length is > 0 and < 50 && !obj.Field3.Contains(" "));
    }

    [Test]
    public void CreateNoConstructor()
    {
        Assert.Throws<ConstructorException>(() => _creator.Create(typeof(NoConstructor)));
    }
}