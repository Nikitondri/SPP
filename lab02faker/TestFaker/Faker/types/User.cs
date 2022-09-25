using NUnit.Framework.Constraints;

namespace TestFaker.Faker.Creator;

public class User
{
    public string Name { get; }
    public int Age { get; set; }
    public float weight;

    public User(string name, int age)
    {
        Name = name;
        Age = age;
    }

    public User(float weight)
    {
        this.weight = weight;
    }
}