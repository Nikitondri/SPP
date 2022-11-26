namespace ConsoleApp.service.reader;

public interface IReader<S, R>
{
    R Read(S source);
}