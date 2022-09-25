using System.Collections.Generic;
using Faker.Faker;
using Faker.Faker.impl;
using NUnit.Framework;

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
}