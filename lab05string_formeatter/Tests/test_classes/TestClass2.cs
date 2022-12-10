namespace Tests.test_classes;

public class TestClass2
{
    private string field;

    public TestClass2(string field)
    {
        this.field = field;
    }

    public override string ToString()
    {
        return "overridden method";
    }
}