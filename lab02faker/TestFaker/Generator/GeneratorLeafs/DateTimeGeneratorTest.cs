using System;
using Faker.Faker.impl;
using Faker.Generator;
using Faker.Generator.Context;
using Faker.Generator.GeneratorLeafs;
using NUnit.Framework;

namespace TestFaker.Generator.GeneratorLeafs;

[TestFixture]
public class DateTimeGeneratorTest
{
    private readonly IValueGenerator _doubleGenerator = new DateTimeGenerator();
    private readonly GeneratorContext _context = new(new Random(), new FakerImpl());
    private readonly Type _type = typeof(DateTime);

    [Test, Retry(5)]
    public void GenerateTest()
    {
        var result1 = (DateTime)_doubleGenerator.Generate(_type, _context);
        var result2 = (DateTime)_doubleGenerator.Generate(_type, _context);
        var result3 = (DateTime)_doubleGenerator.Generate(_type, _context);
        Assert.True(result1.GetType() == _type && result2.GetType() == _type && result3.GetType() == _type);
        Assert.True(!result1.Equals(result2) && !result1.Equals(result3) && !result2.Equals(result3));
        Assert.True(result1.Second >= new DateTime(1970, 1, 1).Second && 
                    result1.Second <= DateTime.Now.Second);
        Assert.True(result2.Second >= new DateTime(1970, 1, 1).Second && 
                    result2.Second <= DateTime.Now.Second);
        Assert.True(result3.Second >= new DateTime(1970, 1, 1).Second && 
                    result3.Second <= DateTime.Now.Second);
    }

    [Test, Retry(5)]
    public void CanGenerateTest()
    {
        Assert.True(_doubleGenerator.Generate(_type, _context).GetType() == _type);
    }
}