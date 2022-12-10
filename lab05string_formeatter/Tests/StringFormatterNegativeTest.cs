using App.exception;
using App.service.formatter;
using Tests.test_classes;

namespace Tests;

[TestFixture]
public class StringFormatterNegativeTest
{
    [Test]
    public void NonExistFieldTest()
    {
        IStringFormatter formatter = new StringFormatterImpl();
        Assert.Throws<InterpolateException>(
            () => formatter.Format("Я {privateStringField} {unknown}", CreateTestClass())
        );
    }
    
    [Test]
    public void ToManyOpenBracketsTest()
    {
        IStringFormatter formatter = new StringFormatterImpl();
        Assert.Throws<BracketsException>(
            () => formatter.Format("Я {privateStringField} {{{publicStringField}}}", CreateTestClass())
        );
    }
    
    [Test]
    public void UnclosedBracketsTest()
    {
        IStringFormatter formatter = new StringFormatterImpl();
        Assert.Throws<BracketsException>(
            () => formatter.Format("Я {privateStringField {publicStringField}", CreateTestClass())
        );
    }
    
    [Test]
    public void UnclosedBracketsInEndLineTest()
    {
        IStringFormatter formatter = new StringFormatterImpl();
        Assert.Throws<BracketsException>(
            () => formatter.Format("Я {privateStringField} {publicStringField", CreateTestClass())
        );
    }
    
    [Test]
    public void UnopenedBracketsTest()
    {
        IStringFormatter formatter = new StringFormatterImpl();
        Assert.Throws<BracketsException>(
            () => formatter.Format("Я privateStringField} {publicStringField}", CreateTestClass())
        );
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