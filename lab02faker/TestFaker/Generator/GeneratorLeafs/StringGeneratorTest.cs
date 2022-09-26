using System;
using Faker.Faker.impl;
using Faker.Generator;
using Faker.Generator.Context;
using Faker.Generator.GeneratorLeafs;
using NUnit.Framework;

namespace TestFaker.Generator.GeneratorLeafs;

[TestFixture]
public class StringGeneratorTest
{
        
    private readonly IValueGenerator _doubleGenerator = new StringGenerator();
    private readonly GeneratorContext _context = new(new Random(), new FakerImpl());
    private readonly Type _type = typeof(string);
    
    [Test, Retry(5)]
    public void GenerateTest()
    {
        var result1 = (string)_doubleGenerator.Generate(_type, _context);
        var result2 = (string)_doubleGenerator.Generate(_type, _context);
        var result3 = (string)_doubleGenerator.Generate(_type, _context);
        Assert.True(result1.GetType() == _type && result2.GetType() == _type && result3.GetType() == _type);
        Assert.True(!result1.Equals(result2) && !result1.Equals(result3) && !result2.Equals(result3));
        foreach (var ch in result1)
        {
            
            Assert.True(ch is >= 'a' and <= 'z');
        }
        foreach (var ch in result2)
        {
            Assert.True(ch is >= 'a' and <= 'z');
        }
        foreach (var ch in result3)
        {
            Assert.True(ch is >= 'a' and <= 'z');
        }
    }
    
    [Test, Retry(5)]
    public void CanGenerateTest()
    {
        Assert.True(_doubleGenerator.Generate(_type, _context).GetType() == _type);
    }
}