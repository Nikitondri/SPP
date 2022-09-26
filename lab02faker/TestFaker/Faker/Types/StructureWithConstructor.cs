namespace TestFaker.Faker.types;

public class StructureWithConstructor
{
    public string name;
    public string surname;
    public int age;

    public StructureWithConstructor(string name, string surname)
    {
        this.name = name;
        this.surname = surname;
    }
}