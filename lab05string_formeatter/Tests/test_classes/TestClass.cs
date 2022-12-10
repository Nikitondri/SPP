namespace Tests.test_classes;

public class TestClass
{
    private string privateStringField;
    private int privateIntField;
    public string publicStringField;
    public int publicIntField;

    public string publicStringProperty { get; }
    public int publicIntProperty { get; }
    public TestClass2 objectField;

    public TestClass(string privateStringField, int privateIntField, string publicStringField, int publicIntField,
        string publicStringProperty, int publicIntProperty, TestClass2 testClass2)
    {
        this.privateStringField = privateStringField;
        this.privateIntField = privateIntField;
        this.publicStringField = publicStringField;
        this.publicIntField = publicIntField;
        objectField = testClass2;
        this.publicStringProperty = publicStringProperty;
        this.publicIntProperty = publicIntProperty;
    }
}