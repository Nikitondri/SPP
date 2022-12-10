using App.exception;
using App.service.formatter;
using Tests.test_classes;

namespace Tests;

[TestFixture]
public class StringFormatterPositiveTest
{

    [Test]
    public void FormattingTest()
    {
        IStringFormatter formatter = new StringFormatterImpl();
        const string expected = "Я Петя Иванов, мне 28 лет, я живу в городе Минск 2 года";
        var result = formatter.Format(
            "Я {privateStringField} {publicStringField}, мне {privateIntField}{publicIntField} лет, я живу в городе {publicStringProperty} {publicIntProperty} года",
            CreateTestClass());
        Assert.That(result, Is.EqualTo(expected));
    }

    [Test]
    public void ScreeningTest()
    {
        IStringFormatter formatter = new StringFormatterImpl();
        const string expected = "Я Петя {publicStringField}";
        var result = formatter.Format(
            "Я {privateStringField} {{publicStringField}}",
            CreateTestClass());
        Assert.That(result, Is.EqualTo(expected));
    }
    
    [Test]
    public void ObjectFieldTest()
    {
        IStringFormatter formatter = new StringFormatterImpl();
        const string expected = "overridden method";
        var result = formatter.Format(
            "{objectField}",
            CreateTestClass());
        Assert.That(result, Is.EqualTo(expected));
    }



    private TestClass CreateTestClass()
    {
        return new TestClass(
            "Петя",
            2,
            "Иванов",
            8,
            "Минск",
            2,
            new TestClass2(""));
    }
}