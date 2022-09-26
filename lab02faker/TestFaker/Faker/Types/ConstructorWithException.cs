using Faker.Exception;

namespace TestFaker.Faker.types;

public class ConstructorWithException
{
    public string Field1 { get; set; }
    public string Field2 { get; set; }
    public string Field3 { get; set; }

    public ConstructorWithException(string field1, string field2, string field3)
    {
        Field1 = field1;
        Field2 = field2;
        Field3 = field3;
        throw new CreateObjectException();
    }

    public ConstructorWithException(string field1, string field2)
    {
        Field1 = field1;
        Field2 = field2;
    }
}