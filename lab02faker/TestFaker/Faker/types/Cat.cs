using System;

namespace TestFaker.Faker.Creator;

public class Cat
{
    public string Name { get; set; }
    
    public string type { get; set; }
    
    private int Age { get; set; }

    public Cat(string name, string type, int age)
    {
        Name = name;
        this.type = type;
        Age = age;
    }

    public int getAge()
    {
        return Age;
    }
}