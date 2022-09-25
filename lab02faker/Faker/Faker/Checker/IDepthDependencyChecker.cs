namespace Faker.Faker.Checker;

public interface IDepthDependencyChecker
{
    void Add(Type type);
    void Delete(Type type);
    bool IsMaxDepth();
}