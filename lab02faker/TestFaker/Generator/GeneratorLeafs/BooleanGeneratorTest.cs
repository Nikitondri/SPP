using System;
using Faker.Faker.impl;
using Faker.Generator;
using Faker.Generator.Context;
using Faker.Generator.GeneratorLeafs;
using NUnit.Framework;

namespace TestFaker.Generator.GeneratorLeafs;

[TestFixture]
public class BooleanGeneratorTest
{
    private readonly IValueGenerator _doubleGenerator = new BooleanGenerator();
    private readonly GeneratorContext _context = new(new Random(), new FakerImpl());
    private readonly Type _type = typeof(bool);
    
    [Test, Retry(5)]
    public void GenerateTest()
    {
        var result1 = (bool)_doubleGenerator.Generate(_type, _context);
        var result2 = (bool)_doubleGenerator.Generate(_type, _context);
        var result3 = (bool)_doubleGenerator.Generate(_type, _context);
        Assert.True(result1.GetType() == _type && result2.GetType() == _type && result3.GetType() == _type);
        Assert.True(result1 || !result1);
        Assert.True(result2 || !result2);
        Assert.True(result3 || !result3);
    }
    
    [Test, Retry(5)]
    public void CanGenerateTest()
    {
        Assert.True(_doubleGenerator.Generate(_type, _context).GetType() == _type);
    }
}