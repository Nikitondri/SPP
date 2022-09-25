using System;
using Faker.Faker;
using Faker.Generator.Context;
using Faker.Generator.GeneratorLeafs;
using Faker.Generator.Interface;
using NUnit.Framework;

namespace TestFaker.Generator.GeneratorLeafs;

[TestFixture]
public class DoubleGeneratorTest
{
    
    private readonly IValueGenerator _doubleGenerator = new DoubleGenerator();
    private readonly GeneratorContext _context = new(new Random(), new FakerImpl());
    private readonly Type _type = typeof(double);
    
    [Test, Retry(5)]
    public void CanGenerateTest()
    {
        var result1 = (double)_doubleGenerator.Generate(_type, _context);
        var result2 = (double)_doubleGenerator.Generate(_type, _context);
        var result3 = (double)_doubleGenerator.Generate(_type, _context);
        Assert.True(result1.GetType() == _type && result2.GetType() == _type && result3.GetType() == _type);
        Assert.True(!result1.Equals(result2) && !result1.Equals(result3) && !result2.Equals(result3));
        Assert.True(result1 is >= double.MinValue and <= double.MaxValue);
        Assert.True(result2 is >= double.MinValue and <= double.MaxValue);
        Assert.True(result3 is >= double.MinValue and <= double.MaxValue);
    }
    
    [Test, Retry(5)]
    public void GenerateTest()
    {
        Assert.True(_doubleGenerator.Generate(_type, _context).GetType() == _type);
    }
}