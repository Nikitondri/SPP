using System;
using Faker.Faker.impl;
using Faker.Generator;
using Faker.Generator.Context;
using Faker.Generator.GeneratorLeafs;
using NUnit.Framework;

namespace TestFaker.Generator.GeneratorLeafs;

[TestFixture]
public class CharGeneratorTest
{
        
    private readonly IValueGenerator _doubleGenerator = new CharGenerator();
    private readonly GeneratorContext _context = new(new Random(), new FakerImpl());
    private readonly Type _type = typeof(char);
    
    [Test, Retry(5)]
    public void GenerateTest()
    {
        var result1 = (char)_doubleGenerator.Generate(_type, _context);
        var result2 = (char)_doubleGenerator.Generate(_type, _context);
        var result3 = (char)_doubleGenerator.Generate(_type, _context);
        Assert.True(result1.GetType() == _type && result2.GetType() == _type && result3.GetType() == _type);
        Assert.True(!result1.Equals(result2) && !result1.Equals(result3) && !result2.Equals(result3));
        Assert.True(result1 > 0 && result1 <= 256);
        Assert.True(result2 > 0 && result2 <= 256);
        Assert.True(result3 > 0 && result3 <= 256);
    }
    
    [Test, Retry(5)]
    public void CanGenerateTest()
    {
        Assert.True(_doubleGenerator.Generate(_type, _context).GetType() == _type);
    }
}