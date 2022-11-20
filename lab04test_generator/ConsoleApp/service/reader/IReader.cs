namespace ConsoleApp.service.reader;

public interface IReader<S, D>
{
    D Read(S source);
}