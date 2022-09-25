using System;
using Faker.Faker;
using Faker.Faker.impl;
using Faker.Generator;
using Faker.Generator.Context;
using NUnit.Framework;

namespace TestFaker.Generator;

[TestFixture]
public class GeneratorCompositeTest
{

    private readonly IValueGenerator _generatorComposite = new GeneratorComposite();
    private readonly GeneratorContext _context = new(new Random((int)DateTime.Now.Ticks), new FakerImpl());
    
    [TestCase(typeof(int))]
    [TestCase(typeof(double))]
    [TestCase(typeof(float))]
    [TestCase(typeof(bool))]
    [TestCase(typeof(char))]
    [TestCase(typeof(string))]
    [TestCase(typeof(short))]
    [TestCase(typeof(byte))]
    [TestCase(typeof(DateTime))]
    public void CanGenerateTest(Type type)
    {
        Assert.True(_generatorComposite.CanGenerate(type));
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
    public void GenerateTest(Type type)
    {
        Assert.True(_generatorComposite.Generate(type, _context).GetType() == type);
    }
}