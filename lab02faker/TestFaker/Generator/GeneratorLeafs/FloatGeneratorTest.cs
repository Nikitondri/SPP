using System;
using Faker.Faker.impl;
using Faker.Generator;
using Faker.Generator.Context;
using Faker.Generator.GeneratorLeafs;
using NUnit.Framework;

namespace TestFaker.Generator.GeneratorLeafs;

[TestFixture]
public class FloatGeneratorTest
{
        
    private readonly IValueGenerator _doubleGenerator = new FloatGenerator();
    private readonly GeneratorContext _context = new(new Random(), new FakerImpl());
    private readonly Type _type = typeof(float);
    
    [Test, Retry(5)]
    public void GenerateTest()
    {
        var result1 = (float)_doubleGenerator.Generate(_type, _context);
        var result2 = (float)_doubleGenerator.Generate(_type, _context);
        var result3 = (float)_doubleGenerator.Generate(_type, _context);
        Assert.True(result1.GetType() == _type && result2.GetType() == _type && result3.GetType() == _type);
        Assert.True(!result1.Equals(result2) && !result1.Equals(result3) && !result2.Equals(result3));
        Assert.True(result1 is >= float.MinValue and <= float.MaxValue);
        Assert.True(result2 is >= float.MinValue and <= float.MaxValue);
        Assert.True(result3 is >= float.MinValue and <= float.MaxValue);
    }
    
    [Test, Retry(5)]
    public void CanGenerateTest()
    {
        Assert.True(_doubleGenerator.Generate(_type, _context).GetType() == _type);
    }
}