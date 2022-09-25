using System;
using System.Collections.Generic;
using Faker.Exception;
using Faker.Faker;
using Faker.Faker.impl;
using NUnit.Framework;
using TestFaker.Faker.Creator;
using TestFaker.Faker.types;

namespace TestFaker.Faker;

[TestFixture]
public class FakerImplTest
{

    private readonly IFaker _faker = new FakerImpl();

    [Test]
    public void CreateListPrimitiveTest()
    {
        var list = _faker.Create<List<string>>();
        Assert.True(list.Count is > 0 and < 10);
        foreach (var str in list)
        {
            Assert.True(str.Length is > 0 and < 50);
        }
    }
    
    [Test]
    public void CreateListNoPrimitiveTest()
    {
        var list = _faker.Create<List<UserAndCat>>();
        Assert.True(list.Count is > 0 and < 10);
        foreach (var obj in list)
        {
            Assert.True(obj.cat.GetType() == typeof(Cat));
            Assert.True(obj.User.GetType() == typeof(User));
        }
    }

    [Test]
    public void StructureTest()
    {
        var structure = _faker.Create<Structure>();
        Assert.True(structure.name.Length is > 0 and < 50);
        Assert.True(structure.surname.Length is > 0 and < 50);
        Assert.True(structure.age is > int.MinValue and < int.MaxValue);
    }
    
    [Test]
    public void StructureWithConstructorTest()
    {
        var structure = _faker.Create<StructureWithConstructor>();
        Assert.True(structure.name.Length is > 0 and < 50);
        Assert.True(structure.surname.Length is > 0 and < 50);
        Assert.True(structure.age is > int.MinValue and < int.MaxValue);
    }

    [TestCase(typeof(int))]
    [TestCase(typeof(double))]
    [TestCase(typeof(float))]
    [TestCase(typeof(bool))]
    [TestCase(typeof(char))]
    [TestCase(typeof(string))]
    [TestCase(typeof(short))]
    [TestCase(typeof(byte))]
    [TestCase(typeof(DateTime))]
    public void CreatePrimitiveTest(Type type)
    {
        var result = _faker
            .GetType()
            .GetMethod("Create")?
            .MakeGenericMethod(type)
            .Invoke(_faker, new object[] { });
        Assert.True(result?.GetType() == type);
    }
    
    [Test]
    public void CreateUser()
    {
        var user = _faker.Create<User>();
        Assert.True(user.weight is > float.MinValue and < float.MaxValue);
        Assert.True(user.Age is > int.MinValue and < int.MaxValue);
        Assert.True(user.Name.Length is > 0 and < 50 && !user.Name.Contains(" "));
    }
    
    [Test]
    public void CreateCat()
    {
        var cat = _faker.Create<Cat>();
        Assert.True(cat.getAge() is > int.MinValue and < int.MaxValue);
        Assert.True(cat.Name.Length is > 0 and < 50 && !cat.Name.Contains(" "));
        Assert.True(cat.type.Length is > 0 and < 50 && !cat.type.Contains(" "));
    }

    [Test]
    public void CreateUserAndCat()
    {
        var userAndCat = _faker.Create<UserAndCat>();
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
        var obj = _faker.Create<ConstructorWithException>();
        Assert.True(obj.Field1.Length is > 0 and < 50 && !obj.Field1.Contains(" "));
        Assert.True(obj.Field2.Length is > 0 and < 50 && !obj.Field2.Contains(" "));
        Assert.True(obj.Field3.Length is > 0 and < 50 && !obj.Field3.Contains(" "));
    }
    
    [Test]
    public void CycleDependencyTest()
    {
        var obj = _faker.Create<Parent>();
        Assert.True(obj.Name.Length is > 0 and < 50);
        Assert.True(obj.parents[0].Name.Length is > 0 and < 50);
        Assert.True(obj.parents[0].parents[0].Name.Length is > 0 and < 50);
        Assert.True(obj.parents[0].parents[0].parents[0].Name.Length is > 0 and < 50);
        Assert.True(obj.parents[0].parents[0].parents[0].parents[0].Name.Length is > 0 and < 50);
        Assert.Null(obj.parents[0].parents[0].parents[0].parents[0].parents[0]);
    }

    [Test]
    public void CreateNoConstructor()
    {
        Assert.Throws<ConstructorException>(() => _faker.Create<NoConstructor>());
    }
}