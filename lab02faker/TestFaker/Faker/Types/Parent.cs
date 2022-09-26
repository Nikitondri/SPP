using System.Collections.Generic;
using TestFaker.Faker.Creator;

namespace TestFaker.Faker.types;

public class Parent
{
    public string Name { get; set; }
    public List<Parent> parents { get; set; }

    public Parent(string name, List<Parent> parents)
    {
        Name = name;
        this.parents = parents;
    }
}