using App.service.formatter;
using NUnit.Framework;

namespace Test;

[TestFixture]
public class StringFormatterTest
{
    private TestClass _testClass;

    [SetUp]
    public void Setup()
    {
        _testClass = new TestClass(
            "Петя",
            2,
            "Иванов",
            8,
            "Минск",
            2);
    }

    [Test]
    public void positiveTest()
    {
        var expected = "Я Петя Иванов, мне 28 лет, я живу в Минске 2 года";
        var result = StringFormatterImpl.Shared.Format(
            "Я {privateStringField} {publicStringField}, мне {privateIntField}{publicIntField} лет, я живу в городе {publicStringProperty} {publicIntProperty} года",
            _testClass);
        Assert.Equals(expected, result);
    }


    private class TestClass
    {
        private string privateStringField;
        private int privateIntField;
        public string publicStringField;
        public int publicIntField;

        public string publicStringProperty { get; }
        public int publicIntProperty { get; }

        public TestClass(string privateStringField, int privateIntField, string publicStringField, int publicIntField,
            string publicStringProperty, int publicIntProperty)
        {
            this.privateStringField = privateStringField;
            this.privateIntField = privateIntField;
            this.publicStringField = publicStringField;
            this.publicIntField = publicIntField;
            this.publicStringProperty = publicStringProperty;
            this.publicIntProperty = publicIntProperty;
        }
    }
}